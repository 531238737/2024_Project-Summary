#include <cmath> 
#include <matplot/matplot.h> 
#include <Eigen/Dense>
#include <iostream> 
#include<opencv2/opencv.hpp>
#include <cstdio>
#include "mat.h"
#include <iostream>  
#include <cuda_runtime.h>  

#include<vector>

using namespace cv;
using namespace std;
using namespace matplot;
using namespace Eigen;
//#include"paraeters.h"
//MatrixXd cu_main();
MatrixXi cu_main();
#define _CRT_SECURE_NO_WARNINGS 1

#include <iostream>

#include <mat.h>//头文件

using namespace std;



/// <summary>
/// 主函数启动入口
/// </summary>
/// <returns></returns>
int main() {

    //MatrixXd temp_sum_image = cu_main();
    MatrixXi temp_sum_image = cu_main();








    return 0;
}