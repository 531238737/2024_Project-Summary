#include <iostream>
#include <vector>
#include <queue>
#include <cmath>
#include <limits>
#include <Eigen/Dense>
#include <xlnt/xlnt.hpp>
using namespace xlnt;

using namespace std;

const int N = 800; // 网格的宽度
const int M = 1400; // 网格的高度
const double speed_of_sound = 6320; // 铝中的声速，以微秒/厘米为单位
const double dx = 0.1; // 网格间距
const double dy = 0.1; // 网格间距

// 用于存储从发射阵元到每个网格点的最短路径的延迟时间
vector<vector<vector<double>>> delays(64, vector<vector<double>>(N, vector<double>(M, numeric_limits<double>::max())));

// Dijkstra 算法的实现
void dijkstra(const vector<vector<vector<double>>>& g, int start_x, int start_y) {
    int n = g.size();
    vector<vector<double>> dis(N, vector<double>(M, numeric_limits<double>::max()));
    vector<vector<bool>> visited(N, vector<bool>(M, false));
    // 创建堆，根据到起始点的距离排序
    priority_queue<pair<double, pair<int, int>>, vector<pair<double, pair<int, int>>>, greater<>> pq;
    pq.emplace(0, make_pair(start_x, start_y)); // 起始点到它自己的距离是 0
    dis[start_x][start_y] = 0;

    int source = 0;
    while (!pq.empty()&& source<1) {
        int k_x = pq.top().second.first;
        int k_y = pq.top().second.second;
        pq.pop();
        if (visited[k_x][k_y]) continue;
        visited[k_x][k_y] = true;
        for (int j = 0; j < N; j++) {
            for (int l = 0; l < M; l++) {
                if (!visited[j][l] && g[0][k_x][k_y] && dis[k_x][k_y] + g[0][k_x][k_y] < dis[j][l]) {
                    dis[j][l] = dis[k_x][k_y] + g[0][k_x][k_y];
                    pq.emplace(dis[j][l], make_pair(start_x, start_y));
                }
            }
        }
        source++;
    }
    delays[source]  = dis;
}


// 计算发射源到铝块每个点的距离
void calculateDistanceMatrix(vector<vector<vector<double>>> &g) {
   // vector<vector<double>> g(N, vector<double>(M, 0));
    
    int mid_x = N / 2; // 发射源的x坐标

    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < M; ++j) {
            double distance = sqrt(pow(i - mid_x, 2) + pow(j, 2)); // 计算距离
            g[0][i][j] = distance / (speed_of_sound * 10); // 转换为时间，单位为微秒
        }
    }

    // 打印距离矩阵，用于验证
    /*for (int i = 0; i < N; ++i) {
        for (int j = 0; j < M; ++j) {
            cout << g[0][i][j] << " ";
        }
        cout << endl;
    }*/
}


bool saveExcel(vector<vector<vector<double>>> g)
{
    // 创建一个新的工作簿
    workbook wb;
    // 指定保存文件的固定路径
    string file_path = "D:\\MatrixData.xlsx";
    

    // 将矩阵数据写入工作表
    for (size_t i = 0; i < g.size(); ++i) {
        
        // 创建一个新的工作表，并命名（这里使用简单的命名方式，如"Sheet1", "Sheet2", ...）
        worksheet ws;
        if (i == 0) {
            ws = wb.active_sheet();


        }
        else
        {
            ws = wb.create_sheet(i);

        }
        

        

        for (size_t j = 0; j < g[i].size(); ++j) {
            for (size_t k = 0; k < g[i][j].size(); ++k)

            ws.cell(xlnt::cell_reference(j + 1, k + 1)).value(g[i][j][k]);
        }

       // 保存工作簿到文件
        try {
            wb.save(file_path);
            cout << "Matrix data has been saved to MatrixData.xlsx" << std::endl;
        }
        catch (const std::exception& e) {
            std::cerr << "Error saving file: " << e.what() << std::endl;
            return false;
        }
    }
    

   
    return true;
}

int main() {
    // 创建图
    int source_num = 1;
    int receive_num = 64;
    vector<vector<vector<double>>> g(2, vector<vector<double>>(N, vector<double>(M, 0)));
    cout << "开始" << endl;
    calculateDistanceMatrix(g); // 计算距离矩阵
    cout << "计算" << endl;
    saveExcel(g);
    // 对于每个阵元，计算到所有网格点的延迟时间
    for (int i = 0; i < 1; i++) {
        for (int j = 0; j < 1; j++) {
            int x = 400; // 将阵元间距转换为网格索引
            int y = 0;
            dijkstra(g, x, y);
        }
    }
    cout << "结果" << endl;
    // 打印延迟时间矩阵
  /*  for (int i = 0; i < 64; i++) {
        
            for (int k = 0; k < N; k++) {
                for (int l = 0; l < M; l++) {
                    cout << delays[i][k][l] << " ";
                }
                cout << endl;
            }
            cout << endl;
        
        cout << endl;
    }*/

    return 0;
}