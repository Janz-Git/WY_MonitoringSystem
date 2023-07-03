using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSRawSignalCurveReturn
    /// </summary>
    public partial class DVSRawSignalCurveReturn
    {
        public DVSRawSignalCurveReturn()
        { }
        #region  BasicMethod
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int SignalID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dvsrawsignalcurvereturn");
            strSql.Append(" where SignalID=@SignalID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@SignalID", MySqlDbType.Int32)
            };
            parameters[0].Value = SignalID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(DiBo.Model.DVSRawSignalCurveReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvsrawsignalcurvereturn(");
            strSql.Append("CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@ChannelID,@CurveLength,@Resolution,@CurveData,@CreateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@CurveLength", MySqlDbType.Int32),
                    new MySqlParameter("@Resolution", MySqlDbType.Float),
                    new MySqlParameter("@CurveData", MySqlDbType.Text),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.CurveLength;
            parameters[4].Value = model.Resolution;
            parameters[5].Value = model.CurveData;
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
		public static bool Update(DiBo.Model.DVSRawSignalCurveReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvsrawsignalcurvereturn set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("CurveLength=@CurveLength,");
            strSql.Append("Resolution=@Resolution,");
            strSql.Append("CurveData=@CurveData,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where SignalID=@SignalID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@CurveLength", MySqlDbType.Int32),
                    new MySqlParameter("@Resolution", MySqlDbType.Float),
                    new MySqlParameter("@CurveData", MySqlDbType.Text),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@SignalID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.CurveLength;
            parameters[4].Value = model.Resolution;
            parameters[5].Value = model.CurveData;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.SignalID;

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
            strSql.Append("select SignalID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate ");
            strSql.Append(" FROM DVSRawSignalCurveReturn ");
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
            strSql.Append(" SignalID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate ");
            strSql.Append(" FROM DVSRawSignalCurveReturn ");
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
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static List<DiBo.Model.DVSRawSignalCurveReturn> GetModel(int dataNumber)
        {
            List<DiBo.Model.DVSRawSignalCurveReturn> models = new List<Model.DVSRawSignalCurveReturn>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SignalID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate from DVSRawSignalCurveReturn order by SignalID desc limit 0," + dataNumber + "");

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
        public static DiBo.Model.DVSRawSignalCurveReturn DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSRawSignalCurveReturn model = new DiBo.Model.DVSRawSignalCurveReturn();
            if (row != null)
            {
                if (row["SignalID"] != null && row["SignalID"].ToString() != "")
                {
                    model.SignalID = int.Parse(row["SignalID"].ToString());
                }
                if (row["CommandCode"] != null)
                {
                    model.CommandCode = row["CommandCode"].ToString();
                }
                if (row["DeviceID"] != null)
                {
                    model.DeviceID = row["DeviceID"].ToString();
                }
                if (row["ChannelID"] != null && row["ChannelID"].ToString() != "")
                {
                    model.ChannelID = int.Parse(row["ChannelID"].ToString());
                }
                if (row["CurveLength"] != null && row["CurveLength"].ToString() != "")
                {
                    model.CurveLength = int.Parse(row["CurveLength"].ToString());
                }
                if (row["Resolution"] != null && row["Resolution"].ToString() != "")
                {
                    model.Resolution = decimal.Parse(row["Resolution"].ToString());
                }
                if (row["CurveData"] != null)
                {
                    model.CurveData = row["CurveData"].ToString();
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


