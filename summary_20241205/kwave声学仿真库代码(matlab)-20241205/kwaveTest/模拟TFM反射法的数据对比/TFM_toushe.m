

tic
TFM_parameters;

%%  计算每个源的数据
temp_sum_image = zeros(ny,nx);%500，400
folderPath = 'OUTPUT_temp';
% 检查文件夹是否存在，如果不存在则创建
if ~exist(folderPath, 'dir')
    mkdir(folderPath);
end

for source_current = 0:num_of_source-1
    
    rcvData = load(strcat("INPUT_前端有信号/TFM_data_",sprintf("%g",source_current+1)));
    TFM_data=double(rcvData.sensor_data.');%转置
    %TFM_data=double(rcvData.TFM_data);%转置
    RData = TFM_data(1:Nsteps,:);
    RData(1:1000,:)=0;
   
    
    
    
   
   % RData=abs(RData);
    
    for l_total = 1:nrec % 每个接收源
        dist1 = zeros(ny,nx);
        dist2 = zeros(ny,nx);
        time_ruler_st = zeros(ny,nx);
        row_num = 1:ny; % Ny contribution (Vectorized)
        dist_combine = zeros(ny,nx);
        for col_num = 1:nx % 成像区域的每一列
           %dist1(row_num,col_num) = sqrt((x_source - x(col_num)).^2+(y_source - y(row_num)).^2);
           dist2(row_num,col_num) = sqrt((RecPos(l_total,1) - x(col_num)).^2+((RecPos(l_total,2) - y(row_num)).^2));            
          % dist_combine(row_num,col_num) = dist1(row_num,col_num) + dist2(row_num,col_num);
           dist_combine(row_num,col_num) =  dist2(row_num,col_num);
           time_ruler_st(row_num,col_num) = floor((manual_delay+dist_combine(row_num,col_num)/(bg_Vp))/(dt)); 
           for i = 1:length(time_ruler_st(:,col_num)) %遍历每一列不能出现0
               if time_ruler_st(i,col_num) == 0
                   time_ruler_st(i,col_num) = 1;
               end
           end
           if time_ruler_st(row_num,col_num) <= length(RData(:,l_total))
               temp_sum_image(row_num,col_num) =...
                   temp_sum_image(row_num,col_num)...
                   + RData(time_ruler_st(row_num,col_num),l_total);
           else
                temp_sum_image(row_num,col_num) =temp_sum_image(row_num,col_num)...
                    ;
           end
        end
    end
    parsave(int2str(source_current+1),temp_sum_image)
end



%% Plotting
close all
fig = figure(1);
%fig.Position = [0 0 1920 1920*ny/nx];
imagesc(temp_sum_image);
hold on;
colormap jet
xlim([0 nx]);
ylim([0 ny]);
xticks(0:nx/20:nx);
yticks(0:ny/20:ny);
xticklabels(1000*negat_max_x:1000*(abs(negat_max_x)+posit_max_x)...
    /20:1000*posit_max_x)
yticklabels(1000*posit_max_y:1000*-(abs(negat_max_y)+posit_max_y)/20:...
    1000*negat_max_y)
xlabel("mm")
ylabel("mm")
grid on;
saveas(gcf, 'sum_image.fig');



%% Display Operating Time
disp("Finished!")
if toc < 60*2
    fprintf("Time elapsed imaging: %g Seconds\n",toc)
elseif toc < 60*60*2
    fprintf("Time elapsed imaging: %g Minutes\n",toc/60)
else
    fprintf("Time elapsed imaging: %g Hours\n",toc/3600)
end

%% Parsave
function parsave(fname, temp_sum_image)
  save(strcat("OUTPUT_temp/",fname), 'temp_sum_image')
end
