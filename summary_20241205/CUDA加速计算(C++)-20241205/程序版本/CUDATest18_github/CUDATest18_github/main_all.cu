#include <cmath> 

#include <iostream>
#include <Eigen/Dense>
#include<vector>
#include <fstream>
#include <sstream>
#include<string>
#include<ctime>
#include<time.h>
#include <chrono>
#include <algorithm>
#include<opencv2/opencv.hpp>
#include <unsupported/Eigen/CXX11/Tensor>
#include <cmath> 
#include <cuda_runtime.h>

using namespace cv;
using namespace std;
using namespace Eigen;
#include "mat.h"
#include"cuda.h"
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include"paraeters_all.h"



//以下代码作用是屏蔽waring警告
#pragma warning(default : 1234)
#pragma warning(disable : 65)

#pragma   warning(   disable  :   4290   ) 
#pragma warning(disable : 266)
#pragma warning(disable : 29)
#pragma warning(disable : 20
#pragma warning(disable : 65)
#pragma warning(disable : 169)










/// <summary>
/// 核函数，用来计算TFM成像区域
/// </summary>
/// <param name="temp_sum_image">最终成像矩阵</param>
/// <param name="RData">FMC数据</param>
/// <param name="time_ruler_st_all">延迟时间矩阵</param>
/// <param name="rows">成像区域的行数</param>
/// <param name="cols">成像区域的列数</param>
/// <param name="depth">source数</param>
/// <param name="RData_rows">FMC数据的行数</param>
/// <returns></returns>
__global__ void calculateDistances_all_8(int* temp_sum_image,int16_t* RData, int16_t* time_ruler_st_all, int rows, int cols, int depth, int RData_rows) {
    int k = blockIdx.x;//小矩阵在大矩阵中的x索引
    int l = blockIdx.y;//小矩阵在大矩阵中的y索引

    int tx = threadIdx.x;//像素点在其所在小矩阵中的x索引
    int ty = threadIdx.y;//像素点在其所在小矩阵中的y索引
    int tid = ty * blockDim.x + tx;//像素带你在其所在小矩阵中的位置索引
    int threads_per_block = blockDim.x * blockDim.y;//每个block包含的线程数

    int local_sum = 0;

    //计算像素点
    for (int idx = tid; idx < depth * depth; idx += threads_per_block) {
        int i = idx / depth;
        int j = idx % depth;
        local_sum += RData[time_ruler_st_all[(j * cols + l) * rows * depth + (i * rows + k)] +
            RData_rows * (j * depth + i)];
    }

    // 使用warp-level primitives进行归约
    for (int offset = warpSize / 2; offset > 0; offset /= 2) {
        local_sum += __shfl_down_sync(0xFFFFFFFF, local_sum, offset);
    }

    // 块内的第一个线程将warp-level归约的结果写入共享内存
    extern __shared__ int shared_sums[];
    if (tid % warpSize == 0) {
        shared_sums[tid / warpSize] = local_sum;
    }
    __syncthreads();

    // 归约warp结果
    if (tid == 0) {
        int block_sum = 0;
        for (int i = 0; i < blockDim.x * blockDim.y / warpSize; ++i) {
            block_sum += shared_sums[i];
        }
        temp_sum_image[l * rows + k] = block_sum;
    }
}








/// <summary>
/// vector变量转Matrix变量
/// </summary>
/// <param name="data"></param>
/// <returns></returns>
MatrixXd vector2DToMatrix(const vector<vector<double>>& data) {
    int rows = data.size();
    int cols = data[0].size();

    MatrixXd matrix = Map<const MatrixXd>(data[0].data(), rows, cols).eval();

    return matrix;
}







/// <summary>
/// 从 CSV 文件中读取数据并保存为vector矩阵
/// </summary>
/// <param name="filename">文件名</param>
/// <returns>vector矩阵</returns>
vector<vector<int>> readCSVToMatrix(const string& filename) {
    ifstream file(filename);
    string line;
    vector<vector<int>> matrix;

    if (file.is_open()) {
        while (getline(file, line)) {
            istringstream sline(line);
            string field;
            vector<int> row;

            while (getline(sline, field, ',')) {
                row.push_back(stoi(field)); // 将字符串转换为整数并添加到行中
            }

            matrix.push_back(row); // 将行添加到矩阵中
        }

        file.close();

    }

    return matrix;
}





//读取csv文件
template<typename M>
M load_csv(const string& path) {
    try {

        ifstream indata;
        indata.open(path);
        if (!indata.is_open()) {
            throw runtime_error("Error opening file");
        }

        string line;
        vector<double> values;
        unsigned int rows = 0;
        while (getline(indata, line)) {
            stringstream lineStream(line);
            string cell;
            while (getline(lineStream, cell, ',')) {
                values.push_back(stod(cell));
            }
            ++rows;
        }

        indata.close();


        return Map<const Matrix<typename M::Scalar, M::RowsAtCompileTime, M::ColsAtCompileTime, RowMajor>>(values.data(), rows, values.size() / rows);
    }
    catch (const exception& e) {
        cerr << "Exception caught: " << e.what() << endl;

        throw;
    }
}



/// <summary>
/// 读取csv文件转换为MatrixXd矩阵
/// </summary>
/// <param name="path">路径</param>
/// <returns>MatrixXd矩阵</returns>
MatrixXd read_csv(const string& path) {
    ifstream file(path);
    MatrixXd out(1500, 128);
    if (!file.is_open()) {
        throw runtime_error("Error opening file");

    }
    stringstream buffer;
    buffer << file.rdbuf();
    string cell;
    string line;
    int i = 0;
    while (getline(buffer, line, '\n')) {
        stringstream lineStream(line);
        string cell;
        int j = 0;
        while (getline(lineStream, cell, ',')) {
            out(i, j) = stod(cell);
            j++;
        }
        i++;
    }
    return out;

}
/// <summary>
/// 读取mat文件
/// </summary>
/// <param name="filePath">路径</param>
/// <param name="matrixName">矩阵名字</param>
/// <param name="RData">FMC矩阵</param>
void ReadMatlabMat(string  filePath, string matrixName, MatrixXd& RData)
{
    MATFile* pmatFile = NULL;
    mxArray* pMxArray = NULL;
    double* matdata;

    pmatFile = matOpen(filePath.c_str(), "r");//打开.mat文件
    if (pmatFile == NULL)
    {
        cout << "打开文件失败" << endl;
        return;
    }
    // assert(pmatFile != NULL);
    pMxArray = matGetVariable(pmatFile, matrixName.c_str());//获取.mat文件里面名为matrixName的矩阵

    matdata = mxGetPr(pMxArray);
    int rows = mxGetM(pMxArray); // 获取矩阵的行数
    int cols = mxGetN(pMxArray); // 获取矩阵的列数



    matClose(pmatFile);//close file



    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {

            RData(i, j) = matdata[j * rows + i];
        }
    }
    mxDestroyArray(pMxArray);//释放内存
    matdata = NULL;
    return;
}



/// <summary>
/// 读取mat文件并转换为32位有符号整型
/// </summary>
/// <param name="filePath">路径</param>
/// <param name="matrixName">矩阵名称</param>
/// <param name="input">FMC矩阵</param>
void ReadMatlabMat(string  filePath, string matrixName, MatrixXi& input)
{
    MATFile* pmatFile = NULL;
    mxArray* pMxArray = NULL;



    pmatFile = matOpen(filePath.c_str(), "r");//打开.mat文件
    if (pmatFile == NULL)
    {
        cout << "打开文件失败" << endl;
        return;
    }
    else
    {
        cout << "打开文件成功" << endl;

    }

    // assert(pmatFile != NULL);
    pMxArray = matGetVariable(pmatFile, matrixName.c_str());//获取.mat文件里面名为matrixName的矩阵



    int32_T* pData = (int32_T*)mxGetData(pMxArray); // 获取整数数据指针
    // 进行相关操作



    int rows = mxGetM(pMxArray); // 获取矩阵的行数
    int cols = mxGetN(pMxArray); // 获取矩阵的列数



    matClose(pmatFile);//close file



    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {

            input(i, j) = (pData[j * rows + i]);
        }
    }
    mxDestroyArray(pMxArray);//释放内存

    return;
}


/// <summary>
/// 获取mat文件矩阵指针
/// </summary>
/// <param name="filePath">路径</param>
/// <param name="matrixName">矩阵名字</param>
/// <param name="time_ruler_st_pointer">延迟时间矩阵指针</param>
/// <param name="pmatFile">指向 MATFile 类型的指针，用于处理 .mat 文件的文件对象。</param>
/// <param name="pMxArray">指向 mxArray 类型的指针，用于存储从 .mat 文件中读取的矩阵数据</param>
void ReadMatlabDataAndProcess(string filePath, string matrixName, int32_T*& time_ruler_st_pointer, MATFile*& pmatFile, mxArray*& pMxArray) {

    pmatFile = matOpen(filePath.c_str(), "r");//打开.mat文件
    if (pmatFile == NULL)
    {
        cout << "打开文件失败" << endl;
        return;
    }
    else
    {
        cout << "打开文件成功" << endl;

    }

    // assert(pmatFile != NULL);
    pMxArray = matGetVariable(pmatFile, matrixName.c_str());//获取.mat文件里面名为matrixName的矩阵
    if (pMxArray == NULL) {
        cout << "获取矩阵失败" << endl;
        matClose(pmatFile);
        return;
    }
    time_ruler_st_pointer = (int32_T*)mxGetData(pMxArray); // 获取整数数据指针

}



/// <summary>
/// 获取mat文件矩阵指针转换为16位整数
/// </summary>
/// <param name="filePath">路径</param>
/// <param name="matrixName">矩阵名字</param>
/// <param name="time_ruler_st_pointer">延迟时间矩阵指针</param>
/// <param name="pmatFile">指向 MATFile 类型的指针，用于处理 .mat 文件的文件对象。</param>
/// <param name="pMxArray">指向 mxArray 类型的指针，用于存储从 .mat 文件中读取的矩阵数据</param>
void ReadMatlabDataAndProcess(string filePath, string matrixName, int16_T*& time_ruler_st_pointer, MATFile*& pmatFile, mxArray*& pMxArray) {

    pmatFile = matOpen(filePath.c_str(), "r");//打开.mat文件
    if (pmatFile == NULL)
    {
        cout << "打开文件失败" << endl;
        return;
    }
    else
    {
        cout << "打开文件成功" << endl;

    }

    // assert(pmatFile != NULL);
    pMxArray = matGetVariable(pmatFile, matrixName.c_str());//获取.mat文件里面名为matrixName的矩阵
    if (pMxArray == NULL) {
        cout << "获取矩阵失败" << endl;
        matClose(pmatFile);
        return;
    }
    time_ruler_st_pointer = (int16_T*)mxGetData(pMxArray); // 获取整数数据指针

}







/// <summary>
/// 获取mat文件矩阵指针转换为32位整数
/// </summary>
/// <param name="filePath">路径</param>
/// <param name="matrixName">矩阵名字</param>
/// <param name="time_ruler_st_pointer">延迟时间矩阵指针</param>
/// <param name="pmatFile">指向 MATFile 类型的指针，用于处理 .mat 文件的文件对象。</param>
/// <param name="pMxArray">指向 mxArray 类型的指针，用于存储从 .mat 文件中读取的矩阵数据</param>
MatrixXi ReadMatlabMat(string  filePath, string matrixName)
{
    MATFile* pmatFile = NULL;
    mxArray* pMxArray = NULL;
    MatrixXi out(1500, 128);


    pmatFile = matOpen(filePath.c_str(), "r");//打开.mat文件
    if (pmatFile == NULL)
    {
        cout << "打开文件失败" << endl;
        return out;
    }
    // assert(pmatFile != NULL);
    pMxArray = matGetVariable(pmatFile, matrixName.c_str());//获取.mat文件里面名为matrixName的矩阵



    int32_T* pData = (int32_T*)mxGetData(pMxArray); // 获取整数数据指针
    // 进行相关操作



    int rows = mxGetM(pMxArray); // 获取矩阵的行数
    int cols = mxGetN(pMxArray); // 获取矩阵的列数




    matClose(pmatFile);//close file



    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {

            out(i, j) = (pData[j * rows + i]);
        }
    }
    mxDestroyArray(pMxArray);//释放内存

    return out;
}







/// <summary>
/// 保存数据成csv
/// </summary>
/// <param name="fileName">文件名</param>
/// <param name="matrix">保存MatrixXd矩阵</param>
void saveData(string fileName, MatrixXd  matrix)
{

    const static IOFormat CSVFormat(FullPrecision, DontAlignCols, ", ", "\n");

    ofstream file(fileName);
    if (file.is_open())
    {
        file << matrix.format(CSVFormat);
        file.close();
    }
}

/// <summary>
/// 保存数据成csv
/// </summary>
/// <param name="fileName">文件名</param>
/// <param name="matrix">保存MatrixXi矩阵</param>
void saveData(string fileName, MatrixXi  matrix)
{

    const static IOFormat CSVFormat(FullPrecision, DontAlignCols, ", ", "\n");

    ofstream file(fileName);
    if (file.is_open())
    {
        file << matrix.format(CSVFormat);
        file.close();
    }
}
/// <summary>
/// 保存数据成csv
/// </summary>
/// <param name="fileName">文件名</param>
/// <param name="matrix">保存RowVectorXd矩阵</param>
void saveData(string fileName, RowVectorXd  matrix)
{

    const static IOFormat CSVFormat(FullPrecision, DontAlignCols, ", ", "\n");

    ofstream file(fileName);
    if (file.is_open())
    {
        file << matrix.format(CSVFormat);
        file.close();
    }
}
/// <summary>
/// 保存数据成csv
/// </summary>
/// <param name="fileName">文件名</param>
/// <param name="matrix">保存RowVectorXi矩阵</param>
void saveData(string fileName, RowVectorXi  matrix)
{

    const static IOFormat CSVFormat(FullPrecision, DontAlignCols, ", ", "\n");

    ofstream file(fileName);
    if (file.is_open())
    {
        file << matrix.format(CSVFormat);
        file.close();
    }
}


/// <summary>
/// 渲染图像
/// </summary>
/// <param name="matrix">图像矩阵</param>
void scaleMatrixToImage(const MatrixXd& matrix) {
    // 找到矩阵中的最小值和最大值
    double minVal = matrix.minCoeff();
    double maxVal = matrix.maxCoeff();

    // 创建一个 CV_8U 类型的图像矩阵
    Mat image(matrix.rows(), matrix.cols(), CV_8U);

    // 缩放矩阵数据到 [0, 255] 范围
    /*MatrixXd scaledMatrix = (matrix.array() - minVal) / (maxVal - minVal) * 255.0;*/
    MatrixXd scaledMatrix = (matrix.array() - minVal) / (maxVal - minVal) * 255.0;
    scaledMatrix = scaledMatrix.array().max(0.0).min(255.0); // 将数据限制在 [0, 255] 范围内
    // 将矩阵数据转换为 CV_8U 类型的图像数据
    for (int i = 0; i < matrix.rows(); i++) {
        for (int j = 0; j < matrix.cols(); j++) {
            image.at<uchar>(i, j) = static_cast<uchar>(scaledMatrix(i, j));
        }
    }

    // 创建带有外边距的新图像


    applyColorMap(image, image, COLORMAP_JET);

    // 显示图像
    namedWindow("Scaled Image", WINDOW_NORMAL);
    imshow("Scaled Image", image);
    waitKey(0);


}


/// <summary>
/// 渲染图象
/// </summary>
/// <param name="matrix">整数图像矩阵</param>
void scaleMatrixToImage(const MatrixXi& matrix) {
    // 找到矩阵中的最小值和最大值
    int minVal = matrix.minCoeff();
    int maxVal = matrix.maxCoeff();

    // 创建一个 CV_8U 类型的图像矩阵
    Mat image(matrix.rows(), matrix.cols(), CV_8U);

    // 缩放矩阵数据到 [0, 255] 范围

    MatrixXd scaledMatrix = (matrix.cast<double>().array() - minVal) / (maxVal - minVal) * 255.0;
    // saveData("out/mat/scaledMatrix1.csv", scaledMatrix);
    scaledMatrix = scaledMatrix.array().max(0.0).min(255.0); // 将数据限制在 [0, 255] 范围内
     saveData("out/mat/scaledMatrix_all.csv", scaledMatrix);
     // 将矩阵数据转换为 CV_8U 类型的图像数据
    for (int i = 0; i < matrix.rows(); i++) {
        for (int j = 0; j < matrix.cols(); j++) {
            image.at<uchar>(i, j) = static_cast<uchar>(scaledMatrix(i, j));
        }
    }

    // 创建带有外边距的新图像


    applyColorMap(image, image, COLORMAP_JET);
    cout << "显示图像" << endl;
    string winname = "MyWindow";


    // 显示图像
    namedWindow(winname, WINDOW_KEEPRATIO);
    imshow(winname, image);
    waitKey(0);



}







/// <summary>
/// 主函数
/// </summary>
/// <returns></returns>
MatrixXi cu_main() {
    int deviceCount;
    cudaGetDeviceCount(&deviceCount);//获取GPU信息
    for (int i = 0; i < deviceCount; i++)
    {
        cudaDeviceProp devProp;
        cudaGetDeviceProperties(&devProp, i);
        cout << "使用GPU device " << i << ": " << devProp.name << endl;
        cout << "设备全局内存总量： " << devProp.totalGlobalMem / 1024 / 1024 << "MB" << std::endl;
        cout << "SM的数量：" << devProp.multiProcessorCount << endl;
        cout << "每个线程块的共享内存大小：" << devProp.sharedMemPerBlock / 1024.0 << " KB" << endl;
        cout << "每个线程块的最大线程数：" << devProp.maxThreadsPerBlock << endl;
        cout << "设备上一个线程块（Block）种可用的32位寄存器数量： " << devProp.regsPerBlock << endl;

        cout << "每个EM的最大线程数：" << devProp.maxThreadsPerMultiProcessor << endl;
        cout << "每个EM的最大线程束数：" << devProp.maxThreadsPerMultiProcessor / 32 << endl;
        cout << "设备上多处理器的数量： " << devProp.multiProcessorCount << endl;
        cout << "======================================================" << endl;

    }

    MatrixXi  temp_sum_image = MatrixXi::Zero(ny, nx);


    try {

        auto start = chrono::high_resolution_clock::now();
        //初始化各项参数
        //申请并分配内存

        int* d_temp_sum_image;
        int16_T* d_mat_time_ruler_st, * time_ruler_st_pointer, * d_mat_RData, * RData_pointer;//申明指针


        auto start1 = chrono::high_resolution_clock::now();
        string filename1 = "data/mat/RData_int16.mat";

        MATFile* pmatFile_RData = NULL;
        mxArray* pMxArray_RData = NULL;
        MATFile* pmatFile = NULL;
        mxArray* pMxArray = NULL;
        string matrixName1 = "result";
        //ReadMatlabMat(filename, "result", RData);
        ReadMatlabDataAndProcess(filename1, matrixName1, RData_pointer, pmatFile_RData, pMxArray_RData);//读取FMC数据


        string filename2 = "data/mat/time_ruler_st/time_ruler_st_mini.mat";
        string matrixName2 = "result_some";
        ReadMatlabDataAndProcess(filename2, matrixName2, time_ruler_st_pointer, pmatFile, pMxArray);//读取延迟时间矩阵

        //位gpu分配内存
        cudaMalloc(&d_temp_sum_image, nx_ny_int);
        cudaMalloc(&d_mat_RData, RData_int16);
        cudaMalloc(&d_mat_time_ruler_st, mat_time_ruler_st_int16);


        // 分配数据
        cudaMemcpy(d_temp_sum_image, temp_sum_image.data(), nx_ny_int, cudaMemcpyHostToDevice);
        cudaMemcpy(d_mat_RData, RData_pointer, RData_int16, cudaMemcpyHostToDevice);
        cudaMemcpy(d_mat_time_ruler_st, time_ruler_st_pointer, mat_time_ruler_st_int16, cudaMemcpyHostToDevice);
        auto end1 = chrono::high_resolution_clock::now();
        // 计算时间差
        chrono::duration<double> duration1 = end1 - start1;
        cout << "读取RData与time_ruler_st花费时间：" << duration1.count() << "s" << endl;


        int RData_rows = 1500;



        auto start2 = chrono::high_resolution_clock::now();










        // 定义线程块大小
        dim3 blockDim(32, 32);  // 16x16 = 256 个线程

        // 定义网格大小
        dim3 gridDim(80, 130);  // 80 个 k 值，130 个 l 值

        // 计算共享内存大小（以字节为单位）
        size_t shared_mem_size = 1024 * sizeof(int32_t);  // 每个线程一个 int

        

        // 启动核函数
        calculateDistances_all_8 << <gridDim, blockDim, shared_mem_size >> > (d_temp_sum_image, d_mat_RData, d_mat_time_ruler_st, ny, nx, nrec,  RData_rows);


        cudaDeviceSynchronize();//等待gpu任务完成
        auto end2 = chrono::high_resolution_clock::now();

        chrono::duration<double> duration2 = end2 - start2;
        cout << "cuda运行花费时间" << duration2.count() << "秒" << endl;

        ;
        cudaMemcpy(temp_sum_image.data(), d_temp_sum_image, nx_ny_int, cudaMemcpyDeviceToHost);


        //释放gpu内存
        cudaFree(d_temp_sum_image);

        cudaFree(d_mat_time_ruler_st);
        cudaFree(d_mat_RData);

        matClose(pmatFile);
        mxDestroyArray(pMxArray);//释放内存
        matClose(pmatFile_RData);
        mxDestroyArray(pMxArray_RData);//释放内存
        // 最终保存整个计算过程中的temp_sum_image
        //saveData("out/mat/temp_sum_image_final_mat.csv", temp_sum_image);
        cudaDeviceReset();//重置CUDA，释放所有内存，销毁事件
        auto end = chrono::high_resolution_clock::now();
        // 计算时间差
        chrono::duration<double> duration = end - start;
        // 输出程序运行时间，以秒为单位

        cout << "程序运行时间: " << duration.count() << " 秒" << endl;

        saveData("out/mat/scaledMatrix4.csv", temp_sum_image);
        scaleMatrixToImage(temp_sum_image);

        // matrixToImage(temp_sum_image);
    }
    catch (const cv::Exception& e) {
        // 捕获异常并输出异常信息
        cerr << "OpenCV Exception caught: " << e.what() << endl;
    }

    catch (...) {
        // 捕获未知异常并输出异常信息
        cerr << "Unknown Exception caught." << endl;
    }

    return temp_sum_image;
}


