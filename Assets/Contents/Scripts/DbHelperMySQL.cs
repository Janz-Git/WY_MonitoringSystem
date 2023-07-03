using System;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

using MySql.Data.MySqlClient;

namespace DotNet.FrameWork.Data
{
    public class DbHelperMySQL
    {
        public static string connectionString = "";

        private static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            object obj = ExecuteScalar(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 是否存在（基于MySqlParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = ExecuteScalar(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 执行sql语句，返回结果的第一行第一列，多余的行或列会被忽略掉，如果未查询到数据返回null
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);
                object obj = cmd.ExecuteScalar();

                return obj;
            }
        }

        /// <summary>
        /// 执行sql语句，返回受影响的行数，未执行返回-1，select 返回-1
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);
                object val = cmd.ExecuteNonQuery();
                return Convert.ToInt32(val);
            }
        }

        /// <summary>
        /// 执行sql语句，返回受影响的行数，未执行返回-1，select 返回-1
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);
                PrepareCommand(cmd, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return Convert.ToInt32(val);
            }
        }

        public static DataTable ExecuteDataTable(string cmdText)
        {
            DataSet ds = new DataSet();
            using (MySqlDataAdapter msda = new MySqlDataAdapter(cmdText, GetConnection()))
            {
                msda.Fill(ds);
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = GetConnection())
            {
                PrepareCommand(cmd, CommandType.Text, cmdText, commandParameters);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds.Tables[0];
                }
            }
        }

        //获取起始页码和结束页码
        public static DataTable ExecuteDataTable(string cmdText, int startResord, int maxRecord)
        {
            using (MySqlConnection connection = GetConnection())
            {
                DataSet ds = new DataSet();
                try
                {
                    MySqlDataAdapter command = new MySqlDataAdapter(cmdText, connection);
                    command.Fill(ds, startResord, maxRecord, "ds");
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds.Tables[0];
            }
        }


        /// <summary>
        /// 执行带参数的sql语句并返回第一行的第一列
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText,params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = GetConnection())
            {
                PrepareCommand(cmd, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }



        /// <summary>
        /// 获取分页数据 在不用存储过程情况下
        /// </summary>
        /// <param name="recordCount">总记录条数</param>
        /// <param name="selectList">选择的列逗号隔开,支持top num</param>
        /// <param name="tableName">表名字</param>
        /// <param name="whereStr">条件字符 必须前加 and</param>
        /// <param name="orderExpression">排序 例如 ID</param>
        /// <param name="pageIdex">当前索引页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public static DataTable GetRecordByPage(out int recordCount, string selectList, string tableName, string whereStr, string orderExpression, int pageIdex, int pageSize)
        {
            int rows = 0;
            DataTable dt = new DataTable();
            MatchCollection matchs = Regex.Matches(selectList, @"top\s+\d{1,}", RegexOptions.IgnoreCase);//含有top
            string sqlStr = sqlStr = string.Format("select {0} from {1} where 1=1 {2}", selectList, tableName, whereStr);
            if (!string.IsNullOrEmpty(orderExpression)) { sqlStr += string.Format(" Order by {0}", orderExpression); }
            if (matchs.Count > 0) //含有top的时候
            {
                DataTable dtTemp = ExecuteDataTable(sqlStr);
                rows = dtTemp.Rows.Count;
            }
            else //不含有top的时候
            {
                string sqlCount = string.Format("select count(*) from {0} where 1=1 {1} ", tableName, whereStr);
                //获取行数
                object obj = ExecuteScalar(sqlCount);
                if (obj != null)
                {
                    rows = Convert.ToInt32(obj);
                }
            }
            dt = ExecuteDataTable(sqlStr, (pageIdex - 1) * pageSize, pageSize);
            recordCount = rows;
            return dt;
        }


        private static void PrepareCommand(MySqlCommand cmd, CommandType cmdType, string cmdText, MySqlParameter[] commandParameters)
        {
            cmd.Connection = GetConnection();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (commandParameters != null)
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
        }

    }
}