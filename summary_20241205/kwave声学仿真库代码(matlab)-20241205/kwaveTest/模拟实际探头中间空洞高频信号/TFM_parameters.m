%% 单各物体对穿TFM实验


top_of_domain = 0;%源最上面的y的位置
bottom_of_domain=-0.085;%接收源y的位置
%% Source Defintions
num_of_source = 64; % total number of source in FMC 
%Our transducer arrays have 32 Sources, this should not change in the code
%unless we happen to change our transducers.我们的传感器阵列有32个源，这不应该在代码中更改
ch_1_loc = -0.019200; % source 1 location according to the defined coordinate system根据定义的坐标系的源1位置
%ch_1_loc should only be changed if a new transducer is used or the
%coordinate system is changed from 0,0 being at the center of the
%transducer.
%仅在使用新传感器或坐标系从传感器中心的0，0更改时才应更改ch_1_loc
ch_spacing = .0006; % channel spacing (transducer pitch)通道间隔（传感器间距）
%This spacing is unique to our transducers that are being used, it should
%not be changed if the transducer itself is not being changed.
%该间隔对于我们正在使用的传感器来说是独特的，如果传感器本身没有改变，则不应改变该间隔。

ch_y_loc = top_of_domain; %源在y方向上的位置。

%它们都是一样的，因为它们在一条线上。


%% Receiveer Defintions 接收源定义
nrec=64;  %total number of receivers in FMC for RM = NSRC   RM的NMC中接收器总数= NSRC
r_spacing = ch_spacing;
r_y_loc = bottom_of_domain;
r=1:nrec;
RecPos(r,1)=ch_1_loc+(r-1)*r_spacing;
RecPos(r,2)=r_y_loc;
%% Simulation Definitions  模拟定义
dt = 2*10^-8; % dt，20纳秒
Nsteps = 1500; %Total Steps in a single excitation event

delay_original_steps = 25; %When does the source excitation occur?
manual_delay = delay_original_steps * dt;
%% Region Defintions

%区域定义显示了我们所关心的区域，它不一定代表正在分析的整个样本。
negat_max_x = -.05;
posit_max_x = .05;
negat_max_y = -0.1;
posit_max_y = 0.1;
totalx = -negat_max_x+posit_max_x;
totaly = -negat_max_y+posit_max_y;


%Number of pixels in the x and y direction
nx = 500;
ny = 500;
row_num = 1:ny;
col_num = 1:nx;

x_source_yuan=linspace( ch_1_loc, -ch_1_loc, nrec );
x = linspace( negat_max_x, posit_max_x, nx );
y = linspace( posit_max_y, negat_max_y, ny );
bg_Vp = 6300; %Entered in m/s















