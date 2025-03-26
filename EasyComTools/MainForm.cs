using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace EasyComTools
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = null;

        private MainForm()
        {
            InitializeComponent();
        }

        public static MainForm Instance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new MainForm();
            }
            return _instance;
        }

        #region 主窗口初始化
        private PlotManager _plotManager;
        private void MainForm_Load(object sender, EventArgs e)
        {
            _plotManager = new PlotManager(Plot);

        }
        #endregion

        #region 串口
        private SerialPort _serialPort;
        private bool _isConnected;
        private readonly SerialProtocolParser _protocolParser = new SerialProtocolParser(new Crc16());

        #region 定时检测串口设备
        private void TIM_ComListCheck_Tick(object sender, EventArgs e)
        {
            var currentPorts = SerialPort.GetPortNames();
            if (currentPorts.SequenceEqual(CB_ComList.Items.Cast<string>())) return;

            Invoke((Action)(() =>
            {
                CB_ComList.DataSource = currentPorts;
            }));
        }
        #endregion

        #region 串口连接
        /// <summary>
        /// 串口已连接返回true，否则返回false
        /// </summary>
        private bool PleaseConnect()
        {
            if (!_isConnected)
                MessageBox.Show("请先连接设备！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return _isConnected;
        }

        private void Connect(string portName)
        {
            try
            {
                _serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
                _serialPort.DataReceived += SerialDataReceived;
                _serialPort.Open();
                _isConnected = true;
                PB_Connect.Text = "断开";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}");
            }
        }

        private void Disconnect()
        {
            if (_isStartATest)
            {
                PB_StartATest_Click(null, null);
            }
            _serialPort?.Close();
            _isConnected = false;
            PB_Connect.Text = "连接";
        }

        private void PB_Connect_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                Disconnect();
            }
            else
            {
                Connect(CB_ComList.SelectedItem?.ToString());
            }
        }
        #endregion

        #region 串口接收
        private bool _isEnableParser = false;
        private void StartSerialParser() { _isEnableParser = true; }
        private void StopSerialParser() { _isEnableParser = false; }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);

                // 使用协议解析接口
                if (_isEnableParser)_protocolParser.ReceiveBytes(buffer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"数据接收异常: {ex.Message}");
            }
        }
        #endregion
        #endregion

        #region 绘制曲线图

        private void TIM_PltReresh_Tick(object sender, EventArgs e)
        {
            foreach (var col in _dataTable)
            {
                col.Value.GetArrayBuffer(out double[] batches, out double[] values);
                if (batches.Length > 0)
                    _plotManager.AddPoints(batches, values, col.Key);
            }
            _plotManager.Render();
        }

        #endregion

        #region 功能按键
        

        #region 开始一次测试（实时保存数据到数据库）
        private bool _isStartATest = false;
        private void PB_StartATest_Click(object sender, EventArgs e)
        {
            if (!PleaseConnect()) return;

            if (!_isStartATest)
            {
                try
                {
                    _historyManager.CreateNewTest(DateTime.Now);
                    TIM_AddData.Start();
                    _plotManager.Clear();
                    TIM_PltReresh.Start();
                    StartSerialParser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"创建新测试失败: {ex.Message}");
                    return;
                }
            }
            else
            {
                try
                {
                    StopSerialParser();
                    TIM_AddData.Stop();
                    // 处理可能还在缓冲区的数据
                    GetParserDataAndAdd();
                    // 处理字典中未保存的数据
                    foreach (var col in _dataTable)
                    {
                        string sql = col.Value.GetRestData();
                        if (sql.Length > 0)
                            _historyManager.AddColData(sql);
                    }
                    // 清除字典
                    _dataTable.Clear();
                    TIM_PltReresh.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"停止测试失败: {ex.Message}");
                    return;
                }
            }
            _isStartATest = !_isStartATest;
            PB_StartATest.Text = _isStartATest ? "结束测试" : "开始测试";
        }
        #endregion

        #region 暂停
        private void PB_Stop_Click(object sender, EventArgs e)
        {
            if (!PleaseConnect()) return;
        }
        #endregion

        #region 查看历史测试
        private void PB_ViewTest_Click(object sender, EventArgs e)
        {
            if (_isStartATest) { MessageBox.Show("请先结束测试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            var viewTestForm = ViewTest.Instance();

            _instance.Hide();
            viewTestForm.Show();
        }
        #endregion

        #endregion

        #region 数据保存
        private readonly Dictionary<string, ColumnData> _dataTable = new Dictionary<string, ColumnData>();
        private readonly HistoryManager _historyManager = new HistoryManager();

        internal HistoryManager HistoryManager => _historyManager;

        private void AddData<T>(byte id, DateTime t, T data, uint batchID)
        {
            string header = id.ToString();

            if (!_dataTable.ContainsKey(header))
            {
                _dataTable[header] = new ColumnData(_historyManager._currentTestID, header);
            }

            string tmp = _dataTable[header].AddPoint(t.ToOADate(), Convert.ToDouble(data), batchID);

            // tmp不为null则需将缓冲存入数据库
            if (tmp != null)
            {
                _historyManager.AddColData(tmp);
                _dataTable[header].ClearSql();
            }
        }

        /// <summary>
        /// 获取解析器里缓存队列的数据并添加
        /// </summary>
        private void GetParserDataAndAdd()
        {
            while (_protocolParser.TryGetData(out (DateTime, SerialProtocolParser.DataFrame) data))
            {
                try
                {
                    byte id = data.Item2.Identifier;
                    DateTime time = data.Item1;
                    SerialProtocolParser.DataFrame frame = data.Item2;
                    uint batchID = BitConverter.ToUInt32(frame.BatchID, 0);

                    switch (data.Item2.TypeId)
                    {
                        case SerialProtocolParser.DataType.UInt32:
                            AddData(id, time, BitConverter.ToUInt32(frame.Payload, 0), batchID);
                            break;
                        case SerialProtocolParser.DataType.Int32:
                            AddData(id, time, BitConverter.ToInt32(frame.Payload, 0), batchID);
                            break;
                        case SerialProtocolParser.DataType.Float32:
                            AddData(id, time, BitConverter.ToSingle(frame.Payload, 0), batchID);
                            break;
                        case SerialProtocolParser.DataType.Double64:
                            AddData(id, time, BitConverter.ToDouble(frame.Payload, 0), batchID);
                            break;
                    }
                }
                catch 
                {
                    return;
                }
            }
        }

        private void TIM_AddData_Tick(object sender, EventArgs e)
        {
            GetParserDataAndAdd();
        }
        #endregion

    }
}
