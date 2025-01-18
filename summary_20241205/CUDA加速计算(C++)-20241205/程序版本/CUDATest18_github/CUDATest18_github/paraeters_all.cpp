#include"paraeters_all.h"
#include<cmath>
#include<vector>
#include<Eigen/Dense>
#include <cmath>
#include "mat.h"
using namespace Eigen;
typedef Matrix<int, 1, Dynamic> RowVectorXi;










// Receiveer Defintions
const int nrec = 128;// source数量
const int nrec_all = 16384;//FMC矩阵列数
int time_ruler_st_mini_rows = 10240;//延迟时间矩阵行数
int time_ruler_st_mini_cols = 16640;//延迟时间矩阵列数











uint64_t RData_int16 = 1500 * nrec_all * sizeof(int16_T);//FMC数据占用空间
uint64_t nx_ny_int = ny * nx * sizeof(int);//成像区域占用空间
uint64_t mat_time_ruler_st_int16 = time_ruler_st_mini_rows * time_ruler_st_mini_cols * sizeof(int16_T);//延迟时间占用空间

