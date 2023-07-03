using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSZoneInfo
    /// </summary>
    public partial class DVSZoneInfo
    {
        public DVSZoneInfo()
        { }
        #region  BasicMethod

        public static List<Model.DVSZoneInfo> SaveDVSZoneInfo(int zoneInfoID, List<Model.DVSZoneInfo> dvsZoneInfos)
        {
            List<Model.DVSZoneInfo> modelList = new List<Model.DVSZoneInfo>();
            int count = dvsZoneInfos.Count;
            for (int i = 0; i < count; i++)
            {
                Model.DVSZoneInfo zoneInfo = dvsZoneInfos[i];

                Model.DVSZoneInfo modelZoneInfo = new Model.DVSZoneInfo();
                modelZoneInfo.ZoneInfoID = zoneInfoID;
                modelZoneInfo.ZoneID = zoneInfo.ZoneID;
                modelZoneInfo.ChannelID = zoneInfo.ChannelID;
                modelZoneInfo.ZoneName = zoneInfo.ZoneName;
                modelZoneInfo.FiberStart = zoneInfo.FiberStart;
                modelZoneInfo.FiberEnd = zoneInfo.FiberEnd;
                modelZoneInfo.Threshold = zoneInfo.Threshold;
                modelZoneInfo.IsAlarm = zoneInfo.IsAlarm;
                modelZoneInfo.DataType = zoneInfo.DataType;

                var obj2 = DbHelperMySQL.ExecuteScalar("select ZID from DVSZoneInfo where ZoneInfoID='" + zoneInfoID + "' and ZoneID='" + zoneInfo.ZoneID + "' and ChannelID='" + zoneInfo.ChannelID + "' and DataType='"+ zoneInfo.DataType + "'");
                if (obj2 != null)
                {
                    modelZoneInfo.ZID = int.Parse(obj2.ToString());

                    Update(modelZoneInfo);
                }
                else
                {
                    modelZoneInfo.ZID = Add(modelZoneInfo);
                }
                modelList.Add(modelZoneInfo);
            }
            return modelList;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ZID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DVSZoneInfo");
            strSql.Append(" where ZID=@ZID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ZID", MySqlDbType.Int32)
            };
            parameters[0].Value = ZID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(DiBo.Model.DVSZoneInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvszoneinfo(");
            strSql.Append("ZoneInfoID,ZoneID,ChannelID,ZoneName,FiberStart,FiberEnd,Threshold,IsAlarm,DataType,CreateDate,UpdateDate)");
            strSql.Append(" values (");
            strSql.Append("@ZoneInfoID,@ZoneID,@ChannelID,@ZoneName,@FiberStart,@FiberEnd,@Threshold,@IsAlarm,@DataType,@CreateDate,@UpdateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ZoneInfoID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneID", MySqlDbType.Int32),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneName", MySqlDbType.VarChar,50),
                    new MySqlParameter("@FiberStart", MySqlDbType.Int32),
                    new MySqlParameter("@FiberEnd", MySqlDbType.Int32),
                    new MySqlParameter("@Threshold", MySqlDbType.Int32),
                    new MySqlParameter("@IsAlarm", MySqlDbType.Int16),
                    new MySqlParameter("@DataType", MySqlDbType.Int16),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.ZoneInfoID;
            parameters[1].Value = model.ZoneID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.ZoneName;
            parameters[4].Value = model.FiberStart;
            parameters[5].Value = model.FiberEnd;
            parameters[6].Value = model.Threshold;
            parameters[7].Value = model.IsAlarm;
            parameters[8].Value = model.DataType;
            parameters[9].Value = model.CreateDate;
            parameters[10].Value = model.UpdateDate;

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
		public static bool Update(DiBo.Model.DVSZoneInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvszoneinfo set ");
            strSql.Append("ZoneInfoID=@ZoneInfoID,");
            strSql.Append("ZoneID=@ZoneID,");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("ZoneName=@ZoneName,");
            strSql.Append("FiberStart=@FiberStart,");
            strSql.Append("FiberEnd=@FiberEnd,");
            strSql.Append("Threshold=@Threshold,");
            strSql.Append("IsAlarm=@IsAlarm,");
            strSql.Append("DataType=@DataType,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where ZID=@ZID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ZoneInfoID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneID", MySqlDbType.Int32),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@ZoneName", MySqlDbType.VarChar,50),
                    new MySqlParameter("@FiberStart", MySqlDbType.Int32),
                    new MySqlParameter("@FiberEnd", MySqlDbType.Int32),
                    new MySqlParameter("@Threshold", MySqlDbType.Int32),
                    new MySqlParameter("@IsAlarm", MySqlDbType.Int16),
                    new MySqlParameter("@DataType", MySqlDbType.Int16),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@ZID", MySqlDbType.Int32)};
            parameters[0].Value = model.ZoneInfoID;
            parameters[1].Value = model.ZoneID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.ZoneName;
            parameters[4].Value = model.FiberStart;
            parameters[5].Value = model.FiberEnd;
            parameters[6].Value = model.Threshold;
            parameters[7].Value = model.IsAlarm;
            parameters[8].Value = model.DataType;
            parameters[9].Value = model.UpdateDate;
            parameters[10].Value = model.ZID;

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
            strSql.Append("select ZID,ZoneInfoID,ZoneID,ChannelID,ZoneName,FiberStart,FiberEnd,Threshold,IsAlarm,DataType,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSZoneInfo ");
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
            strSql.Append(" ZID,ZoneInfoID,ZoneID,ChannelID,ZoneName,FiberStart,FiberEnd,Threshold,IsAlarm,DataType,CreateDate,UpdateDate ");
            strSql.Append(" FROM DVSZoneInfo ");
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
        public static List<DiBo.Model.DVSZoneInfo> GetModel(string strWhere)
        {
            List<DiBo.Model.DVSZoneInfo> models=new List<Model.DVSZoneInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ZID,ZoneInfoID,ZoneID,ChannelID,ZoneName,FiberStart,FiberEnd,Threshold,IsAlarm,DataType,CreateDate,UpdateDate from DVSZoneInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

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
        public static DiBo.Model.DVSZoneInfo DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSZoneInfo model = new DiBo.Model.DVSZoneInfo();
            if (row != null)
            {
                if (row["ZID"] != null && row["ZID"].ToString() != "")
                {
                    model.ZID = int.Parse(row["ZID"].ToString());
                }
                if (row["ZoneInfoID"] != null && row["ZoneInfoID"].ToString() != "")
                {
                    model.ZoneInfoID = int.Parse(row["ZoneInfoID"].ToString());
                }
                if (row["ZoneID"] != null && row["ZoneID"].ToString() != "")
                {
                    model.ZoneID = int.Parse(row["ZoneID"].ToString());
                }
                if (row["ChannelID"] != null && row["ChannelID"].ToString() != "")
                {
                    model.ChannelID = int.Parse(row["ChannelID"].ToString());
                }
                if (row["ZoneName"] != null)
                {
                    model.ZoneName = row["ZoneName"].ToString();
                }
                if (row["FiberStart"] != null && row["FiberStart"].ToString() != "")
                {
                    model.FiberStart = int.Parse(row["FiberStart"].ToString());
                }
                if (row["FiberEnd"] != null && row["FiberEnd"].ToString() != "")
                {
                    model.FiberEnd = int.Parse(row["FiberEnd"].ToString());
                }
                if (row["Threshold"] != null && row["Threshold"].ToString() != "")
                {
                    model.Threshold = int.Parse(row["Threshold"].ToString());
                }
                if (row["IsAlarm"] != null && row["IsAlarm"].ToString() != "")
                {
                    model.IsAlarm = int.Parse(row["IsAlarm"].ToString());
                }
                if (row["DataType"] != null && row["DataType"].ToString() != "")
                {
                    model.DataType = int.Parse(row["DataType"].ToString());
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

        #endregion  BasicMethod
    }
}

