namespace PEG_test1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.network_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_green = new System.Windows.Forms.PictureBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.IncrementTB = new System.Windows.Forms.TextBox();
            this.AxesCmB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EnableBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPTP_R_Pos = new System.Windows.Forms.Button();
            this.btnPTP_R_Neg = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PEG_setBtn = new System.Windows.Forms.Button();
            this.label_jieshu_y = new System.Windows.Forms.Label();
            this.label_jiange_y = new System.Windows.Forms.Label();
            this.label_kaishi_y = new System.Windows.Forms.Label();
            this.textBox_JieshuY = new System.Windows.Forms.TextBox();
            this.textBox_JiangeY = new System.Windows.Forms.TextBox();
            this.textBox_KaishiY = new System.Windows.Forms.TextBox();
            this.axis_0_pos = new System.Windows.Forms.TextBox();
            this.axis_1_pos = new System.Windows.Forms.TextBox();
            this.axis_2_pos = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ExcelStartbtn = new System.Windows.Forms.Button();
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.SetZerobtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtJerk_z = new System.Windows.Forms.TextBox();
            this.txtKdec_z = new System.Windows.Forms.TextBox();
            this.txtDec_z = new System.Windows.Forms.TextBox();
            this.txtAcc_z = new System.Windows.Forms.TextBox();
            this.txtVel_z = new System.Windows.Forms.TextBox();
            this.txtJerk_y = new System.Windows.Forms.TextBox();
            this.txtKdec_y = new System.Windows.Forms.TextBox();
            this.txtDec_y = new System.Windows.Forms.TextBox();
            this.txtAcc_y = new System.Windows.Forms.TextBox();
            this.txtVel_y = new System.Windows.Forms.TextBox();
            this.txtJerk_x = new System.Windows.Forms.TextBox();
            this.txtKdec_x = new System.Windows.Forms.TextBox();
            this.txtDec_x = new System.Windows.Forms.TextBox();
            this.txtAcc_x = new System.Windows.Forms.TextBox();
            this.txtVel_x = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_green)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // network_button
            // 
            this.network_button.Location = new System.Drawing.Point(406, 26);
            this.network_button.Name = "network_button";
            this.network_button.Size = new System.Drawing.Size(212, 53);
            this.network_button.TabIndex = 0;
            this.network_button.Text = "Connect";
            this.network_button.UseVisualStyleBackColor = true;
            this.network_button.Click += new System.EventHandler(this.network_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(179, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(183, 31);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "10.0.0.100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP_address";
            // 
            // pictureBox_green
            // 
            this.pictureBox_green.Location = new System.Drawing.Point(665, 26);
            this.pictureBox_green.Name = "pictureBox_green";
            this.pictureBox_green.Size = new System.Drawing.Size(72, 63);
            this.pictureBox_green.TabIndex = 3;
            this.pictureBox_green.TabStop = false;
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(482, 126);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(212, 53);
            this.StartBtn.TabIndex = 4;
            this.StartBtn.Text = "开始";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // IncrementTB
            // 
            this.IncrementTB.Location = new System.Drawing.Point(237, 139);
            this.IncrementTB.Name = "IncrementTB";
            this.IncrementTB.Size = new System.Drawing.Size(183, 31);
            this.IncrementTB.TabIndex = 5;
            // 
            // AxesCmB
            // 
            this.AxesCmB.FormattingEnabled = true;
            this.AxesCmB.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z",
            "T",
            "A",
            "B",
            "C",
            "D"});
            this.AxesCmB.Location = new System.Drawing.Point(252, 85);
            this.AxesCmB.Name = "AxesCmB";
            this.AxesCmB.Size = new System.Drawing.Size(121, 29);
            this.AxesCmB.TabIndex = 6;
            this.AxesCmB.SelectedIndexChanged += new System.EventHandler(this.AxesCmB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Axes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "IncrementBT";
            // 
            // EnableBtn
            // 
            this.EnableBtn.Location = new System.Drawing.Point(809, 36);
            this.EnableBtn.Name = "EnableBtn";
            this.EnableBtn.Size = new System.Drawing.Size(212, 53);
            this.EnableBtn.TabIndex = 9;
            this.EnableBtn.Text = "Enable";
            this.EnableBtn.UseVisualStyleBackColor = true;
            this.EnableBtn.Click += new System.EventHandler(this.EnableBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPTP_R_Pos);
            this.panel1.Controls.Add(this.btnPTP_R_Neg);
            this.panel1.Controls.Add(this.AxesCmB);
            this.panel1.Controls.Add(this.StartBtn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.IncrementTB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(895, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 263);
            this.panel1.TabIndex = 10;
            // 
            // btnPTP_R_Pos
            // 
            this.btnPTP_R_Pos.Location = new System.Drawing.Point(461, 198);
            this.btnPTP_R_Pos.Name = "btnPTP_R_Pos";
            this.btnPTP_R_Pos.Size = new System.Drawing.Size(249, 43);
            this.btnPTP_R_Pos.TabIndex = 21;
            this.btnPTP_R_Pos.Text = "Relative Pos. (+)";
            this.btnPTP_R_Pos.UseVisualStyleBackColor = true;
            this.btnPTP_R_Pos.Click += new System.EventHandler(this.btnPTP_R_Pos_Click);
            // 
            // btnPTP_R_Neg
            // 
            this.btnPTP_R_Neg.Location = new System.Drawing.Point(192, 186);
            this.btnPTP_R_Neg.Name = "btnPTP_R_Neg";
            this.btnPTP_R_Neg.Size = new System.Drawing.Size(248, 50);
            this.btnPTP_R_Neg.TabIndex = 20;
            this.btnPTP_R_Neg.Text = "Relative Pos. (-)";
            this.btnPTP_R_Neg.UseVisualStyleBackColor = true;
            this.btnPTP_R_Neg.Click += new System.EventHandler(this.btnPTP_R_Neg_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PEG_setBtn);
            this.panel2.Controls.Add(this.label_jieshu_y);
            this.panel2.Controls.Add(this.label_jiange_y);
            this.panel2.Controls.Add(this.label_kaishi_y);
            this.panel2.Controls.Add(this.textBox_JieshuY);
            this.panel2.Controls.Add(this.textBox_JiangeY);
            this.panel2.Controls.Add(this.textBox_KaishiY);
            this.panel2.Location = new System.Drawing.Point(60, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 262);
            this.panel2.TabIndex = 11;
            // 
            // PEG_setBtn
            // 
            this.PEG_setBtn.Location = new System.Drawing.Point(68, 114);
            this.PEG_setBtn.Name = "PEG_setBtn";
            this.PEG_setBtn.Size = new System.Drawing.Size(212, 53);
            this.PEG_setBtn.TabIndex = 12;
            this.PEG_setBtn.Text = "PEG设置";
            this.PEG_setBtn.UseVisualStyleBackColor = true;
            this.PEG_setBtn.Click += new System.EventHandler(this.PEG_setBtn_Click);
            // 
            // label_jieshu_y
            // 
            this.label_jieshu_y.AutoSize = true;
            this.label_jieshu_y.Location = new System.Drawing.Point(310, 201);
            this.label_jieshu_y.Name = "label_jieshu_y";
            this.label_jieshu_y.Size = new System.Drawing.Size(169, 21);
            this.label_jieshu_y.TabIndex = 11;
            this.label_jieshu_y.Text = "Y结束位置（mm）";
            // 
            // label_jiange_y
            // 
            this.label_jiange_y.AutoSize = true;
            this.label_jiange_y.Location = new System.Drawing.Point(321, 130);
            this.label_jiange_y.Name = "label_jiange_y";
            this.label_jiange_y.Size = new System.Drawing.Size(127, 21);
            this.label_jiange_y.TabIndex = 10;
            this.label_jiange_y.Text = "Y间隔（mm）";
            // 
            // label_kaishi_y
            // 
            this.label_kaishi_y.AutoSize = true;
            this.label_kaishi_y.Location = new System.Drawing.Point(310, 62);
            this.label_kaishi_y.Name = "label_kaishi_y";
            this.label_kaishi_y.Size = new System.Drawing.Size(169, 21);
            this.label_kaishi_y.TabIndex = 9;
            this.label_kaishi_y.Text = "Y开始位置（mm）";
            // 
            // textBox_JieshuY
            // 
            this.textBox_JieshuY.Location = new System.Drawing.Point(518, 198);
            this.textBox_JieshuY.Name = "textBox_JieshuY";
            this.textBox_JieshuY.Size = new System.Drawing.Size(183, 31);
            this.textBox_JieshuY.TabIndex = 8;
            // 
            // textBox_JiangeY
            // 
            this.textBox_JiangeY.Location = new System.Drawing.Point(518, 127);
            this.textBox_JiangeY.Name = "textBox_JiangeY";
            this.textBox_JiangeY.Size = new System.Drawing.Size(183, 31);
            this.textBox_JiangeY.TabIndex = 7;
            // 
            // textBox_KaishiY
            // 
            this.textBox_KaishiY.Location = new System.Drawing.Point(518, 52);
            this.textBox_KaishiY.Name = "textBox_KaishiY";
            this.textBox_KaishiY.Size = new System.Drawing.Size(183, 31);
            this.textBox_KaishiY.TabIndex = 6;
            // 
            // axis_0_pos
            // 
            this.axis_0_pos.Location = new System.Drawing.Point(271, 139);
            this.axis_0_pos.Name = "axis_0_pos";
            this.axis_0_pos.Size = new System.Drawing.Size(183, 31);
            this.axis_0_pos.TabIndex = 12;
            // 
            // axis_1_pos
            // 
            this.axis_1_pos.Location = new System.Drawing.Point(271, 231);
            this.axis_1_pos.Name = "axis_1_pos";
            this.axis_1_pos.Size = new System.Drawing.Size(183, 31);
            this.axis_1_pos.TabIndex = 13;
            // 
            // axis_2_pos
            // 
            this.axis_2_pos.Location = new System.Drawing.Point(271, 322);
            this.axis_2_pos.Name = "axis_2_pos";
            this.axis_2_pos.Size = new System.Drawing.Size(183, 31);
            this.axis_2_pos.TabIndex = 14;
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 21);
            this.label4.TabIndex = 15;
            this.label4.Text = "X轴位置（mm）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 21);
            this.label5.TabIndex = 16;
            this.label5.Text = "Y轴位置（mm）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 21);
            this.label6.TabIndex = 17;
            this.label6.Text = "Z轴位置（mm）";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ExcelStartbtn);
            this.panel3.Controls.Add(this.button_OpenFile);
            this.panel3.Location = new System.Drawing.Point(1488, 146);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(248, 177);
            this.panel3.TabIndex = 18;
            // 
            // ExcelStartbtn
            // 
            this.ExcelStartbtn.Location = new System.Drawing.Point(16, 97);
            this.ExcelStartbtn.Name = "ExcelStartbtn";
            this.ExcelStartbtn.Size = new System.Drawing.Size(212, 53);
            this.ExcelStartbtn.TabIndex = 11;
            this.ExcelStartbtn.Text = "连续轨迹运动";
            this.ExcelStartbtn.UseVisualStyleBackColor = true;
            this.ExcelStartbtn.Click += new System.EventHandler(this.ExcelStartbtn_Click);
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Location = new System.Drawing.Point(16, 23);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(212, 53);
            this.button_OpenFile.TabIndex = 10;
            this.button_OpenFile.Text = "读取文件";
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // SetZerobtn
            // 
            this.SetZerobtn.Location = new System.Drawing.Point(1087, 39);
            this.SetZerobtn.Name = "SetZerobtn";
            this.SetZerobtn.Size = new System.Drawing.Size(212, 53);
            this.SetZerobtn.TabIndex = 19;
            this.SetZerobtn.Text = "设置零点";
            this.SetZerobtn.UseVisualStyleBackColor = true;
            this.SetZerobtn.Click += new System.EventHandler(this.SetZerobtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtJerk_z);
            this.panel4.Controls.Add(this.txtKdec_z);
            this.panel4.Controls.Add(this.txtDec_z);
            this.panel4.Controls.Add(this.txtAcc_z);
            this.panel4.Controls.Add(this.txtVel_z);
            this.panel4.Controls.Add(this.txtJerk_y);
            this.panel4.Controls.Add(this.txtKdec_y);
            this.panel4.Controls.Add(this.txtDec_y);
            this.panel4.Controls.Add(this.txtAcc_y);
            this.panel4.Controls.Add(this.txtVel_y);
            this.panel4.Controls.Add(this.txtJerk_x);
            this.panel4.Controls.Add(this.txtKdec_x);
            this.panel4.Controls.Add(this.txtDec_x);
            this.panel4.Controls.Add(this.txtAcc_x);
            this.panel4.Controls.Add(this.txtVel_x);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(509, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(864, 335);
            this.panel4.TabIndex = 40;
            // 
            // txtJerk_z
            // 
            this.txtJerk_z.Enabled = false;
            this.txtJerk_z.Location = new System.Drawing.Point(624, 263);
            this.txtJerk_z.Name = "txtJerk_z";
            this.txtJerk_z.Size = new System.Drawing.Size(101, 31);
            this.txtJerk_z.TabIndex = 59;
            this.txtJerk_z.Text = "0";
            this.txtJerk_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKdec_z
            // 
            this.txtKdec_z.Enabled = false;
            this.txtKdec_z.Location = new System.Drawing.Point(624, 208);
            this.txtKdec_z.Name = "txtKdec_z";
            this.txtKdec_z.Size = new System.Drawing.Size(101, 31);
            this.txtKdec_z.TabIndex = 58;
            this.txtKdec_z.Text = "0";
            this.txtKdec_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDec_z
            // 
            this.txtDec_z.Enabled = false;
            this.txtDec_z.Location = new System.Drawing.Point(624, 149);
            this.txtDec_z.Name = "txtDec_z";
            this.txtDec_z.Size = new System.Drawing.Size(101, 31);
            this.txtDec_z.TabIndex = 57;
            this.txtDec_z.Text = "0";
            this.txtDec_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAcc_z
            // 
            this.txtAcc_z.Enabled = false;
            this.txtAcc_z.Location = new System.Drawing.Point(624, 75);
            this.txtAcc_z.Name = "txtAcc_z";
            this.txtAcc_z.Size = new System.Drawing.Size(101, 31);
            this.txtAcc_z.TabIndex = 56;
            this.txtAcc_z.Text = "0";
            this.txtAcc_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVel_z
            // 
            this.txtVel_z.Enabled = false;
            this.txtVel_z.Location = new System.Drawing.Point(624, 27);
            this.txtVel_z.Name = "txtVel_z";
            this.txtVel_z.Size = new System.Drawing.Size(101, 31);
            this.txtVel_z.TabIndex = 55;
            this.txtVel_z.Text = "0";
            this.txtVel_z.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJerk_y
            // 
            this.txtJerk_y.Enabled = false;
            this.txtJerk_y.Location = new System.Drawing.Point(475, 263);
            this.txtJerk_y.Name = "txtJerk_y";
            this.txtJerk_y.Size = new System.Drawing.Size(101, 31);
            this.txtJerk_y.TabIndex = 54;
            this.txtJerk_y.Text = "0";
            this.txtJerk_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKdec_y
            // 
            this.txtKdec_y.Enabled = false;
            this.txtKdec_y.Location = new System.Drawing.Point(475, 208);
            this.txtKdec_y.Name = "txtKdec_y";
            this.txtKdec_y.Size = new System.Drawing.Size(101, 31);
            this.txtKdec_y.TabIndex = 53;
            this.txtKdec_y.Text = "0";
            this.txtKdec_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDec_y
            // 
            this.txtDec_y.Enabled = false;
            this.txtDec_y.Location = new System.Drawing.Point(475, 149);
            this.txtDec_y.Name = "txtDec_y";
            this.txtDec_y.Size = new System.Drawing.Size(101, 31);
            this.txtDec_y.TabIndex = 52;
            this.txtDec_y.Text = "0";
            this.txtDec_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAcc_y
            // 
            this.txtAcc_y.Enabled = false;
            this.txtAcc_y.Location = new System.Drawing.Point(475, 75);
            this.txtAcc_y.Name = "txtAcc_y";
            this.txtAcc_y.Size = new System.Drawing.Size(101, 31);
            this.txtAcc_y.TabIndex = 51;
            this.txtAcc_y.Text = "0";
            this.txtAcc_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVel_y
            // 
            this.txtVel_y.Enabled = false;
            this.txtVel_y.Location = new System.Drawing.Point(475, 27);
            this.txtVel_y.Name = "txtVel_y";
            this.txtVel_y.Size = new System.Drawing.Size(101, 31);
            this.txtVel_y.TabIndex = 50;
            this.txtVel_y.Text = "0";
            this.txtVel_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJerk_x
            // 
            this.txtJerk_x.Enabled = false;
            this.txtJerk_x.Location = new System.Drawing.Point(314, 260);
            this.txtJerk_x.Name = "txtJerk_x";
            this.txtJerk_x.Size = new System.Drawing.Size(101, 31);
            this.txtJerk_x.TabIndex = 49;
            this.txtJerk_x.Text = "0";
            this.txtJerk_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKdec_x
            // 
            this.txtKdec_x.Enabled = false;
            this.txtKdec_x.Location = new System.Drawing.Point(314, 205);
            this.txtKdec_x.Name = "txtKdec_x";
            this.txtKdec_x.Size = new System.Drawing.Size(101, 31);
            this.txtKdec_x.TabIndex = 48;
            this.txtKdec_x.Text = "0";
            this.txtKdec_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDec_x
            // 
            this.txtDec_x.Enabled = false;
            this.txtDec_x.Location = new System.Drawing.Point(314, 146);
            this.txtDec_x.Name = "txtDec_x";
            this.txtDec_x.Size = new System.Drawing.Size(101, 31);
            this.txtDec_x.TabIndex = 47;
            this.txtDec_x.Text = "0";
            this.txtDec_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAcc_x
            // 
            this.txtAcc_x.Enabled = false;
            this.txtAcc_x.Location = new System.Drawing.Point(314, 72);
            this.txtAcc_x.Name = "txtAcc_x";
            this.txtAcc_x.Size = new System.Drawing.Size(101, 31);
            this.txtAcc_x.TabIndex = 46;
            this.txtAcc_x.Text = "0";
            this.txtAcc_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVel_x
            // 
            this.txtVel_x.Enabled = false;
            this.txtVel_x.Location = new System.Drawing.Point(314, 24);
            this.txtVel_x.Name = "txtVel_x";
            this.txtVel_x.Size = new System.Drawing.Size(101, 31);
            this.txtVel_x.TabIndex = 45;
            this.txtVel_x.Text = "0";
            this.txtVel_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(58, 270);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 21);
            this.label11.TabIndex = 44;
            this.label11.Text = "Jerk (JERK)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 210);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(274, 21);
            this.label10.TabIndex = 43;
            this.label10.Text = "Kill Deceleration (KDEC)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(208, 21);
            this.label9.TabIndex = 42;
            this.label9.Text = "Deceleration (DEC)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 21);
            this.label8.TabIndex = 41;
            this.label8.Text = "Acceleration (ACC)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 21);
            this.label7.TabIndex = 40;
            this.label7.Text = "Velocity (VEL)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 965);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.SetZerobtn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.axis_2_pos);
            this.Controls.Add(this.axis_1_pos);
            this.Controls.Add(this.axis_0_pos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EnableBtn);
            this.Controls.Add(this.pictureBox_green);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.network_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_green)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button network_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_green;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox IncrementTB;
        private System.Windows.Forms.ComboBox AxesCmB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button EnableBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_JieshuY;
        private System.Windows.Forms.TextBox textBox_JiangeY;
        private System.Windows.Forms.TextBox textBox_KaishiY;
        private System.Windows.Forms.Label label_jieshu_y;
        private System.Windows.Forms.Label label_jiange_y;
        private System.Windows.Forms.Label label_kaishi_y;
        private System.Windows.Forms.Button PEG_setBtn;
        private System.Windows.Forms.TextBox axis_0_pos;
        private System.Windows.Forms.TextBox axis_1_pos;
        private System.Windows.Forms.TextBox axis_2_pos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_OpenFile;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button ExcelStartbtn;
        private System.Windows.Forms.Button SetZerobtn;
        private System.Windows.Forms.Button btnPTP_R_Neg;
        private System.Windows.Forms.Button btnPTP_R_Pos;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtJerk_z;
        private System.Windows.Forms.TextBox txtKdec_z;
        private System.Windows.Forms.TextBox txtDec_z;
        private System.Windows.Forms.TextBox txtAcc_z;
        private System.Windows.Forms.TextBox txtVel_z;
        private System.Windows.Forms.TextBox txtJerk_y;
        private System.Windows.Forms.TextBox txtKdec_y;
        private System.Windows.Forms.TextBox txtDec_y;
        private System.Windows.Forms.TextBox txtAcc_y;
        private System.Windows.Forms.TextBox txtVel_y;
        private System.Windows.Forms.TextBox txtJerk_x;
        private System.Windows.Forms.TextBox txtKdec_x;
        private System.Windows.Forms.TextBox txtDec_x;
        private System.Windows.Forms.TextBox txtAcc_x;
        private System.Windows.Forms.TextBox txtVel_x;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}

