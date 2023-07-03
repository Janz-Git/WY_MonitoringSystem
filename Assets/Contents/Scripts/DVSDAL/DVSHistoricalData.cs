using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSHistoricalData
    /// </summary>
    public partial class DVSHistoricalData
    {
        public DVSHistoricalData()
        { }
        #region  BasicMethod

        public static bool UpdateDealingStatus(int AlarmInfoID,string dealingUser,string dealingDescription)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DVSAlarmInfoReport set ");
            strSql.Append("DealingUser=@DealingUser,");
            strSql.Append("DealingDesc=@DealingDesc,");
            strSql.Append("DealingDate=@DealingDate,");
            strSql.Append("DealingStatus=@DealingStatus,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where AlarmInfoID=@AlarmInfoID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@DealingUser", MySqlDbType.VarChar,20),
                    new MySqlParameter("@DealingDesc", MySqlDbType.VarChar,100),
                    new MySqlParameter("@DealingDate", MySqlDbType.DateTime),
                    new MySqlParameter("@DealingStatus", MySqlDbType.Int16),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@AlarmInfoID", MySqlDbType.Int16)};
            parameters[0].Value = dealingUser;
            parameters[1].Value = dealingDescription;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = 1;
            parameters[4].Value = DateTime.Now;
            parameters[5].Value = AlarmInfoID;

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
		/// 增加一条数据
		/// </summary>
		public static int Add(DiBo.Model.DVSHistoricalData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DVSAlarmInfoReport(");
            strSql.Append("CommandCode,DeviceID,ChannelID,ZoneID,AlarmDateTime,FiberPosition,AlarmType,MaxAmptitude,AlarmEndTime,AlarmLevel,AlarmStatus,ZoneName,DealingUser,DealingDesc,DealingDate,DealingStatus,VCount,CreateDate,UpdateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@ChannelID,@ZoneID,@AlarmDateTime,@FiberPosition,@AlarmType,@MaxAmptitude,@AlarmEndTime,@AlarmLevel,@AlarmStatus,@ZoneName,@DealingUser,@DealingDesc,@DealingDate,@DealingStatus,@VCount,@CreateDate,@UpdateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneID", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmDateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@FiberPosition", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmType", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MaxAmptitude", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmEndTime", MySqlDbType.DateTime),
                    new MySqlParameter("@AlarmLevel", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmStatus", MySqlDbType.Int16),
                    new MySqlParameter("@ZoneName", MySqlDbType.VarChar,30),
                    new MySqlParameter("@DealingUser", MySqlDbType.VarChar,20),
                    new MySqlParameter("@DealingDesc", MySqlDbType.VarChar,100),
                    new MySqlParameter("@DealingDate", MySqlDbType.DateTime),
                    new MySqlParameter("@DealingStatus", MySqlDbType.Int16),
                    new MySqlParameter("@VCount", MySqlDbType.Int32),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.ZoneID;
            parameters[4].Value = model.AlarmDateTime;
            parameters[5].Value = model.FiberPosition;
            parameters[6].Value = model.AlarmType;
            parameters[7].Value = model.MaxAmptitude;
            parameters[8].Value = model.AlarmEndTime;
            parameters[9].Value = model.AlarmLevel;
            parameters[10].Value = model.AlarmStatus;
            parameters[11].Value = model.ZoneName;
            parameters[12].Value = model.DealingUser;
            parameters[13].Value = model.DealingDesc;
            parameters[14].Value = model.DealingDate;
            parameters[15].Value = model.DealingStatus;
            parameters[16].Value = model.VCount;
            parameters[17].Value = model.CreateDate;
            parameters[18].Value = model.UpdateDate;

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
        public static bool Update(DiBo.Model.DVSAlarmInfoReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DVSAlarmInfoReport set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("ZoneID=@ZoneID,");
            strSql.Append("AlarmDateTime=@AlarmDateTime,");
            strSql.Append("FiberPosition=@FiberPosition,");
            strSql.Append("AlarmType=@AlarmType,");
            strSql.Append("MaxAmptitude=@MaxAmptitude,");
            strSql.Append("AlarmEndTime=@AlarmEndTime,");
            strSql.Append("AlarmLevel=@AlarmLevel,");
            strSql.Append("AlarmStatus=@AlarmStatus,");
            strSql.Append("ZoneName=@ZoneName,");
            strSql.Append("DealingUser=@DealingUser,");
            strSql.Append("DealingDesc=@DealingDesc,");
            strSql.Append("DealingDate=@DealingDate,");
            strSql.Append("DealingStatus=@DealingStatus,");
            strSql.Append("VCount=@VCount,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where AlarmInfoID=@AlarmInfoID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneID", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmDateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@FiberPosition", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmType", MySqlDbType.VarChar,30),
                    new MySqlParameter("@MaxAmptitude", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmEndTime", MySqlDbType.DateTime),
                    new MySqlParameter("@AlarmLevel", MySqlDbType.Int32),
                    new MySqlParameter("@AlarmStatus", MySqlDbType.Int16),
                    new MySqlParameter("@ZoneName", MySqlDbType.VarChar,30),
                    new MySqlParameter("@DealingUser", MySqlDbType.VarChar,20),
                    new MySqlParameter("@DealingDesc", MySqlDbType.VarChar,100),
                    new MySqlParameter("@DealingDate", MySqlDbType.DateTime),
                    new MySqlParameter("@DealingStatus", MySqlDbType.Int16),
                    new MySqlParameter("@VCount", MySqlDbType.Int32),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@AlarmInfoID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.ZoneID;
            parameters[4].Value = model.AlarmDateTime;
            parameters[5].Value = model.FiberPosition;
            parameters[6].Value = model.AlarmType;
            parameters[7].Value = model.MaxAmptitude;
            parameters[8].Value = model.AlarmEndTime;
            parameters[9].Value = model.AlarmLevel;
            parameters[10].Value = model.AlarmStatus;
            parameters[11].Value = model.ZoneName;
            parameters[12].Value = model.DealingUser;
            parameters[13].Value = model.DealingDesc;
            parameters[14].Value = model.DealingDate;
            parameters[15].Value = model.DealingStatus;
            parameters[16].Value = model.VCount;
            parameters[17].Value = model.UpdateDate;
            parameters[18].Value = model.AlarmInfoID;

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
            strSql.Append("select AlarmInfoID,CommandCode,DeviceID,ChannelID,ZoneID,AlarmDateTime,FiberPosition,AlarmType,MaxAmptitude,AlarmEndTime,AlarmLevel,AlarmStatus,ZoneName,DealingUser,DealingDesc,DealingDate,DealingStatus,VCount,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSAlarmInfoReport ");
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

            strSql.Append(" AlarmInfoID,CommandCode,DeviceID,ChannelID,ZoneID,AlarmDateTime,FiberPosition,AlarmType,MaxAmptitude,AlarmEndTime,AlarmLevel,AlarmStatus,ZoneName,DealingUser,DealingDesc,DealingDate,DealingStatus,VCount,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSAlarmInfoReport ");
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
        /// <param name="dataNumber">获取的数据条数</param>
		/// </summary>
		public static List<DiBo.Model.DVSHistoricalData> GetModels(int dataNumber)
        {
            List<DiBo.Model.DVSHistoricalData> models = new List<Model.DVSHistoricalData>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  AlarmInfoID,CommandCode,DeviceID,ChannelID,ZoneID,AlarmDateTime,FiberPosition,AlarmType,MaxAmptitude,AlarmEndTime,AlarmLevel,AlarmStatus,ZoneName,DealingUser,DealingDesc,DealingDate,DealingStatus,VCount,CreateDate,UpdateDate from DVSAlarmInfoReport order by AlarmInfoID desc limit 0,"+ dataNumber + "");

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
        public static DiBo.Model.DVSHistoricalData DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSHistoricalData model = new DiBo.Model.DVSHistoricalData();
            if (row != null)
            {
                if (row["AlarmInfoID"] != null && row["AlarmInfoID"].ToString() != "")
                {
                    model.AlarmInfoID = int.Parse(row["AlarmInfoID"].ToString());
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
                if (row["ZoneID"] != null && row["ZoneID"].ToString() != "")
                {
                    model.ZoneID = int.Parse(row["ZoneID"].ToString());
                }
                if (row["AlarmDateTime"] != null && row["AlarmDateTime"].ToString() != "")
                {
                    model.AlarmDateTime = DateTime.Parse(row["AlarmDateTime"].ToString());
                }
                if (row["FiberPosition"] != null && row["FiberPosition"].ToString() != "")
                {
                    model.FiberPosition = int.Parse(row["FiberPosition"].ToString());
                }
                if (row["AlarmType"] != null)
                {
                    model.AlarmType = row["AlarmType"].ToString();
                }
                if (row["MaxAmptitude"] != null && row["MaxAmptitude"].ToString() != "")
                {
                    model.MaxAmptitude = int.Parse(row["MaxAmptitude"].ToString());
                }
                if (row["AlarmEndTime"] != null && row["AlarmEndTime"].ToString() != "")
                {
                    model.AlarmEndTime = DateTime.Parse(row["AlarmEndTime"].ToString());
                }
                if (row["AlarmLevel"] != null && row["AlarmLevel"].ToString() != "")
                {
                    model.AlarmLevel = int.Parse(row["AlarmLevel"].ToString());
                }
                if (row["AlarmStatus"] != null && row["AlarmStatus"].ToString() != "")
                {
                    model.AlarmStatus = int.Parse(row["AlarmStatus"].ToString());
                }
                if (row["ZoneName"] != null)
                {
                    model.ZoneName = row["ZoneName"].ToString();
                }
                if (row["DealingUser"] != null)
                {
                    model.DealingUser = row["DealingUser"].ToString();
                }
                if (row["DealingDesc"] != null)
                {
                    model.DealingDesc = row["DealingDesc"].ToString();
                }
                if (row["DealingDate"] != null && row["DealingDate"].ToString() != "")
                {
                    model.DealingDate = DateTime.Parse(row["DealingDate"].ToString());
                }
                if (row["DealingStatus"] != null && row["DealingStatus"].ToString() != "")
                {
                    model.DealingStatus = int.Parse(row["DealingStatus"].ToString());
                }
                if (row["VCount"] != null && row["VCount"].ToString() != "")
                {
                    model.VCount = int.Parse(row["VCount"].ToString());
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["UpdateDate"] != null && row["UpdateDate"].ToString() != "")
                {
                    model.UpdateDate = DateTime.Parse(row["UpdateDate"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public static int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM DVSAlarmInfoReport ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.ExecuteScalar(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public static DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.AlarmInfoID desc");
            }
            strSql.Append(")AS Row, T.*  from DVSAlarmInfoReport T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }


       
        #endregion  BasicMethod
    }
}

