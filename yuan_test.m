% 定义矩阵的大小
Nx = 1000; % x方向的网格点数
Ny = 1000; % y方向的网格点数

% 创建一个空的矩阵
region = zeros(Nx, Ny);

% 定义矩形的基本参数
rect_x_start = 1; % 矩形起始x坐标
rect_y_start = 250; % 矩形起始y坐标
rect_x_end=1000;% 矩形结束x坐标
rect_y_end=750;% 矩形结束y坐标
rect_width = 1000; % 矩形宽度
rect_height = 500; % 矩形高度

% 生成正弦曲线的参数
num_points = rect_width; % 正弦曲线的点数
amplitude = 25; % 正弦曲线的振幅
frequency = -1* pi / rect_width; % 正弦曲线的频率

% 生成正弦曲线的点
x_arc = linspace(rect_x_start, rect_x_end, num_points);
y_arc = rect_y_start + amplitude * sin(frequency * (x_arc - rect_x_start));

% 填充矩形区域
for i = rect_x_start:Nx
   for j = 1:Ny
        
           
            % 检查是否在正弦曲线的上方
            if j >= y_arc(i)
                region(i, j) = 1; % 标记为矩形内部
            end
        
    end
end

% 可视化结果
imagesc(region');
axis equal;
xlabel('x');
ylabel('y');
title('Rectangle with a Sine Curve Arc Face');
colorbar;