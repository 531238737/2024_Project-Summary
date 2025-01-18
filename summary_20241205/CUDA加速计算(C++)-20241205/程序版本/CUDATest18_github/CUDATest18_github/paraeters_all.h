#pragma once

#ifndef PARAETERS_ALL_H
#define PARAETERS_ALL_H
#include<vector>
#include<Eigen/Dense>
using namespace std;
using namespace Eigen;
typedef Matrix<int, 1, Dynamic> RowVectorXi;














// Receiveer Defintions
extern const int nrec;// source数量





extern uint64_t RData_int16;//FMC占用空间
extern uint64_t nx_ny_int;//成像区域占用空间
extern uint64_t mat_time_ruler_st_int16;//延迟时间占用空间





extern int time_ruler_st_mini_rows;//延迟时间矩阵行数
extern int time_ruler_st_mini_cols;//延迟时间矩阵列数






extern constexpr int nx = 130;//成像区域行数
extern constexpr int ny = 80;//成像区域列数



#endif