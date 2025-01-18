import os
import imageio
from PIL import Image
import numpy as np
frames = []#保存图片信息的文件夹
# for image_name in range(1, 6): # 读取image下的图片名称
#     image_name = "he_test\\"+ str(image_name )+".png" # 绝对路径
#
#     frames.append(imageio.imread(image_name))
#
# imageio.mimsave("./result.gif", frames, 'GIF', duration=2) # 保存在当前文件夹
# print("图片转换GIF完成")



for image_name in range(1, 6):  # 读取image下的图片名称
    image_path = "he_test\\" + str(image_name) + ".png"  # 绝对路径
    img = Image.open(image_path)  # 使用PIL打开图像
    img = img.resize((2251, 2073))  # 使用PIL调整图像大小
    img_array = np.array(img)  # 将PIL图像转换为NumPy数组
    frames.append(img_array)  # 将图像数组添加到frames列表

imageio.mimsave("./result.gif", frames, 'GIF', duration=2,loop=0)  # 保存在当前文件夹
print("图片转换GIF完成")