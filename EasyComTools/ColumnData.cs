using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyComTools
{
    #region 数据列类
    internal class ColumnData
    {
        private readonly Semaphore _sm_arrayBuffer = new Semaphore(1, 1);
        private readonly List<double> _batches = new List<double>();
        private readonly List<double> _values = new List<double>();

        private Queue<(double, double, uint)> Points { get; }
        private string SaveSql { set; get; }
        private readonly int _testID;
        private readonly string _header;
        private readonly int _maxDisplaySize;
        private readonly int _maxBufferSize;
        private int _currentBufferSize;

        public ColumnData(int testid, string header, int maxDisplaySize = 100, int maxBufferSize = 500)
        {
            Points = new Queue<(double, double, uint)>();
            SaveSql = string.Empty;
            _maxDisplaySize = maxDisplaySize;
            _maxBufferSize = maxBufferSize;
            _currentBufferSize = 0;
            _testID = testid;
            _header = header;
        }

        private void AddArrayBuffer(uint batch, double val)
        {
            _sm_arrayBuffer.WaitOne();
            _batches.Add(batch);
            _values.Add(val);
            _sm_arrayBuffer.Release();
        }

        public void GetArrayBuffer(out double[] batches, out double[] values)
        {
            _sm_arrayBuffer.WaitOne();

            double[] newbatches = _batches.ToArray();
            double[] newvalues = _values.ToArray();

            _batches.Clear();
            _values.Clear();

            _sm_arrayBuffer.Release();

            batches = newbatches;
            values = newvalues;
        }

        private string SqlPointVal(double time, double val, uint batch_id)
        {
            return $"({_testID}, '{_header}', '{batch_id}', '{DateTime.FromOADate(time):O}', '{val}'), ";
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        /// <param name="time">数据产生时间</param>
        /// <param name="val">数据</param>
        /// <returns>如果达到最大缓冲，则返回需要存到数据库的数据字符串，格式和sql语句一致；否则返回null</returns>
        public string AddPoint(double time, double val, uint batch_id)
        {
            Points.Enqueue((time, val, batch_id));

            AddArrayBuffer(batch_id, val);

            if (Points.Count > _maxDisplaySize)
            {
                var first = Points.Dequeue();
                SaveSql += SqlPointVal(time, val, batch_id);
                ++_currentBufferSize;
            }
            if (_currentBufferSize == _maxBufferSize) return SaveSql;
            return null;
        }

        public string GetSql()
        {
            return SaveSql;
        }

        public void ClearSql()
        {
            SaveSql = string.Empty;
            _currentBufferSize = 0;
        }

        public string GetRestData()
        {
            while (Points.Count > 0)
            {
                var point = Points.Dequeue();
                SaveSql += SqlPointVal(point.Item1, point.Item2, point.Item3);
            }
            return SaveSql;
        }
    }
    #endregion
}
