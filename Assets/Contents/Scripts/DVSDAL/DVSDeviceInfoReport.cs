using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSDeviceInfoReport
    /// </summary>
    public partial class DVSDeviceInfoReport
    {
        public DVSDeviceInfoReport()
        { }

        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(DiBo.Model.DVSDeviceInfoReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DVSDeviceInfoReport(");
            strSql.Append("CommandCode,DeviceID,DeviceName,Version,ChannelCount,StatusOK,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@DeviceName,@Version,@ChannelCount,@StatusOK,@CreateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DeviceName", MySqlDbType.VarChar,100),
                    new MySqlParameter("@Version", MySqlDbType.VarChar,100),
                    new MySqlParameter("@ChannelCount", MySqlDbType.Int32),
                    new MySqlParameter("@StatusOK", MySqlDbType.Int32),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.DeviceName;
            parameters[3].Value = model.Version;
            parameters[4].Value = model.ChannelCount;
            parameters[5].Value = model.StatusOK;
            parameters[6].Value = model.CreateDate;

            var obj = DbHelperMySQL.ExecuteScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DiBo.Model.DVSDeviceInfoReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DVSDeviceInfoReport set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("DeviceName=@DeviceName,");
            strSql.Append("Version=@Version,");
            strSql.Append("ChannelCount=@ChannelCount,");
            strSql.Append("StatusOK=@StatusOK,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where DeviceInfoID=@DeviceInfoID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DeviceName", MySqlDbType.VarChar,100),
                    new MySqlParameter("@Version", MySqlDbType.VarChar,100),
                    new MySqlParameter("@ChannelCount", MySqlDbType.Int32),
                    new MySqlParameter("@StatusOK", MySqlDbType.Int32),
                    new MySqlParameter("@DeviceInfoID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.DeviceName;
            parameters[3].Value = model.Version;
            parameters[4].Value = model.ChannelCount;
            parameters[5].Value = model.StatusOK;
            parameters[6].Value = model.DeviceInfoID;

            int rows = DbHelperMySQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeviceInfoID,CommandCode,DeviceID,DeviceName,Version,ChannelCount,StatusOK,CreateDate ");
            strSql.Append(" FROM DVSDeviceInfoReport ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" DeviceInfoID,CommandCode,DeviceID,DeviceName,Version,ChannelCount,StatusOK,CreateDate ");
            strSql.Append(" FROM DVSDeviceInfoReport ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            if (Top > 0)
            {
                strSql.Append(" limit 0," + Top.ToString());
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="dataNumber">获取的数据条数</param>
        /// <returns></returns>
		public static List<DiBo.Model.DVSDeviceInfoReport> GetModels(int dataNumber)
        {

            List<DiBo.Model.DVSDeviceInfoReport> models=new List<Model.DVSDeviceInfoReport>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeviceInfoID,CommandCode,DeviceID,DeviceName,Version,ChannelCount,StatusOK,CreateDate from DVSDeviceInfoReport order by DeviceInfoID desc limit 0,"+ dataNumber + "");

            DataTable dtbl = DbHelperMySQL.ExecuteDataTable(strSql.ToString());
            if (dtbl.Rows.Count > 0)
            {
                foreach (DataRow row in dtbl.Rows)
                {
                    models.Add(DataRowToModel(row));
                }
            }
            return models;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DiBo.Model.DVSDeviceInfoReport DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSDeviceInfoReport model = new DiBo.Model.DVSDeviceInfoReport();
            if (row != null)
            {
                if (row["DeviceInfoID"] != null && row["DeviceInfoID"].ToString() != "")
                {
                    model.DeviceInfoID = int.Parse(row["DeviceInfoID"].ToString());
                }
                if (row["CommandCode"] != null)
                {
                    model.CommandCode = row["CommandCode"].ToString();
                }
                if (row["DeviceID"] != null)
                {
                    model.DeviceID = row["DeviceID"].ToString();
                }
                if (row["DeviceName"] != null)
                {
                    model.DeviceName = row["DeviceName"].ToString();
                }
                if (row["Version"] != null)
                {
                    model.Version = row["Version"].ToString();
                }
                if (row["ChannelCount"] != null && row["ChannelCount"].ToString() != "")
                {
                    model.ChannelCount = int.Parse(row["ChannelCount"].ToString());
                }
                if (row["StatusOK"] != null && row["StatusOK"].ToString() != "")
                {
                    model.StatusOK = int.Parse(row["StatusOK"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
            }
            return model;
        }

        #endregion  BasicMethod
    }
}


