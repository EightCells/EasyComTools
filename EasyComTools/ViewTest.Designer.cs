namespace EasyComTools
{
    partial class ViewTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LBox_Tests = new System.Windows.Forms.ListBox();
            this.PB_ReadTests = new System.Windows.Forms.Button();
            this.PB_DeleteTest = new System.Windows.Forms.Button();
            this.PB_ResetView = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.CBL_LinesVis = new System.Windows.Forms.CheckedListBox();
            this.Plt = new ScottPlot.WinForms.FormsPlot();
            this.PB_DrawDif = new System.Windows.Forms.Button();
            this.PB_AvgFilter = new System.Windows.Forms.Button();
            this.PB_MidFilter = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Plt, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1064, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1058, 198);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MinimumSize = new System.Drawing.Size(350, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 195);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试列表";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.LBox_Tests, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.PB_ReadTests, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.PB_DeleteTest, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.PB_ResetView, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(344, 175);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // LBox_Tests
            // 
            this.LBox_Tests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBox_Tests.FormattingEnabled = true;
            this.LBox_Tests.ItemHeight = 12;
            this.LBox_Tests.Location = new System.Drawing.Point(3, 3);
            this.LBox_Tests.MinimumSize = new System.Drawing.Size(200, 4);
            this.LBox_Tests.Name = "LBox_Tests";
            this.tableLayoutPanel2.SetRowSpan(this.LBox_Tests, 5);
            this.LBox_Tests.Size = new System.Drawing.Size(214, 169);
            this.LBox_Tests.TabIndex = 0;
            this.LBox_Tests.SelectedIndexChanged += new System.EventHandler(this.LBox_Tests_SelectedIndexChanged);
            // 
            // PB_ReadTests
            // 
            this.PB_ReadTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_ReadTests.Location = new System.Drawing.Point(225, 5);
            this.PB_ReadTests.Margin = new System.Windows.Forms.Padding(5);
            this.PB_ReadTests.Name = "PB_ReadTests";
            this.PB_ReadTests.Size = new System.Drawing.Size(114, 30);
            this.PB_ReadTests.TabIndex = 5;
            this.PB_ReadTests.Text = "读取测试数据";
            this.PB_ReadTests.UseVisualStyleBackColor = true;
            this.PB_ReadTests.Click += new System.EventHandler(this.PB_ReadTests_Click);
            // 
            // PB_DeleteTest
            // 
            this.PB_DeleteTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_DeleteTest.Location = new System.Drawing.Point(225, 45);
            this.PB_DeleteTest.Margin = new System.Windows.Forms.Padding(5);
            this.PB_DeleteTest.Name = "PB_DeleteTest";
            this.PB_DeleteTest.Size = new System.Drawing.Size(114, 30);
            this.PB_DeleteTest.TabIndex = 6;
            this.PB_DeleteTest.Text = "删除测试数据";
            this.PB_DeleteTest.UseVisualStyleBackColor = true;
            this.PB_DeleteTest.Click += new System.EventHandler(this.PB_DeleteTest_Click);
            // 
            // PB_ResetView
            // 
            this.PB_ResetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_ResetView.Location = new System.Drawing.Point(225, 85);
            this.PB_ResetView.Margin = new System.Windows.Forms.Padding(5);
            this.PB_ResetView.Name = "PB_ResetView";
            this.PB_ResetView.Size = new System.Drawing.Size(114, 30);
            this.PB_ResetView.TabIndex = 3;
            this.PB_ResetView.Text = "重置视图";
            this.PB_ResetView.UseVisualStyleBackColor = true;
            this.PB_ResetView.Click += new System.EventHandler(this.PB_ResetView_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(359, 3);
            this.groupBox2.MinimumSize = new System.Drawing.Size(350, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 192);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据列表";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.PB_DrawDif, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.CBL_LinesVis, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.PB_MidFilter, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.PB_AvgFilter, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(344, 172);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // CBL_LinesVis
            // 
            this.CBL_LinesVis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBL_LinesVis.FormattingEnabled = true;
            this.CBL_LinesVis.Location = new System.Drawing.Point(3, 3);
            this.CBL_LinesVis.MinimumSize = new System.Drawing.Size(210, 4);
            this.CBL_LinesVis.Name = "CBL_LinesVis";
            this.tableLayoutPanel3.SetRowSpan(this.CBL_LinesVis, 5);
            this.CBL_LinesVis.Size = new System.Drawing.Size(214, 166);
            this.CBL_LinesVis.TabIndex = 0;
            this.CBL_LinesVis.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CBL_LinesVis_ItemCheck);
            // 
            // Plt
            // 
            this.Plt.AutoScroll = true;
            this.Plt.DisplayScale = 0F;
            this.Plt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plt.Location = new System.Drawing.Point(5, 209);
            this.Plt.Margin = new System.Windows.Forms.Padding(5);
            this.Plt.Name = "Plt";
            this.Plt.Size = new System.Drawing.Size(1054, 467);
            this.Plt.TabIndex = 0;
            // 
            // PB_DrawDif
            // 
            this.PB_DrawDif.Location = new System.Drawing.Point(225, 5);
            this.PB_DrawDif.Margin = new System.Windows.Forms.Padding(5);
            this.PB_DrawDif.Name = "PB_DrawDif";
            this.PB_DrawDif.Size = new System.Drawing.Size(114, 30);
            this.PB_DrawDif.TabIndex = 7;
            this.PB_DrawDif.Text = "绘制差分";
            this.PB_DrawDif.UseVisualStyleBackColor = true;
            this.PB_DrawDif.Click += new System.EventHandler(this.PB_DrawDif_Click);
            // 
            // PB_AvgFilter
            // 
            this.PB_AvgFilter.Location = new System.Drawing.Point(225, 45);
            this.PB_AvgFilter.Margin = new System.Windows.Forms.Padding(5);
            this.PB_AvgFilter.Name = "PB_AvgFilter";
            this.PB_AvgFilter.Size = new System.Drawing.Size(114, 30);
            this.PB_AvgFilter.TabIndex = 8;
            this.PB_AvgFilter.Text = "均值滤波";
            this.PB_AvgFilter.UseVisualStyleBackColor = true;
            this.PB_AvgFilter.Click += new System.EventHandler(this.PB_AvgFilter_Click);
            // 
            // PB_MidFilter
            // 
            this.PB_MidFilter.Location = new System.Drawing.Point(225, 85);
            this.PB_MidFilter.Margin = new System.Windows.Forms.Padding(5);
            this.PB_MidFilter.Name = "PB_MidFilter";
            this.PB_MidFilter.Size = new System.Drawing.Size(114, 30);
            this.PB_MidFilter.TabIndex = 9;
            this.PB_MidFilter.Text = "中值滤波";
            this.PB_MidFilter.UseVisualStyleBackColor = true;
            this.PB_MidFilter.Click += new System.EventHandler(this.PB_MidFilter_Click);
            // 
            // ViewTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "ViewTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read DataBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewTest_FormClosing);
            this.Load += new System.EventHandler(this.ViewTest_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ScottPlot.WinForms.FormsPlot Plt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox LBox_Tests;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox CBL_LinesVis;
        private System.Windows.Forms.Button PB_ReadTests;
        private System.Windows.Forms.Button PB_ResetView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button PB_DeleteTest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button PB_DrawDif;
        private System.Windows.Forms.Button PB_MidFilter;
        private System.Windows.Forms.Button PB_AvgFilter;
    }
}