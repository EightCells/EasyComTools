using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Panels;
using ScottPlot.Plottables;

namespace EasyComTools
{
    public partial class ViewTest : Form
    {
        #region signal信息类
        private class ExFuncSignalXY
        {
            public ColPoints Points { get; }
            public SignalXY Signal { get; }
            public Crosshair Crosshair { get; }
            public bool IsVisible { get; set; }

            public ExFuncSignalXY(ColPoints colPoints,SignalXY signal, Crosshair crosshair)
            {
                Points = colPoints;
                Signal = signal;
                Crosshair = crosshair;
                IsVisible = true;
            }
        }
        #endregion

        #region 成员变量
        private static ViewTest _instance = null;
        private Dictionary<string, string> _tests;
        private readonly Dictionary<string, ExFuncSignalXY> _signals = new Dictionary<string, ExFuncSignalXY>();
        private string _selectedTestID = null;
        private string _displayedTestID = null;
        private LegendPanel _legendPanel = null;
        private HistoryManager HistoryManager { get; }
        #endregion

        #region 构造函数及单例模式
        private ViewTest()
        {
            InitializeComponent();
            HistoryManager = MainForm.Instance().HistoryManager;
        }

        public static ViewTest Instance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new ViewTest();
            }
            return _instance;
        }
        #endregion

        #region 窗口加载和关闭
        private void ViewTest_Load(object sender, EventArgs e)
        {
            var tests = HistoryManager.GetTests();
            _tests = tests;
            LBox_Tests.DisplayMember = "Value";
            LBox_Tests.ValueMember = "Key";
            LBox_Tests.DataSource = new BindingSource(_tests, null);

            if (_tests.Count == 0)
            {
                MessageBox.Show("没有读取到任何测试！即将回到实时测试窗口。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            Plt.MouseMove += (S, E) =>
            {
                // determine where the mouse is and get the nearest point
                Pixel mousePixel = new Pixel(E.Location.X, E.Location.Y);
                Coordinates mouseLocation = Plt.Plot.GetCoordinates(mousePixel);

                DataPoint nearest;
                foreach (var signal in _signals)
                {
                    if (!signal.Value.IsVisible) continue;

                    nearest = signal.Value.Signal.Data.GetNearestX(mouseLocation, Plt.Plot.LastRender);
                    // place the crosshair over the highlighted point
                    if (nearest.IsReal)
                    {
                        signal.Value.Crosshair.IsVisible = signal.Value.IsVisible;
                        signal.Value.Crosshair.Position = nearest.Coordinates;
                        signal.Value.Signal.LegendText = $"{signal.Key}: {nearest.Y:0.00}";
                        signal.Value.Crosshair.VerticalLine.LabelText = $"{nearest.X}";
                    }

                    // hide the crosshair when no point is selected
                    if (!nearest.IsReal && signal.Value.Crosshair.IsVisible)
                    {
                        signal.Value.Crosshair.IsVisible = false;
                        signal.Value.Signal.LegendText = $"{signal.Key}";
                    }
                }

                Plt.Refresh();
            };
        }

        private void ViewTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Instance().Show();
        }
        #endregion

        #region 添加signal及对应控件变更
        /// <summary>
        /// 在字典添加对应列表头的信号信息
        /// </summary>
        /// <param name="col">列表头</param>
        /// <param name="colPoints">列数据</param>
        /// <param name="color">信号曲线的颜色</param>
        private void AddNewEXSignal(string col, ColPoints colPoints, Color color, LinePattern linePattern)
        {
            var signal = Plt.Plot.Add.SignalXY(colPoints.GetBatches(), colPoints.GetVals(), color);
            signal.LegendText = col;
            signal.LinePattern = linePattern;

            var crosshair = Plt.Plot.Add.Crosshair(0, 0);
            crosshair.IsVisible = false;
            crosshair.MarkerColor = color;
            crosshair.HorizontalLine.IsVisible = false;
            crosshair.VerticalLine.Color = Color.Gray(0);
            crosshair.VerticalLine.LineWidth = 1;
            //crosshair.VerticalLine.IsVisible = false;
            crosshair.VerticalLine.LabelFontSize = 16;
            crosshair.VerticalLine.LabelFontColor = Color.FromColor(System.Drawing.Color.White);
            crosshair.VerticalLine.LabelBackgroundColor = Color.FromColor(System.Drawing.Color.Black);
            crosshair.MarkerShape = MarkerShape.FilledCircle;
            crosshair.MarkerSize = 10;

            _signals.Add(col, new ExFuncSignalXY(colPoints, signal, crosshair));

            CBL_LinesVis.Items.Add(col, true);
        }
        #endregion

        #region 控件变更事件
        private void LBox_Tests_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTestID = LBox_Tests.SelectedValue.ToString();
        }

        private void CBL_LinesVis_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string col = CBL_LinesVis.Items[e.Index].ToString();
            _signals[col].IsVisible = e.NewValue == CheckState.Checked;
            _signals[col].Signal.IsVisible = _signals[col].IsVisible;
            _signals[col].Crosshair.IsVisible = _signals[col].IsVisible;
            Plt.Refresh();
        }
        #endregion

        #region 按键功能
        private void PB_ReadTests_Click(object sender, EventArgs e)
        {
            if (_displayedTestID == _selectedTestID) return;
            _displayedTestID = _selectedTestID;
            var testID = _displayedTestID;

            _signals.Clear();
            CBL_LinesVis.Items.Clear();
            Plt.Plot.Clear();

            if (_legendPanel == null)
            {
                _legendPanel = Plt.Plot.ShowLegend(Edge.Top);
                _legendPanel.Alignment = Alignment.UpperLeft;
                _legendPanel.Legend.Orientation = ScottPlot.Orientation.Horizontal;
                _legendPanel.Legend.FontSize = 16;
            }

            var colHeaders = HistoryManager.GetColHeaders(testID);
            if (colHeaders != null)
            {
                int colorIndex = 0;
                foreach (var col in colHeaders)
                {
                    var colData = HistoryManager.GetColPoints(testID, col);
                    if (colData != null)
                    {
                        //var color = Color.RandomHue();
                        if (colorIndex >= PlotManager.GetColorArrayLength()) colorIndex = 0;
                        var color = PlotManager.GetColor(colorIndex++);
                        AddNewEXSignal(col, colData, color, LinePattern.Solid);
                    }
                }
            }

            Plt.Plot.Axes.AutoScale();
            Plt.Refresh();
        }

        private void PB_DeleteTest_Click(object sender, EventArgs e)
        {
            if (_selectedTestID == null) return;
            if (!_tests.ContainsKey(_selectedTestID)) return;

            var res = MessageBox.Show($"请确定是否删除 TestID: {_selectedTestID}，存于时间 {_tests[_selectedTestID]} 的测试。",
                "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Cancel) return;

            HistoryManager.DeleteTest(_selectedTestID);
            _tests.Remove(_selectedTestID );
            LBox_Tests.DataSource = new BindingSource(_tests, null);
        }

        private void PB_ResetView_Click(object sender, EventArgs e)
        {
            Plt.Plot.Axes.AutoScale();
            Plt.Refresh();
        }

        private void PB_DrawDif_Click(object sender, EventArgs e)
        {
            if (CBL_LinesVis.Items.Count == 0) return;
            try
            {
                var item = CBL_LinesVis.SelectedItem;
                string col = item.ToString();

                if (_signals.ContainsKey($"{col}.diff")) return;

                var diff = _signals[col].Points.GetDifferential(1);

                AddNewEXSignal($"{col}.diff", diff, Color.Gray(0), LinePattern.Dotted);
                Plt.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请选择一条曲线再尝试：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PB_AvgFilter_Click(object sender, EventArgs e)
        {
            if (CBL_LinesVis.Items.Count == 0) return;
            try
            {
                var item = CBL_LinesVis.SelectedItem;
                string col = item.ToString();

                if (_signals.ContainsKey($"{col}.avg")) return;

                var avg = _signals[col].Points.GetAvgFilter(10);

                AddNewEXSignal($"{col}.avg", avg, Color.Gray(0), LinePattern.Dotted);
                Plt.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请选择一条曲线再尝试：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PB_MidFilter_Click(object sender, EventArgs e)
        {
            if (CBL_LinesVis.Items.Count == 0) return;
            try
            {
                var item = CBL_LinesVis.SelectedItem;
                string col = item.ToString();

                if (_signals.ContainsKey($"{col}.mid")) return;

                var mid = _signals[col].Points.GetMiddleFilter(7);

                AddNewEXSignal($"{col}.mid", mid, Color.Gray(0), LinePattern.Dotted);
                Plt.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请选择一条曲线再尝试：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
