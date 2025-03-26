using System;
using System.Collections.Generic;
using ScottPlot;
using ScottPlot.WinForms;

namespace EasyComTools
{
    internal class PlotManager
    {
        private readonly static string[] colorHexCodes = new string[]
        {
            "#8B0000",  // 深红
            "#0047AB",  // 钴蓝
            "#32CD32",  // 酸橙绿
            "#800080",  // 深紫
            "#FFA500",  // 亮橙
            "#008B8B",  // 深青绿
            "#FF00FF",  // 洋红
            "#808000",  // 橄榄绿
            "#FF1493",  // 深粉红
            "#008080",  // 深蓝绿
            "#FFD700",  // 金黄
            "#4B0082",  // 靛蓝
            "#FF7F50",  // 珊瑚色
            "#00BFFF",  // 深天蓝
            "#800000",  // 栗色
            "#39FF14",  // 霓虹绿
            "#FF00AF",  // 品红
            "#000080",  // 深海军蓝
            "#FF4500",  // 橙红
            "#005F6A"   // 孔雀蓝
        };

        private readonly FormsPlot _plt;
        //private readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>();
        //private readonly Dictionary<string, (double, double)> _lastEndPoint = new Dictionary<string, (double, double)>();
        private readonly Dictionary<string, ScottPlot.Plottables.DataLogger> _loggers = new Dictionary<string, ScottPlot.Plottables.DataLogger>();

        public PlotManager(FormsPlot formsPlot)
        {
            _plt = formsPlot;
            InitializePlot();
        }

        private void InitializePlot()
        {
            _plt.Plot.Clear();
        }

        public void Clear()
        {
            _plt.Plot.Clear();
            _loggers.Clear();
            _plt.Plot.Axes.AutoScale(true, true);
        }

        public void Render()
        {
            _plt.Plot.Axes.AutoScaleExpandY();
            _plt.Refresh();
        }

        private int _colorIndex = 0;
        public static Color GetColor(int index)
        {
            return Color.FromHex(colorHexCodes[index]);
        }

        public static int GetColorArrayLength()
        {
            return colorHexCodes.Length;
        }

        public void AddPoints(double[] xs, double[] ys, string label)
        {
            try
            {
                if (!_loggers.ContainsKey(label))
                {
                    //var color = Color.RandomHue();
                    if (_colorIndex >= colorHexCodes.Length)
                    {
                        _colorIndex = 0;
                    }
                    var color = GetColor(_colorIndex++);
                    var logger = _plt.Plot.Add.DataLogger();
                    logger.Color = color;
                    logger.ViewFull();
                    logger.LegendText = label;
                    _loggers.Add(label, logger);
                }
                _loggers[label].Add(xs, ys);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"绘制曲线图异常: {ex.Message}");
            }
        }
    }
}
