#pragma once

#ifndef PARAETERS_ALL_H
#define PARAETERS_ALL_H
#include<vector>
#include<Eigen/Dense>
using namespace std;
using namespace Eigen;
typedef Matrix<int, 1, Dynamic> RowVectorXi;














// Receiveer Defintions
extern const int nrec;// source����





extern uint64_t RData_int16;//FMCռ�ÿռ�
extern uint64_t nx_ny_int;//��������ռ�ÿռ�
extern uint64_t mat_time_ruler_st_int16;//�ӳ�ʱ��ռ�ÿռ�





extern int time_ruler_st_mini_rows;//�ӳ�ʱ���������
extern int time_ruler_st_mini_cols;//�ӳ�ʱ���������






extern constexpr int nx = 130;//������������
extern constexpr int ny = 80;//������������



#endif