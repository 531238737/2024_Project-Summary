close all;
clc;
clear;
% create the computational grid
Nx = 3300;           % number of grid points in the x (row) direction
Ny = 15300;           % number of grid points in the y (column) direction
dx = 3e-4;        % grid point spacing in the x direction [m]
dy = 3e-4;        % grid point spacing in the y direction [m]
kgrid = kWaveGrid(Nx, dx, Ny, dy);

a=zeros(3300,15300);

% 圆的中心点和半径，存储在数组中
centers = [1650, 4655; 1650, 6243; 1650, 7494;1650,8644;1650,9715;1650,10735]; % 中心点数组，每行是一个圆的中心
radii = [258, 159, 99,60,40,20]; % 对应的半径数组

% 创建坐标网格
[X, Y] = meshgrid(1:size(kgrid.k,2), 1:size(kgrid.k,1));

% 遍历每个圆
for i = 1:size(centers, 1)
    % 计算每个点到当前圆心的距离
    distance = sqrt((X - centers(i, 2)).^2 + (Y - centers(i, 1)).^2);
    
    % 将距离小于或等于当前圆半径的点设置为1
    a(distance <= radii(i)) = 1;
end




% define the properties of the propagation medium

medium.alpha_coeff = 0.75;  % [dB/(MHz^y cm)]
medium.alpha_power = 1.5;

medium.sound_speed = 1483 * ones(Nx, Ny); 
medium.sound_speed(60:3240,60:15240)=5700;
medium.density = 1000 * ones(Nx, Ny);       % [kg/m^3]
medium.density(60:3240,60:15240)=2700;


% 遍历每个圆
for i = 1:size(centers, 1)
    % 计算每个点到当前圆心的距离
    distance = sqrt((X - centers(i, 2)).^2 + (Y - centers(i, 1)).^2);
    
    % 将距离小于或等于当前圆半径的点设置为1
    medium.sound_speed(distance <= radii(i)) = 1483;
    medium.density(distance <= radii(i)) = 1000;
end


Nt=1500;
kgrid.dt=2e-8;
kgrid.setTime(Nt,kgrid.dt);



% define source mask for a linear transducer with an odd number of elements  
num_elements = 64;      % [grid points]
x_offset = 59;          % [grid points]

start_index = Ny/2 - round(num_elements/2)*60 + 1;







disc_radius = 0;    % [grid points]









sensor.mask = zeros(Nx, Ny);
% 假设我们想要将第二行每隔两个数据变为1

sensor.mask(3241, start_index:2:start_index + num_elements*60 - 1) = 1;


folderPath = 'OUTPUT';
% 检查文件夹是否存在，如果不存在则创建
if ~exist(folderPath, 'dir')
    mkdir(folderPath);
end


source_freq = 5e6;   % [Hz]
source_mag = 100;         % [Pa]

 
for i=1:64
    source.p_mask = zeros(Nx, Ny);
    source.p_mask(x_offset, start_index+2*(i-1)) = 1;



    disc_x_pos = x_offset;    % [grid points]
    disc_y_pos = start_index+2*(i-1);    % [grid points]
    disc_1 =  source_mag * sin(2 * pi * source_freq * kgrid.t_array);
    disc_1(200:end)=0;
    source.p = disc_1 ;

    % define the input arguments
    input_args = {'RecordMovie', true, 'MovieName', 'example_movie_many_guanghua_1','PlotScale', [-2, 2], 'PlotFreq', 5,'DisplayMask', sensor.mask};
 
    % run the simulation
    sensor_data=kspaceFirstOrder2D(kgrid, medium, source, sensor, 'PlotLayout', false, 'PlotPML', false,input_args{:});

    filename = sprintf('TFM_data_%d.mat', i);
     % 完整的文件路径
    fullFilePath = fullfile(folderPath, filename);
     save(fullFilePath, 'sensor_data');
     fprintf('完成第 %d 次循环\n', i);
    
    

end
 

% 
% 
% 
% figure;
% imagesc(sensor_data, [-1, 1]);
% colormap(getColorMap);
% ylabel('Sensor Position');
% xlabel('Time Step');
% colorbar;
% 
% 
% 
% 
% figure;
% plot(sensor_data(2,:), 'r-');
% hold on;
% plot(sensor_data(60,:), 'b-');
% hold on;
% plot(sensor_data(32,:), 'g-');
% legend('第2个通道', '第60个通道','第32个通道');
% xlabel('Time Index');
% ylabel('Pressure');
% axis tight;
% 
% 
% 
% 
% % get allowable VideoWriter profiles
% profiles = VideoWriter.getProfiles();
% 
% % if MPEG profile exists, run second simulation
% if any(ismember({profiles.Name}, 'MPEG-4')) 
% 
%     % define a second set of input arguments
%     input_args = {'MeshPlot', true, 'DisplayMask', 'off', 'PlotPML', false, ...
%         'PlotFreq', 5, 'RecordMovie', true, 'MovieName', 'example_movie_many_guanghua_2', ...
%         'MovieProfile', 'MPEG-4', 'MovieArgs', {'FrameRate', 10}};
%     % run the simulation again
%     kspaceFirstOrder2D(kgrid, medium, source, sensor, input_args{:});
% 
% else
% 
%     % display warning about MPEG profile
%     disp('The optional input ''MovieProfile'', ''MPEG-4'' is not supported in your version of MATLAB');
% 
% end



