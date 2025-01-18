using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACS.SPiiPlusNET;
using OfficeOpenXml;




using System.Collections;

using System.Runtime.InteropServices; //for COMException class
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace PEG_test1
{
    public partial class Form1 : Form
    {

        private Api Ch;//设备标识
        private AxisStates state_1, state_2, state_3;//xyz轴
        private MotorStates x_MotorState, y_MotorState, z_MotorState;//三轴的运动状态
        double axis_0,axis_1,axis_2;//三轴的位置信息
        double velocity_x, velocity_y, velocity_z;//三轴速度值
        double acceleration_x, acceleration_y, acceleration_z;//三轴加速度值
        double deceleration_x, deceleration_y, deceleration_z;//三轴减速值
        double KillDeceleration_x, KillDeceleration_y, KillDeceleration_z;//三个轴终止减速的值
        double Jeck_x, Jeck_y,Jeck_z;//三个轴急动度的值








        bool bConnected;//  For toggling connection between the Controller and the UI用于切换控制器和用户界面之间的连接
        int AxisNum;
        ACS.SPiiPlusNET.Axis Axis;//  For tracking user's axis
        System.Drawing.Image Grey;//灰色显示灯
        System.Drawing.Image Green;//绿色显示灯
        //声明全部轴
        Axis[] axes = { Axis.ACSC_AXIS_0, Axis.ACSC_AXIS_1, Axis.ACSC_AXIS_2, Axis.ACSC_NONE };
        // 初始化一个列表来存储坐标
        //List<(double x, double y, double z)> coordinates = new List<(double x, double y, double z)>();
        List<double[]> coordinates = new List<double[]>();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // 设置定时器属性
           //定时器
            timer1.Tick += timer1_Tick;




        }


        /// <summary>
        /// 启动窗体函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {
            
            // Type Channel is defined in Api Type Library
            Ch = new Api();
            



            //  Set communication state as false
            bConnected = false;
            //  Retrieve Color for Green and Grey
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
            
            
            Green = ((System.Drawing.Image)(resources.GetObject("GreenPB.Image")));
            
            Grey = ((System.Drawing.Image)(resources.GetObject("GreyPB.Image")));
            pictureBox_green.Image = Grey;
        }
        /// <summary>
        /// 连接设备函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void network_button_Click(object sender, EventArgs e)
        {
            try
            {
                if(network_button.Text=="Connect")//如果是未连接状态
                {
                    int Protocol = (int)EthernetCommOption.ACSC_SOCKET_STREAM_PORT;//选择网络连接端口

                    Ch.OpenCommEthernet(textBox1.Text, Protocol);//进行网络连接
                    bConnected = true;//连接完毕
                    timer1.Start();//启动定时器




                    pictureBox_green.Image = Green;
                    network_button.Text = "Disconnect";

                }
                else
                {
                    network_button.Text = "Connect";
                    bConnected = false;
                    pictureBox_green.Image = Grey;

                }
                

            }
            catch (COMException Ex)
            {
                ErorMsg(Ex);            //  Throw exception if this fails
            }
            catch (ACSException Ex)
            {
                ErorMsg(Ex);            //  Throw exception if this fails
            }

        }

        //  This function closes communication with Controller when "Disconnect" is pressed按下 "断开 "键时，该功能将关闭与控制器的通信。
        //private void DisconnectBtn_Click(object sender, System.EventArgs e)
        //{
        //    // if communication is open
        //    if (bConnected)
        //    {
        //        try
        //        {
        //            DoMonitorEvent.Reset();              // Pause monitor thread
        //            MotorStateThreadIdleEvent.WaitOne(); // Wait until thread is paused
        //            Ch.CloseComm();                      // Close current communication channel
        //            bConnected = false;                  // Turn off connection bit
        //            MakeDisconnectedState();             // Change Host application appearance					
        //        }
        //        catch (COMException Ex)
        //        {
        //            ErorMsg(Ex);
        //        }

        //    }

        //}

      


        /// <summary>
        /// 使能函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableBtn_Click(object sender, System.EventArgs e)
        {
            object pWait = 0;
            // if communication is open
            if (bConnected)
            {
                try
                {
                    // if motor is disabled
                    if (EnableBtn.Text == "Enable")
                    {
                        //Ch.EnableAsync(Axis); // 激活单个电机
                        
                        Ch.EnableM(axes);//使能所有轴
                        
                        EnableBtn.Text = "Disable";

                    }


                    else
                    {
                        timer1.Stop();//停止定时器
                        Ch.DisableAll();//关闭使能
                       // Ch.DisableAsync(Axis);  // Disable motor
                        EnableBtn.Text = "Enable";

                    }

                       
                }
                catch (COMException Ex)         //  Throw exception if this fails
                {
                    ErorMsg(Ex);
                }
            }
        }
        /// <summary>
        /// 选择轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AxesCmB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            AxisNum = AxesCmB.SelectedIndex;
            Axis = (ACS.SPiiPlusNET.Axis)AxisNum;
        }
        /// <summary>
        /// 开始运行函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBtn_Click(object sender, System.EventArgs e)
        {
            
            // if communication is open
            if (bConnected)//如果处于运行状态
            {
               
                try
                {
                    if (StartBtn.Text == "开始") //if moving is not executed 
                    {
                        StartBtn.Text = "停止";
                        // Start PTP motion of Axis, with relative (ACSC_AMF_RELATIVE) coordinate Increment to the target point
                        //开始轴的 PTP 运动，坐标增量为目标点的相对 (ACSC_AMF_RELATIVE) 坐标
                        double Increment = Convert.ToDouble(IncrementTB.Text);
                        //点动
                        Ch.ToPoint(
                            MotionFlags.ACSC_NONE,                          // '0' - Absolute position
                            (Axis)AxesCmB.SelectedIndex,  // Axis number
                            Increment                 // Target position
                            );
                        //Ch.ToPointAsync(MotionFlags.ACSC_AMF_RELATIVE, Axis, Increment);

                    }
                    else
                    {
                        StartBtn.Text = "开始";
                        // kill motion for Axis
                        Ch.KillAsync(Axis);
                    }

                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);            //  Throw exception if this fails
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        // 当出现异常时，该函数将发送错误信息
        private void ErorMsg(COMException Ex)
        {
            string Str = "Error from " + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
        }
        //当出现异常时，该函数将发送错误信息--与之前的 ACSException 相同
        private void ErorMsg(ACSException Ex)
        {
            string Str = "Error from " + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
        }
        /// <summary>
        /// 窗体关闭函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // if communication is open如果通信畅通
            if (true)
            {
                try
                {
                    // 弹出确认对话框
                    DialogResult result = MessageBox.Show("确定要关闭程序吗？", "确认关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // 如果用户点击“否”，取消关闭操作
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    if(bConnected)
                    {
                        Ch.DisableAll();

                    }
                   
                    Ch.CloseComm();         //  Close channel communication 	
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);            //  Throw exception if this fails
                }
            }
        }

        private void PEG_setBtn_Click(object sender, EventArgs e)
        {
            //peg配置
            Ch.AssignPegNT((ACS.SPiiPlusNET.Axis)0, 0x00000000, 0b000);
            Ch.AssignPegOutputsNT((ACS.SPiiPlusNET.Axis)0, 8, 0b0000);
            Ch.PegIncNT( 0, (ACS.SPiiPlusNET.Axis)0, 1, Convert.ToDouble(textBox_KaishiY.Text),
                Convert.ToDouble(textBox_JiangeY.Text), Convert.ToDouble(textBox_JieshuY.Text) , Api.ACSC_NONE, Api.ACSC_NONE);
            Ch.WaitPegReadyNT((ACS.SPiiPlusNET.Axis)0, 2000);
            Ch.StartPegNT(Axis.ACSC_AXIS_0);
           // Ch.ToPointAsync(MotionFlags.ACSC_AMF_RELATIVE, ACS.SPiiPlusNET.Axis.ACSC_AXIS_0, Convert.ToDouble(textBox_JieshuY.Text));
           
            

        }
        /// <summary>
        /// 定时器设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(bConnected)
                {
                    x_MotorState = Ch.GetMotorState(Axis.ACSC_AXIS_0);
                    y_MotorState = Ch.GetMotorState(Axis.ACSC_AXIS_1);
                    z_MotorState = Ch.GetMotorState(Axis.ACSC_AXIS_2);

                    state_1 = Ch.GetAxisState(Axis.ACSC_AXIS_0);
                    state_2 = Ch.GetAxisState(Axis.ACSC_AXIS_1);
                    state_3 = Ch.GetAxisState(Axis.ACSC_AXIS_2);

                    //获取三个轴的位置反馈
                    axis_0 = Ch.GetFPosition((Axis.ACSC_AXIS_0));
                    axis_1 = Ch.GetFPosition((Axis.ACSC_AXIS_1));
                    axis_2 = Ch.GetFPosition((Axis.ACSC_AXIS_2));
                    axis_0_pos.Text = String.Format("{0:0.0000}", axis_0);
                    axis_1_pos.Text = String.Format("{0:0.0000}", axis_1);
                    axis_2_pos.Text = String.Format("{0:0.0000}", axis_2);

                    //获取三个轴的速度设置
                    velocity_x = Ch.GetVelocity((Axis.ACSC_AXIS_0));
                    velocity_y = Ch.GetVelocity((Axis.ACSC_AXIS_1));
                    velocity_z = Ch.GetVelocity((Axis.ACSC_AXIS_2));
                    txtVel_x.Text = String.Format("{0:0.0000}", velocity_x);
                    txtVel_y.Text = String.Format("{0:0.0000}", velocity_y);
                    txtVel_z.Text = String.Format("{0:0.0000}", velocity_z);

                    
                    //获取三个轴的加速度设置
                    acceleration_x=Ch.GetAcceleration((Axis.ACSC_AXIS_0));
                    acceleration_y = Ch.GetAcceleration((Axis.ACSC_AXIS_1));
                    acceleration_z = Ch.GetAcceleration((Axis.ACSC_AXIS_2));
                    txtAcc_x.Text = String.Format("{0:0.0000}", acceleration_x);
                    txtAcc_y.Text = String.Format("{0:0.0000}", acceleration_y);
                    txtAcc_z.Text = String.Format("{0:0.0000}", acceleration_z);


                    //获取三个轴的减速度设置
                    deceleration_x=Ch.GetDeceleration((Axis.ACSC_AXIS_0));
                    deceleration_y = Ch.GetDeceleration((Axis.ACSC_AXIS_1));
                    deceleration_z = Ch.GetDeceleration((Axis.ACSC_AXIS_2));
                    txtDec_x.Text = String.Format("{0:0.0000}", deceleration_x);
                    txtDec_y.Text = String.Format("{0:0.0000}", deceleration_y);
                    txtDec_z.Text = String.Format("{0:0.0000}", deceleration_z);

                    //三个轴终止减速的值设定
                    KillDeceleration_x=Ch.GetKillDeceleration((Axis.ACSC_AXIS_0));
                    KillDeceleration_y = Ch.GetKillDeceleration((Axis.ACSC_AXIS_1));
                    KillDeceleration_z = Ch.GetKillDeceleration((Axis.ACSC_AXIS_2));
                    txtKdec_x.Text = String.Format("{0:0.0000}", KillDeceleration_x);
                    txtKdec_y.Text = String.Format("{0:0.0000}", KillDeceleration_y);
                    txtKdec_z.Text = String.Format("{0:0.0000}", KillDeceleration_z);


                    //三个轴急动度的值设定
                    Jeck_x=Ch.GetJerk(Axis.ACSC_AXIS_0);
                    Jeck_y = Ch.GetJerk(Axis.ACSC_AXIS_1);
                    Jeck_z = Ch.GetJerk(Axis.ACSC_AXIS_2);
                    txtJerk_x.Text = String.Format("{0:0.0000}", Jeck_x);
                    txtJerk_y.Text = String.Format("{0:0.0000}", Jeck_y);
                    txtJerk_z.Text = String.Format("{0:0.0000}", Jeck_z);

                }
                
            }
            catch (COMException Ex)
            {
                ErorMsg(Ex);            //  Throw exception if this fails
            }
            

        }

        string OpenFileName;//打开文件名字
        /// <summary>
        /// 打开文件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OpenFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenFileName = openFile.FileName;

                
                // 读取Excel文件
                using (var package = new ExcelPackage(new FileInfo(OpenFileName)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    coordinates.Clear();
                    // 读取XYZ坐标，假设从第二行开始，并且每一行代表一个坐标点
                    for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        
                        try
                        {
                            double x = worksheet.Cells[row, 1].Value != null ? Convert.ToDouble(worksheet.Cells[row, 1].Value) : 0;
                            double y = worksheet.Cells[row, 2].Value != null ? Convert.ToDouble(worksheet.Cells[row, 2].Value) : 0;
                            double z = worksheet.Cells[row, 3].Value != null ? Convert.ToDouble(worksheet.Cells[row, 3].Value) : 0;
                           
                            coordinates.Add(new double[] { x, y, z });
                        }
                        catch (FormatException)
                        {
                            // 处理格式异常，例如记录日志、跳过当前行等
                            // 这里我们简单地输出异常信息到控制台
                            MessageBox.Show($"Invalid data found and skipped at row {row}");
                           
                        }
                        catch (COMException Ex)
                        {
                            ErorMsg(Ex);            //  Throw exception if this fails
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 根据excel文件进行运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelStartbtn_Click(object sender, EventArgs e)
        {
          
            try
            {
                //获取列表的长度
                int listLength = coordinates.Count;
                int timeout = 20000;
                double[] points = { 0, 0, 0 };
                double velocity_x = 25;
               
                Ch.SetVelocity(Axis.ACSC_AXIS_0, velocity_x);
                Ch.SetVelocity(Axis.ACSC_AXIS_1, velocity_x);
                Ch.SetVelocity(Axis.ACSC_AXIS_2, velocity_x);


                // 循环读取每一行的 x、y、z 值
                Ch.MultiPointM(MotionFlags.ACSC_NONE, axes, 10);
                for (int i = 0; i < listLength; i++)
                {

                    points[0] = coordinates[i][0];
                    points[1] = coordinates[i][1];
                    points[2] = coordinates[i][2];

                    Ch.AddPointM(axes, points);


                }
                Ch.EndSequenceM(axes);
               





            }
            catch (ACSException Ex)
            {
                ErorMsg(Ex);
            }
            


        }


        /// <summary>
        /// 设置零点函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetZerobtn_Click(object sender, EventArgs e)
        {
            Ch.SetFPosition((Axis)AxesCmB.SelectedIndex, 0);
            
        }
        /// <summary>
        /// 设置点动运动函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTP_R_Neg_Click(object sender, EventArgs e)
        {
            double lfTargetPos = 0.0f;
            try
            {
                if (IncrementTB.Text.Length > 0)
                {
                    lfTargetPos = Convert.ToDouble(IncrementTB.Text);
                    if (lfTargetPos > 0) lfTargetPos = lfTargetPos * (-1);      // Target position (from current position, step move)

                    Ch.ToPoint(
                        MotionFlags.ACSC_AMF_RELATIVE,      // Flat
                        (Axis)AxesCmB.SelectedIndex,      // Axis number
                        lfTargetPos                         // Move distance
                        );
                }
            }
            catch (ACSException Ex)
            {
                ErorMsg(Ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 电动运动函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPTP_R_Pos_Click(object sender, EventArgs e)
        {
            double lfTargetPos = 0.0f;
            try
            {
                if (IncrementTB.Text.Length > 0)
                {
                    lfTargetPos = Convert.ToDouble(IncrementTB.Text);
                    if (lfTargetPos < 0) lfTargetPos = lfTargetPos * (-1);

                    Ch.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, (Axis)AxesCmB.SelectedIndex, lfTargetPos);
                }
            }
            catch (ACSException Ex)
            {
                ErorMsg(Ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        private void TextBoxes_Leave(object sender, EventArgs e)
        {
            try
            {
                double lfTemp = 0.0f;

                TextBox textBox = sender as TextBox;
                if (textBox == null) return;

                lfTemp = Convert.ToDouble(textBox.Text.Trim());
                switch (textBox.TabIndex)
                {
                    case 0: Ch.SetVelocityImm((Axis)AxesCmB.SelectedIndex, lfTemp); break;
                    case 1: Ch.SetAccelerationImm((Axis)AxesCmB.SelectedIndex, lfTemp); break;
                    case 2: Ch.SetDecelerationImm((Axis)AxesCmB.SelectedIndex, lfTemp); break;
                    case 3: Ch.SetKillDecelerationImm((Axis)AxesCmB.SelectedIndex, lfTemp); break;
                    case 4: Ch.SetJerkImm((Axis)AxesCmB.SelectedIndex, lfTemp); break;
                }

                textBox.SelectAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TextBoxes_Leave() Error\n" + ex.ToString());
            }
        }
    }
}
