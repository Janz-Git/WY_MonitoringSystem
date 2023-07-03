using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSZoneInfoReturn
    /// </summary>
    public partial class DVSZoneInfoReturn
    {
        public DVSZoneInfoReturn()
        { }

        public static Model.DVSZoneInfoReturn SaveDVSZoneInfoReturn(Model.DVSZoneInfoReturn zoneInfoReturn)
        {
            Model.DVSZoneInfoReturn model = zoneInfoReturn;

            var obj = DbHelperMySQL.ExecuteScalar("select ZoneInfoID from DVSZoneInfoReturn where DeviceID='" + zoneInfoReturn.DeviceID + "'");
            if (obj != null)
            {
                model.ZoneInfoID = int.Parse(obj.ToString());
                Update(model);
            }
            else
            {
                model.ZoneInfoID = Add(model);
            }

            model.ZoneInfo = DAL.DVSZoneInfo.SaveDVSZoneInfo(model.ZoneInfoID, zoneInfoReturn.ZoneInfo);

            return model;
        }


        #region  BasicMethod

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(DiBo.Model.DVSZoneInfoReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvszoneinforeturn(");
            strSql.Append("CommandCode,DeviceID,CreateDate,UpdateDate)");
            strSql.Append(" values (");
            strSql.Append("@CommandCode,@DeviceID,@CreateDate,@UpdateDate)");
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
		public static bool Update(DiBo.Model.DVSZoneInfoReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvszoneinforeturn set ");
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
        public static DiBo.Model.DVSZoneInfoReturn GetModel(string strWhere="")
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ZoneInfoID,CommandCode,DeviceID,CreateDate,UpdateDate from DVSZoneInfoReturn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ZoneInfoID desc limit 0,1 ");

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
        public static DiBo.Model.DVSZoneInfoReturn DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSZoneInfoReturn model = new DiBo.Model.DVSZoneInfoReturn();
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
                    model.ZoneInfo = DAL.DVSZoneInfo.GetModel("ZoneInfoID = '" + model.ZoneInfoID + "' and DataType='0'");
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

