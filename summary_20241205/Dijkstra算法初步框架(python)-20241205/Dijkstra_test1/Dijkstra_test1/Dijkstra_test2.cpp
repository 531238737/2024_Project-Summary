#include <iostream>
#include <vector>
#include <queue>
#include <cmath>
#include <limits>

using namespace std;

void dijkstra(vector<vector<int>>& g, int start) {
    int n = g.size(); // 顶点的个数
    vector<int> dis(n, INT_MAX / 2); // 每个点到起始点的距离
    dis[start] = 0; // 起始点到自己的值是 0
    vector<bool> visited(n, false); // 标记哪些顶点被访问过
    // 创建堆，根据到起始点的距离排序
    priority_queue<pair<int, int>, vector<pair<int, int>>, greater<>> pq;
    pq.emplace(0, start); // 起始点到它自己的距离是 0
    for (int i = 0; i < n; i++) {
        // 每次出队都是离起始点最近且没被标记过的顶点
        if (pq.empty()) break; // 如果堆为空，退出循环
        int k = pq.top().second;
        pq.pop();
        visited[k] = true; // 标记
        for (int j = 0; j < n; j++) { // 核心代码
            // 顶点 j 没有被标记，并且 k 有到 j 的路径，并且这个路径更近，就更新
            if (!visited[j] && g[k][j] != 0 && dis[k] + g[k][j] < dis[j]) {
                // 如果顶点 j 经过 k 到起始点的距离更近，就更新顶点 j 到起始点的距离，并把它添加到堆中
                dis[j] = dis[k] + g[k][j];
                pq.emplace(dis[j], j);
            }
        }
    }

    // 打印数组dis的值，测试使用
    for (int di : dis)
        cout << di << ",";
    cout << endl;
}



int  main() {
    vector<vector<int>>g= { {0, 1, 3, 0, 0, 0},// 图的邻接矩阵。
            {0, 0, 1, 4, 2, 0},
            {0, 0, 0, 5, 5, 0},
            {0, 0, 0, 0, 0, 3},
            {0, 0, 0, 1, 0, 6},
            {0, 0, 0, 0, 0, 0} };
    dijkstra(g, 0);

    return 0;
}
