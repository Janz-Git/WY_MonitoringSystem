using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSZoneInfoUpdate
    /// </summary>
    public partial class DVSZoneInfoUpdate
    {
        public DVSZoneInfoUpdate()
        { }
        #region  BasicMethod

        public static Model.DVSZoneInfoUpdate SaveDVSZoneInfoUpdate(Model.DVSZoneInfoUpdate zoneInfoUpdate)
        {
            Model.DVSZoneInfoUpdate model = zoneInfoUpdate;

            var obj = DbHelperMySQL.ExecuteScalar("select ZoneInfoID from DVSZoneInfoUpdate where DeviceID='" + zoneInfoUpdate.DeviceID + "'");
            if (obj != null)
            {
                model.ZoneInfoID = int.Parse(obj.ToString());
                Update(model);
            }
            else
            {
                model.ZoneInfoID = Add(model);
            }

            model.ZoneInfo = DAL.DVSZoneInfo.SaveDVSZoneInfo(model.ZoneInfoID, zoneInfoUpdate.ZoneInfo);

            return model;
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(DiBo.Model.DVSZoneInfoUpdate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvszoneinfoupdate(");
            strSql.Append("CommandCode,DeviceID,CreateDate,UpdateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@CreateDate,@UpdateDate)");
            strSql.Append(";SELECT LAST_INSERT_ID()");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.CreateDate;
            parameters[3].Value = model.UpdateDate;

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
		public static bool Update(DiBo.Model.DVSZoneInfoUpdate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvszoneinfoupdate set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("UpdateDate=@UpdateDate");
            strSql.Append(" where ZoneInfoID=@ZoneInfoID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@UpdateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@ZoneInfoID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.UpdateDate;
            parameters[3].Value = model.ZoneInfoID;

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
        /// 得到一个对象实体
        /// </summary>
        public static DiBo.Model.DVSZoneInfoUpdate GetModel(string strWhere= "")
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ZoneInfoID,CommandCode,DeviceID,CreateDate,UpdateDate from DVSZoneInfoUpdate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ZoneInfoID desc limit 0,1");

            DataTable dtbl = DbHelperMySQL.ExecuteDataTable(strSql.ToString());
            if (dtbl.Rows.Count > 0)
            {
                return DataRowToModel(dtbl.Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DiBo.Model.DVSZoneInfoUpdate DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSZoneInfoUpdate model = new DiBo.Model.DVSZoneInfoUpdate();
            if (row != null)
            {
                if (row["ZoneInfoID"] != null && row["ZoneInfoID"].ToString() != "")
                {
                    model.ZoneInfoID = int.Parse(row["ZoneInfoID"].ToString());
                }
                if (row["CommandCode"] != null)
                {
                    model.CommandCode = row["CommandCode"].ToString();
                }
                if (row["DeviceID"] != null)
                {
                    model.DeviceID = row["DeviceID"].ToString();
                }
                if (model.ZoneInfoID != 0)
                {
                    model.ZoneInfo = DAL.DVSZoneInfo.GetModel("ZoneInfoID = '" + model.ZoneInfoID + "' and DataType='1'");
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

