# 2024_Project-Summary
将2024年有关所参与编写的所有的代码成果整理为一个项目。  
代码整理完毕共包含如下几个文件夹：  
![explorer_ZeK3xJoN59](https://github.com/user-attachments/assets/31ede535-6775-4b09-a4f3-2318127c5248)
每个文件夹分别对应不同项目。打开对应文件夹按照里面的说明文档步骤执行程序即可，可能会遇到报错，原因为没有下载程序对应的支持库。
## CUDA计算程序(C++)  
CUDA计算项目共包含两个文件夹：  
![image](https://github.com/user-attachments/assets/0d399753-2e24-466a-8aea-f19b733b033e)
第一个文件夹《程序版本》包含有编写的各个版本的CUDA图形计算程序。每个项目的框架如下所示：  
![image](https://github.com/user-attachments/assets/b541efb3-1d67-468d-a5f7-f24cf255f95f)
运行时点击sln文件即可，打开程序后各版本的程序文件框架大体相同，一般由4个执行文件构成：
![image](https://github.com/user-attachments/assets/ad38fd3b-8f40-4f26-9ccc-2677ac3c8b59)
其中“paraeters_all.h”包含有程序所要执行的所有参数声明，“paraeters_all.cpp”包含有所有声明参数对应的具体参数设置。“main.cpp”是整个项目的启动文件，“main_all.cu”是主要的算法设置文件，通过读取设置参数以及数据来进行计算。  
第二个文件夹《数据》则包含有每个版本程序执行所需要的数据，想要运行程序则需要按照程序中所需要的数据名称去《数据》文件中寻找对应的数据导入对应位置中即可，例子如下：
![image](https://github.com/user-attachments/assets/c1c160cb-4574-479a-88c4-55f93ef57f5c)
## 长形扫描运动平台控制代码(C#)
长形运行平台程序文件夹共包含如下文件：  
![image](https://github.com/user-attachments/assets/3b4e773c-6c47-4b29-94ae-0f0128c99680)
  
点击sln文件即可执行程序。
