from moviepy.editor import VideoFileClip


def convert_mp4_to_gif(input_path, output_path, fps=60):
    # 加载 MP4 视频文件
    clip = VideoFileClip(input_path)

    # 获取视频的原始宽度和高度
    width, height = clip.size

    # 设置 GIF 的帧率，保持原始尺寸
    clip = clip.set_fps(fps)

    # 写入 GIF 文件
    clip.write_gif(output_path, fps=fps)


# 使用函数进行转换
convert_mp4_to_gif('test.mp4', 'mp4GIF.gif')

# 指定要处理的视频文件路径

input_video_path = 'test.mp4'

output_video_path = 'tesult.mp4'

# 加载视频文件

video = VideoFileClip(input_video_path)



video_no_audio = video.without_audio()

# 将处理后的视频文件保存到指定路径

video_no_audio.write_videofile(output_video_path, codec='libx264', audio_codec='aac')