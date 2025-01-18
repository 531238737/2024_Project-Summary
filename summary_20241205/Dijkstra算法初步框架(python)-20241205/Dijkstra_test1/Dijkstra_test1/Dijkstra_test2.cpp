#include <iostream>
#include <vector>
#include <queue>
#include <cmath>
#include <limits>

using namespace std;

void dijkstra(vector<vector<int>>& g, int start) {
    int n = g.size(); // ����ĸ���
    vector<int> dis(n, INT_MAX / 2); // ÿ���㵽��ʼ��ľ���
    dis[start] = 0; // ��ʼ�㵽�Լ���ֵ�� 0
    vector<bool> visited(n, false); // �����Щ���㱻���ʹ�
    // �����ѣ����ݵ���ʼ��ľ�������
    priority_queue<pair<int, int>, vector<pair<int, int>>, greater<>> pq;
    pq.emplace(0, start); // ��ʼ�㵽���Լ��ľ����� 0
    for (int i = 0; i < n; i++) {
        // ÿ�γ��Ӷ�������ʼ�������û����ǹ��Ķ���
        if (pq.empty()) break; // �����Ϊ�գ��˳�ѭ��
        int k = pq.top().second;
        pq.pop();
        visited[k] = true; // ���
        for (int j = 0; j < n; j++) { // ���Ĵ���
            // ���� j û�б���ǣ����� k �е� j ��·�����������·���������͸���
            if (!visited[j] && g[k][j] != 0 && dis[k] + g[k][j] < dis[j]) {
                // ������� j ���� k ����ʼ��ľ���������͸��¶��� j ����ʼ��ľ��룬��������ӵ�����
                dis[j] = dis[k] + g[k][j];
                pq.emplace(dis[j], j);
            }
        }
    }

    // ��ӡ����dis��ֵ������ʹ��
    for (int di : dis)
        cout << di << ",";
    cout << endl;
}



int  main() {
    vector<vector<int>>g= { {0, 1, 3, 0, 0, 0},// ͼ���ڽӾ���
            {0, 0, 1, 4, 2, 0},
            {0, 0, 0, 5, 5, 0},
            {0, 0, 0, 0, 0, 3},
            {0, 0, 0, 1, 0, 6},
            {0, 0, 0, 0, 0, 0} };
    dijkstra(g, 0);

    return 0;
}
