namespace EasyComTools
{
    partial class MainForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CB_ComList = new System.Windows.Forms.ComboBox();
            this.PB_Connect = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.PB_StartATest = new System.Windows.Forms.Button();
            this.PB_Stop = new System.Windows.Forms.Button();
            this.Plot = new ScottPlot.WinForms.FormsPlot();
            this.TIM_ComListCheck = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.TIM_AddData = new System.Windows.Forms.Timer(this.components);
            this.TIM_PltReresh = new System.Windows.Forms.Timer(this.components);
            this.PB_ViewTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Plot, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1064, 681);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.CB_ComList);
            this.flowLayoutPanel1.Controls.Add(this.PB_Connect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1058, 34);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // CB_ComList
            // 
            this.CB_ComList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ComList.FormattingEnabled = true;
            this.CB_ComList.ItemHeight = 12;
            this.CB_ComList.Location = new System.Drawing.Point(5, 5);
            this.CB_ComList.Margin = new System.Windows.Forms.Padding(5);
            this.CB_ComList.MinimumSize = new System.Drawing.Size(150, 0);
            this.CB_ComList.Name = "CB_ComList";
            this.CB_ComList.Size = new System.Drawing.Size(150, 20);
            this.CB_ComList.TabIndex = 0;
            // 
            // PB_Connect
            // 
            this.PB_Connect.Location = new System.Drawing.Point(165, 5);
            this.PB_Connect.Margin = new System.Windows.Forms.Padding(5);
            this.PB_Connect.MinimumSize = new System.Drawing.Size(100, 25);
            this.PB_Connect.Name = "PB_Connect";
            this.PB_Connect.Size = new System.Drawing.Size(100, 25);
            this.PB_Connect.TabIndex = 1;
            this.PB_Connect.Text = "连接";
            this.PB_Connect.UseVisualStyleBackColor = true;
            this.PB_Connect.Click += new System.EventHandler(this.PB_Connect_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.PB_StartATest);
            this.flowLayoutPanel2.Controls.Add(this.PB_Stop);
            this.flowLayoutPanel2.Controls.Add(this.PB_ViewTest);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 43);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1058, 58);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // PB_StartATest
            // 
            this.PB_StartATest.Location = new System.Drawing.Point(5, 5);
            this.PB_StartATest.Margin = new System.Windows.Forms.Padding(5);
            this.PB_StartATest.MinimumSize = new System.Drawing.Size(100, 30);
            this.PB_StartATest.Name = "PB_StartATest";
            this.PB_StartATest.Size = new System.Drawing.Size(100, 30);
            this.PB_StartATest.TabIndex = 2;
            this.PB_StartATest.Text = "开始测试";
            this.PB_StartATest.UseVisualStyleBackColor = true;
            this.PB_StartATest.Click += new System.EventHandler(this.PB_StartATest_Click);
            // 
            // PB_Stop
            // 
            this.PB_Stop.Location = new System.Drawing.Point(115, 5);
            this.PB_Stop.Margin = new System.Windows.Forms.Padding(5);
            this.PB_Stop.MinimumSize = new System.Drawing.Size(100, 30);
            this.PB_Stop.Name = "PB_Stop";
            this.PB_Stop.Size = new System.Drawing.Size(100, 30);
            this.PB_Stop.TabIndex = 1;
            this.PB_Stop.Text = "暂停";
            this.PB_Stop.UseVisualStyleBackColor = true;
            this.PB_Stop.Click += new System.EventHandler(this.PB_Stop_Click);
            // 
            // Plot
            // 
            this.Plot.DisplayScale = 0F;
            this.Plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plot.Location = new System.Drawing.Point(3, 107);
            this.Plot.Name = "Plot";
            this.Plot.Size = new System.Drawing.Size(1058, 571);
            this.Plot.TabIndex = 3;
            // 
            // TIM_ComListCheck
            // 
            this.TIM_ComListCheck.Enabled = true;
            this.TIM_ComListCheck.Tick += new System.EventHandler(this.TIM_ComListCheck_Tick);
            // 
            // TIM_AddData
            // 
            this.TIM_AddData.Interval = 50;
            this.TIM_AddData.Tick += new System.EventHandler(this.TIM_AddData_Tick);
            // 
            // TIM_PltReresh
            // 
            this.TIM_PltReresh.Tick += new System.EventHandler(this.TIM_PltReresh_Tick);
            // 
            // PB_ViewTest
            // 
            this.PB_ViewTest.Location = new System.Drawing.Point(225, 5);
            this.PB_ViewTest.Margin = new System.Windows.Forms.Padding(5);
            this.PB_ViewTest.MinimumSize = new System.Drawing.Size(100, 30);
            this.PB_ViewTest.Name = "PB_ViewTest";
            this.PB_ViewTest.Size = new System.Drawing.Size(100, 30);
            this.PB_ViewTest.TabIndex = 3;
            this.PB_ViewTest.Text = "查看历史测试";
            this.PB_ViewTest.UseVisualStyleBackColor = true;
            this.PB_ViewTest.Click += new System.EventHandler(this.PB_ViewTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EasyComTools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox CB_ComList;
        private System.Windows.Forms.Button PB_Connect;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button PB_StartATest;
        private System.Windows.Forms.Button PB_Stop;
        private ScottPlot.WinForms.FormsPlot Plot;
        private System.Windows.Forms.Timer TIM_ComListCheck;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer TIM_AddData;
        private System.Windows.Forms.Timer TIM_PltReresh;
        private System.Windows.Forms.Button PB_ViewTest;
    }
}

