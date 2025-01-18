top_of_domain = 0.01588;
%% Source Defintions
num_of_source = 128; % total number of source in FMC 
%Our transducer arrays have 32 Sources, this should not change in the code
%unless we happen to change our transducers.我们的传感器阵列有32个源，这不应该在代码中更改
ch_1_loc = -0.038100; % source 1 location according to the defined coordinate system根据定义的坐标系的源1位置
%ch_1_loc should only be changed if a new transducer is used or the
%coordinate system is changed from 0,0 being at the center of the
%transducer.
%仅在使用新传感器或坐标系从传感器中心的0，0更改时才应更改ch_1_loc
ch_spacing = .0006; % channel spacing (transducer pitch)通道间隔（传感器间距）
%This spacing is unique to our transducers that are being used, it should
%not be changed if the transducer itself is not being changed.
%该间隔对于我们正在使用的传感器来说是独特的，如果传感器本身没有改变，则不应改变该间隔。

ch_y_loc = top_of_domain; % Position of sources in the y direction, 源在y方向上的位置，。
%they are all the same since they're in a line.它们都是一样的，因为它们在一条线上。

AR=ch_spacing*num_of_source;  % transducer array aparture传感器阵列分离
%% Receiveer Defintions 接收源定义
nrec=128;  %total number of receivers in FMC for RM = NSRC   RM的NMC中接收器总数= NSRC
r_spacing = ch_spacing;
r_y_loc = top_of_domain;
r=1:nrec;
RecPos(r,1)=ch_1_loc+(r-1)*r_spacing;
RecPos(r,2)=r_y_loc;
%% Simulation Definitions  模拟定义
dt = 2*10^-8; % dt
Nsteps = 1500; %Total Steps in a single excitation event
time_length_in_specfem2d = Nsteps*dt; %Total timespan for the data set. (seconds)数据集的总时间跨度。
total_timesteps_for_csic = floor(time_length_in_specfem2d / dt);        
travel_time_full = zeros(1,nrec);
delay_original_steps = 25; %When does the source excitation occur?
manual_delay = delay_original_steps * dt;
%% Region Defintions
%Region definition showcases the area that we are concerned with, it
%doesn't necessarily represent the entire specimen that is being analyzed.
%区域定义显示了我们所关心的区域，它不一定代表正在分析的整个样本。
negat_max_x = -.06352;
posit_max_x = .06352;
negat_max_y = -0.01588;
posit_max_y = 0.01588;
totalx = -negat_max_x+posit_max_x;
totaly = -negat_max_y+posit_max_y;


%Number of pixels in the x and y direction
nx = 1100;
ny = 525;
row_num = 1:ny;
col_num = 1:nx;
%Enabling GPU will allow us to use higher-order version of this

x = linspace( negat_max_x, posit_max_x, nx );
y = linspace( posit_max_y, negat_max_y, ny );
bg_Vp = 5700; %Entered in m/s

%% Post Processing
times_counter = 0; %Need Explaination
thresholdValue= 0.5; %Need Explaination
threshold = 0.2; %Need Explaination
threshold2 = 0.1; 

%% Plotting
flag_avg = 0;
%%














