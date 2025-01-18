%Date: 10th May 2023
%Description: This script utilize the total focusing method and output the
%mapping of the defined region. 

% INPUTS: 
% 1) FMC data need to be available in the data folder. Keep the data folder
% in the working folder.
% 2) experimental set up parameters need to be adjusted accordingly to get
% accurate results

tic
TFM_parameters;

%% What would benefit to be submitted in parralel to GPUs
for source_current = 0:num_of_source-1
    x_source= ch_1_loc +(source_current)*ch_spacing;
    y_source=ch_y_loc;
    rcvData = load(strcat("INPUT/SingleElementExcitation_element_",sprintf("%g",source_current+1)));    
    RData = double(rcvData.RcvData(1:Nsteps,:));
    
    temp_sum_image = zeros(ny,nx);
    for l_total = 1:nrec % (Not vectorized)
        dist1 = zeros(ny,nx);
        dist2 = zeros(ny,nx);
        time_ruler_st = zeros(ny,nx);
        row_num = 1:ny; % Ny contribution (Vectorized)
        dist_combine = zeros(ny,nx);
        for col_num = 1:nx % Nx contribution (Not vectorized)
           dist1(row_num,col_num) = sqrt((x_source - x(col_num)).^2+(y_source - y(row_num)).^2);
           dist2(row_num,col_num) = sqrt((RecPos(l_total,1) - x(col_num)).^2+((RecPos(l_total,2) - y(row_num)).^2));            
           dist_combine(row_num,col_num) = dist1(row_num,col_num) + dist2(row_num,col_num);
           time_ruler_st(row_num,col_num) = floor((manual_delay+dist_combine(row_num,col_num)/(bg_Vp))/(dt)); 
           for i = 1:length(time_ruler_st(:,col_num)) 
               if time_ruler_st(i,col_num) == 0
                   time_ruler_st(i,col_num) = 1;
               end
           end
           if time_ruler_st(row_num,col_num) <= length(RData(:,l_total))
               temp_sum_image(row_num,col_num) =...
                   temp_sum_image(row_num,col_num)...
                   + RData(time_ruler_st(row_num,col_num),l_total);
           end
        end
    end
    parsave(int2str(source_current+1),temp_sum_image)
end

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
  save(strcat("sliceImages/ImageSliceSource",fname), 'temp_sum_image')
end
