close all;
clc;
clear;
% create the computational grid
Nx = 667;           % number of grid points in the x (row) direction
Ny = 334;           % number of grid points in the y (column) direction
dx = 3e-4;        % grid point spacing in the x direction [m]
dy = 3e-4;        % grid point spacing in the y direction [m]
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
medium.sound_speed(164:170,140:148)=340;
medium.density = 2700 * ones(Nx, Ny);       % [kg/m^3]
medium.density(164:170,140:148) = 1000;   


t_end = 5e-5;       % [s]
Nt=3000;
kgrid.dt=2e-8;
kgrid.setTime(Nt,kgrid.dt);
%kgrid.makeTime( medium.sound_speed, [], t_end);


% define source mask for a linear transducer with an odd number of elements  
num_elements = 64;      % [grid points]
x_offset = 50;          % [grid points]
source.p_mask = zeros(Nx, Ny);
start_index = Ny/2 - round(num_elements/2)*2 + 1;
source.p_mask(x_offset, start_index:2:start_index + num_elements*2 - 1) = 1;



source.p =zeros(num_elements,length(kgrid.t_array));
for i=1:64
    source.p(i,i*5)=100;
    
    
end








sensor.mask = zeros(Nx, Ny);
% 假设我们想要将第二行每隔两个数据变为1

sensor.mask(335, start_index:2:start_index + num_elements*2 - 1) = 1;


 

 

% define the input arguments
input_args = {'RecordMovie', true, 'MovieName', 'example_movie_many_guanghua_1','PlotScale', [-2, 2], 'PlotFreq', 5,'DisplayMask', source.p_mask,'DisplayMask',medium.sound_speed};
 
% run the simulation
sensor_data=kspaceFirstOrder2D(kgrid, medium, source, sensor, 'PlotLayout', true, 'PlotPML', false,input_args{:});


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




% get allowable VideoWriter profiles
profiles = VideoWriter.getProfiles();
 
% if MPEG profile exists, run second simulation
if any(ismember({profiles.Name}, 'MPEG-4')) 
 
    % define a second set of input arguments
    input_args = {'MeshPlot', true, 'DisplayMask', 'off', 'PlotPML', false, ...
        'PlotFreq', 5, 'RecordMovie', true, 'MovieName', 'example_movie_many_guanghua_2', ...
        'MovieProfile', 'MPEG-4', 'MovieArgs', {'FrameRate', 10}};
    % run the simulation again
    kspaceFirstOrder2D(kgrid, medium, source, sensor, input_args{:});
 
else
    
    % display warning about MPEG profile
    disp('The optional input ''MovieProfile'', ''MPEG-4'' is not supported in your version of MATLAB');
 
end



