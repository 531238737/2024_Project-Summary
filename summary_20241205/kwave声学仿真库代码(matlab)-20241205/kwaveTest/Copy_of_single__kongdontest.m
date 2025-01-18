close all;
clc;
clear;
% create the computational grid
Nx = 512;           % number of grid points in the x (row) direction
Ny = 512;           % number of grid points in the y (column) direction
dx = 0.1e-1;        % grid point spacing in the x direction [m]
dy = 0.1e-1;        % grid point spacing in the y direction [m]
kgrid = kWaveGrid(Nx, dx, Ny, dy);

% % define the properties of the propagation medium
% medium.sound_speed = 6300 * ones(Nx, Ny);   % [m/s]
% medium.sound_speed(60:68,60 :68) = 1800;       % [m/s]
% medium.density = 2700 * ones(Nx, Ny);       % [kg/m^3]
% medium.density(60:68,60 :68) = 1.225;          % [kg/m^3]

% define the properties of the propagation medium

medium.alpha_coeff = 0.75;  % [dB/(MHz^y cm)]
medium.alpha_power = 1.5;

medium.sound_speed = 6300 * ones(Nx, Ny); 
medium.sound_speed(253:258,253:258)=340;
medium.density = 2700 * ones(Nx, Ny);       % [kg/m^3]
medium.density(253:258,253 :258) = 1000;   


t_end = 1.5e-3;       % [s]
kgrid.makeTime( medium.sound_speed, [], t_end);
% create initial pressure distribution using makeDisc
disc_magnitude = 100; % [Pa]
disc_x_pos = 22;    % [grid points]
disc_y_pos = 256;    % [grid points]
disc_radius = 0;    % [grid points]
disc_1 = disc_magnitude * makeDisc(Nx, Ny, disc_x_pos, disc_y_pos, disc_radius);

% disc_magnitude = 3; % [Pa]
% disc_x_pos = 80;    % [grid points]
% disc_y_pos = 60;    % [grid points]
% disc_radius = 5;    % [grid points]
% disc_2 = disc_magnitude * makeDisc(Nx, Ny, disc_x_pos, disc_y_pos, disc_radius);

source.p0 = disc_1 ;


sensor.mask = zeros(Nx, Ny);
% 
for i=1:8:512
    sensor.mask(490, i) = 1;

end
 

 

% define the input arguments
input_args = {'RecordMovie', true, 'MovieName', 'example_movie_1','PlotScale', [-2, 2], 'PlotFreq', 5,'DisplayMask', medium.sound_speed};
 
% run the simulation
sensor_data=kspaceFirstOrder2D(kgrid, medium, source, sensor, 'PlotLayout', true, 'PlotPML', false,input_args{:});

% get allowable VideoWriter profiles
profiles = VideoWriter.getProfiles();
 
% if MPEG profile exists, run second simulation
if any(ismember({profiles.Name}, 'MPEG-4')) 
 
    % define a second set of input arguments
    input_args = {'MeshPlot', true, 'DisplayMask', 'off', 'PlotPML', false, ...
        'PlotFreq', 5, 'RecordMovie', true, 'MovieName', 'example_movie_2', ...
        'MovieProfile', 'MPEG-4', 'MovieArgs', {'FrameRate', 10}};
    % run the simulation again
    kspaceFirstOrder2D(kgrid, medium, source, sensor, input_args{:});
 
else
    
    % display warning about MPEG profile
    disp('The optional input ''MovieProfile'', ''MPEG-4'' is not supported in your version of MATLAB');
 
end


figure;
imagesc(sensor_data, [-1, 1]);
colormap(getColorMap);
ylabel('Sensor Position');
xlabel('Time Step');
colorbar;




figure;
plot(sensor_data(2,:), 'r-');
hold on;
plot(sensor_data(60,:), 'b-');
hold on;
plot(sensor_data(32,:), 'g-');
legend('第2个通道', '第60个通道','第32个通道');
xlabel('Time Index');
ylabel('Pressure');
axis tight;