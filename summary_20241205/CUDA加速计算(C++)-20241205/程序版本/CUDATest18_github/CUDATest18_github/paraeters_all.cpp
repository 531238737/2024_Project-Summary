#include"paraeters_all.h"
#include<cmath>
#include<vector>
#include<Eigen/Dense>
#include <cmath>
#include "mat.h"
using namespace Eigen;
typedef Matrix<int, 1, Dynamic> RowVectorXi;










// Receiveer Defintions
const int nrec = 128;// source����
const int nrec_all = 16384;//FMC��������
int time_ruler_st_mini_rows = 10240;//�ӳ�ʱ���������
int time_ruler_st_mini_cols = 16640;//�ӳ�ʱ���������











uint64_t RData_int16 = 1500 * nrec_all * sizeof(int16_T);//FMC����ռ�ÿռ�
uint64_t nx_ny_int = ny * nx * sizeof(int);//��������ռ�ÿռ�
uint64_t mat_time_ruler_st_int16 = time_ruler_st_mini_rows * time_ruler_st_mini_cols * sizeof(int16_T);//�ӳ�ʱ��ռ�ÿռ�

