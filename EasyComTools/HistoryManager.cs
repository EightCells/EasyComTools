using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EasyComTools
{
    internal class ColPoints
    {
        private readonly List<double> Batches;
        private readonly List<double> Vals;

        public ColPoints()
        {
            Batches = new List<double>();
            Vals = new List<double>();
        }

        public void Add(double batch, double val)
        {
            Batches.Add(batch);
            Vals.Add(val);
        }

        public double[] GetBatches()
        {
            return Batches.ToArray();
        }

        public double[] GetVals()
        {
            return Vals.ToArray();
        }

        public ColPoints GetDifferential(int step)
        {
            ColPoints res = new ColPoints();

            for (int i = 0; i < Batches.Count; i++)
            {
                if (i < step) res.Add(Batches[i], 0);
                else res.Add(Batches[i], (Vals[i] - Vals[i - 1]) * 10);
            }

            return res;
        }

        public ColPoints GetAvgFilter(int step)
        {
            ColPoints res = new ColPoints();

            for (int i = 0; i < Batches.Count; i++)
            {
                double sum = 0;
                int cnt = Math.Min(i + 1, step);
                for (int j = 0; j < cnt; j++)
                {
                    sum += Vals[i - j];
                }
                res.Add(Batches[i], sum / cnt);
            }

            return res;
        }

        public ColPoints GetMiddleFilter(int step)
        {
            ColPoints res = new ColPoints();

            for (int i = 0; i < Batches.Count; i++)
            {
                int cnt = Math.Min(i + 1, step);
                var tmp = new List<double>(cnt);
                for (int j = 0; j < cnt; j++)
                {
                    tmp.Add(Vals[i - j]);
                }
                tmp.Sort();
                res.Add(Batches[i], tmp[cnt / 2]);
            }

            return res;
        }
    }

    internal class HistoryManager
    {
        #region 属性和字段
        private readonly string _databasePath;
        private SQLiteConnection _connection;
        #endregion

        #region 构造函数
        public HistoryManager(string dbPath = "EasyComDatabase.db")
        {
            _databasePath = dbPath;

            InitializeDatabase();
        }

        #endregion

        #region 初始化方法
        private void InitializeDatabase()
        {
            bool isNewDb = !System.IO.File.Exists(_databasePath);
            if (isNewDb) SQLiteConnection.CreateFile(_databasePath);

            _connection = new SQLiteConnection($"Data Source={_databasePath};Version=3;");
            _connection.Open();

            if (isNewDb) CreateMetaTable();
        }

        private void CreateMetaTable()
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = @"
                    CREATE TABLE TestInfo(
                    TestID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CreateTime TEXT NOT NULL)";

                cmd.ExecuteNonQuery();
            }

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = @"
                    CREATE TABLE ColData(
                    ColID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TestID INTEGER NOT NULL,
                    ColHeader TEXT NOT NULL,
                    BatchID INTEGER NOT NULL,
                    Time TEXT NOT NULL DEFAULT 'NA',
                    Val TEXT NOT NULL)";

                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 创建新测试表
        public int _currentTestID;
        public void CreateNewTest(DateTime dateTime)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO TestInfo (CreateTime) 
                    VALUES (@createTime)";

                cmd.Parameters.AddWithValue("@createTime", dateTime.ToString("o"));
                cmd.ExecuteNonQuery();
            }

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT last_insert_rowid()";
                _currentTestID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        #endregion

        #region 添加数据
        public void AddColData(string datasql)
        {
            var sqlBuilder = new StringBuilder(@"INSERT INTO ColData (TestID, ColHeader, BatchID, Time, Val) VALUES ");

            sqlBuilder.Append(datasql.TrimEnd(',', ' '));
            sqlBuilder.Append(";");

            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = sqlBuilder.ToString();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 读取数据库数据
        public Dictionary<string, string> GetTests()
        {
            var tests = new Dictionary<string, string>();
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT TestID, CreateTime FROM TestInfo;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(reader.GetInt32(0).ToString(), reader.GetString(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取测试数据失败:\r\n{ex.Message}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tests;
        }

        public void DeleteTest(string testID)
        {
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ColData WHERE TestID = @testID;";
                    cmd.Parameters.AddWithValue("@testID", testID);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM TestInfo WHERE TestID = @testID;";
                    cmd.Parameters.AddWithValue("@testID", testID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除测试失败 TestID={testID}:\r\n{ex.Message}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<string> GetColHeaders(string testID)
        {
            var colHeaders = new List<string>();
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT DISTINCT ColHeader FROM ColData WHERE TestID = @testID;";
                    cmd.Parameters.AddWithValue("@testID", testID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colHeaders.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取列表头ColHeader失败， TestID={testID}:\r\n{ex.Message}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return colHeaders;
        }

        public ColPoints GetColPoints(string testID, string colHeader)
        {
            var points = new ColPoints();
            try
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText =
                        @"SELECT BatchID, Val FROM ColData 
                        WHERE TestID = @testID AND ColHeader = @colHeader;";

                    cmd.Parameters.AddWithValue("@testID", testID);
                    cmd.Parameters.AddWithValue("@colHeader", colHeader);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            points.Add(Convert.ToDouble(reader.GetInt32(0)), Convert.ToDouble(reader.GetString(1)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取数据失败 TestID={testID} ColHeader={colHeader}:\r\n{ex.Message}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return points;
        }
        #endregion
    }
}
