namespace IoTRobotWorldUDPServer
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label5 = new System.Windows.Forms.Label();
            this.UDPMessageTextBox = new System.Windows.Forms.TextBox();
            this.SendUDPMessageButton = new System.Windows.Forms.Button();
            this.ReportListBox = new System.Windows.Forms.ListBox();
            this.UDPRegularSenderTimer = new System.Windows.Forms.Timer(this.components);
            this.RegularUDPSendCheckBox = new System.Windows.Forms.CheckBox();
            this.AppendLFSymbolCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCell3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCell2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCell1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LocalPortTextBox = new System.Windows.Forms.TextBox();
            this.LocalIPTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RemotePortTextBox = new System.Windows.Forms.TextBox();
            this.RemoteIPTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StartStopUDPClientButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxPlatform3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPlatform2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.trafficLight = new System.Windows.Forms.PictureBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficLight)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 375);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Message";
            // 
            // UDPMessageTextBox
            // 
            this.UDPMessageTextBox.Location = new System.Drawing.Point(68, 372);
            this.UDPMessageTextBox.Name = "UDPMessageTextBox";
            this.UDPMessageTextBox.Size = new System.Drawing.Size(249, 20);
            this.UDPMessageTextBox.TabIndex = 9;
            this.UDPMessageTextBox.Text = "{\"N\":1, \"M\":0, \"F\":50, \"B\":10, \"T\":0}";
            // 
            // SendUDPMessageButton
            // 
            this.SendUDPMessageButton.Location = new System.Drawing.Point(407, 367);
            this.SendUDPMessageButton.Name = "SendUDPMessageButton";
            this.SendUDPMessageButton.Size = new System.Drawing.Size(75, 28);
            this.SendUDPMessageButton.TabIndex = 10;
            this.SendUDPMessageButton.Text = "Send";
            this.SendUDPMessageButton.UseVisualStyleBackColor = true;
            this.SendUDPMessageButton.Click += new System.EventHandler(this.SendUDPMessageButton_Click);
            // 
            // ReportListBox
            // 
            this.ReportListBox.FormattingEnabled = true;
            this.ReportListBox.HorizontalScrollbar = true;
            this.ReportListBox.Location = new System.Drawing.Point(408, 13);
            this.ReportListBox.Name = "ReportListBox";
            this.ReportListBox.ScrollAlwaysVisible = true;
            this.ReportListBox.Size = new System.Drawing.Size(393, 264);
            this.ReportListBox.TabIndex = 11;
            // 
            // UDPRegularSenderTimer
            // 
            this.UDPRegularSenderTimer.Interval = 1000;
            this.UDPRegularSenderTimer.Tick += new System.EventHandler(this.UDPRegularSenderTimer_Tick);
            // 
            // RegularUDPSendCheckBox
            // 
            this.RegularUDPSendCheckBox.AutoSize = true;
            this.RegularUDPSendCheckBox.Location = new System.Drawing.Point(488, 375);
            this.RegularUDPSendCheckBox.Name = "RegularUDPSendCheckBox";
            this.RegularUDPSendCheckBox.Size = new System.Drawing.Size(134, 17);
            this.RegularUDPSendCheckBox.TabIndex = 13;
            this.RegularUDPSendCheckBox.Text = "Send message regular ";
            this.RegularUDPSendCheckBox.UseVisualStyleBackColor = true;
            this.RegularUDPSendCheckBox.CheckedChanged += new System.EventHandler(this.RegularUDPSendCheckBox_CheckedChanged);
            // 
            // AppendLFSymbolCheckBox
            // 
            this.AppendLFSymbolCheckBox.AutoSize = true;
            this.AppendLFSymbolCheckBox.Checked = true;
            this.AppendLFSymbolCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AppendLFSymbolCheckBox.Location = new System.Drawing.Point(323, 374);
            this.AppendLFSymbolCheckBox.Name = "AppendLFSymbolCheckBox";
            this.AppendLFSymbolCheckBox.Size = new System.Drawing.Size(78, 17);
            this.AppendLFSymbolCheckBox.TabIndex = 14;
            this.AppendLFSymbolCheckBox.Text = "Append LF";
            this.AppendLFSymbolCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCell3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxCell2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxCell1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(7, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 106);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подставка 1";
            // 
            // textBoxCell3
            // 
            this.textBoxCell3.Location = new System.Drawing.Point(69, 74);
            this.textBoxCell3.Name = "textBoxCell3";
            this.textBoxCell3.Size = new System.Drawing.Size(104, 20);
            this.textBoxCell3.TabIndex = 5;
            this.textBoxCell3.Text = "З";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Ячейка 3";
            // 
            // textBoxCell2
            // 
            this.textBoxCell2.Location = new System.Drawing.Point(69, 48);
            this.textBoxCell2.Name = "textBoxCell2";
            this.textBoxCell2.Size = new System.Drawing.Size(104, 20);
            this.textBoxCell2.TabIndex = 3;
            this.textBoxCell2.Text = "С";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Ячейка 2";
            // 
            // textBoxCell1
            // 
            this.textBoxCell1.Location = new System.Drawing.Point(69, 22);
            this.textBoxCell1.Name = "textBoxCell1";
            this.textBoxCell1.Size = new System.Drawing.Size(104, 20);
            this.textBoxCell1.TabIndex = 1;
            this.textBoxCell1.Text = "С,С,К";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ячейка 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LocalPortTextBox);
            this.groupBox2.Controls.Add(this.LocalIPTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.RemotePortTextBox);
            this.groupBox2.Controls.Add(this.RemoteIPTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(7, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 90);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Подключение";
            // 
            // LocalPortTextBox
            // 
            this.LocalPortTextBox.Location = new System.Drawing.Point(312, 57);
            this.LocalPortTextBox.Name = "LocalPortTextBox";
            this.LocalPortTextBox.Size = new System.Drawing.Size(73, 20);
            this.LocalPortTextBox.TabIndex = 20;
            this.LocalPortTextBox.Text = "7777";
            // 
            // LocalIPTextBox
            // 
            this.LocalIPTextBox.Location = new System.Drawing.Point(312, 25);
            this.LocalIPTextBox.Name = "LocalIPTextBox";
            this.LocalIPTextBox.Size = new System.Drawing.Size(73, 20);
            this.LocalIPTextBox.TabIndex = 19;
            this.LocalIPTextBox.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Локальный порт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Локальный IP";
            // 
            // RemotePortTextBox
            // 
            this.RemotePortTextBox.Location = new System.Drawing.Point(108, 57);
            this.RemotePortTextBox.Name = "RemotePortTextBox";
            this.RemotePortTextBox.Size = new System.Drawing.Size(74, 20);
            this.RemotePortTextBox.TabIndex = 16;
            this.RemotePortTextBox.Text = "8888";
            // 
            // RemoteIPTextBox
            // 
            this.RemoteIPTextBox.Location = new System.Drawing.Point(108, 25);
            this.RemoteIPTextBox.Name = "RemoteIPTextBox";
            this.RemoteIPTextBox.Size = new System.Drawing.Size(74, 20);
            this.RemoteIPTextBox.TabIndex = 15;
            this.RemoteIPTextBox.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Удаленный порт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Удаленный IP";
            // 
            // StartStopUDPClientButton
            // 
            this.StartStopUDPClientButton.Location = new System.Drawing.Point(7, 221);
            this.StartStopUDPClientButton.Name = "StartStopUDPClientButton";
            this.StartStopUDPClientButton.Size = new System.Drawing.Size(91, 57);
            this.StartStopUDPClientButton.TabIndex = 21;
            this.StartStopUDPClientButton.Text = "Подключиться";
            this.StartStopUDPClientButton.UseVisualStyleBackColor = true;
            this.StartStopUDPClientButton.Click += new System.EventHandler(this.StartStopUDPClientButton_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(104, 221);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(85, 57);
            this.startButton.TabIndex = 23;
            this.startButton.Text = "Старт";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxStatus);
            this.groupBox3.Controls.Add(this.textBoxPlatform3);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBoxPlatform2);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.trafficLight);
            this.groupBox3.Location = new System.Drawing.Point(198, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 169);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статус";
            // 
            // textBoxPlatform3
            // 
            this.textBoxPlatform3.Enabled = false;
            this.textBoxPlatform3.Location = new System.Drawing.Point(83, 74);
            this.textBoxPlatform3.Name = "textBoxPlatform3";
            this.textBoxPlatform3.Size = new System.Drawing.Size(111, 20);
            this.textBoxPlatform3.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Подставка 3";
            // 
            // textBoxPlatform2
            // 
            this.textBoxPlatform2.Enabled = false;
            this.textBoxPlatform2.Location = new System.Drawing.Point(83, 47);
            this.textBoxPlatform2.Name = "textBoxPlatform2";
            this.textBoxPlatform2.Size = new System.Drawing.Size(111, 20);
            this.textBoxPlatform2.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Подставка 2";
            // 
            // trafficLight
            // 
            this.trafficLight.Location = new System.Drawing.Point(6, 106);
            this.trafficLight.Name = "trafficLight";
            this.trafficLight.Size = new System.Drawing.Size(188, 57);
            this.trafficLight.TabIndex = 29;
            this.trafficLight.TabStop = false;
            this.trafficLight.Paint += new System.Windows.Forms.PaintEventHandler(this.trafficLight_Paint);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Enabled = false;
            this.textBoxStatus.Location = new System.Drawing.Point(9, 21);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(185, 20);
            this.textBoxStatus.TabIndex = 34;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 292);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.StartStopUDPClientButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AppendLFSymbolCheckBox);
            this.Controls.Add(this.RegularUDPSendCheckBox);
            this.Controls.Add(this.ReportListBox);
            this.Controls.Add(this.SendUDPMessageButton);
            this.Controls.Add(this.UDPMessageTextBox);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "IoTRobotWorldUDPServer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trafficLight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UDPMessageTextBox;
        private System.Windows.Forms.Button SendUDPMessageButton;
        private System.Windows.Forms.ListBox ReportListBox;
        private System.Windows.Forms.Timer UDPRegularSenderTimer;
        private System.Windows.Forms.CheckBox RegularUDPSendCheckBox;
        private System.Windows.Forms.CheckBox AppendLFSymbolCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCell1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCell3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxCell2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox LocalPortTextBox;
        private System.Windows.Forms.TextBox LocalIPTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RemotePortTextBox;
        private System.Windows.Forms.TextBox RemoteIPTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartStopUDPClientButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.TextBox textBoxPlatform3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPlatform2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox trafficLight;
    }
}

