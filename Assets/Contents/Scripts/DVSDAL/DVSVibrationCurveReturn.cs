using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSCurveReturn
    /// </summary>
    public partial class DVSVibrationCurveReturn
    {
        public DVSVibrationCurveReturn()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(DiBo.Model.DVSVibrationCurveReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvsvibrationcurvereturn(");
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
		public static bool Update(DiBo.Model.DVSVibrationCurveReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvsvibrationcurvereturn set ");
            strSql.Append("CommandCode=@CommandCode,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("ChannelID=@ChannelID,");
            strSql.Append("CurveLength=@CurveLength,");
            strSql.Append("Resolution=@Resolution,");
            strSql.Append("CurveData=@CurveData,");
            strSql.Append("CreateDate=@CreateDate");
            strSql.Append(" where CurveID=@CurveID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CommandCode", MySqlDbType.VarChar,10),
                    new MySqlParameter("@DeviceID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@ChannelID", MySqlDbType.Int32),
                    new MySqlParameter("@CurveLength", MySqlDbType.Int32),
                    new MySqlParameter("@Resolution", MySqlDbType.Float),
                    new MySqlParameter("@CurveData", MySqlDbType.Text),
                    new MySqlParameter("@CreateDate", MySqlDbType.DateTime),
                    new MySqlParameter("@CurveID", MySqlDbType.Int32)};
            parameters[0].Value = model.CommandCode;
            parameters[1].Value = model.DeviceID;
            parameters[2].Value = model.ChannelID;
            parameters[3].Value = model.CurveLength;
            parameters[4].Value = model.Resolution;
            parameters[5].Value = model.CurveData;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CurveID;

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
            strSql.Append("select CurveID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate ");
            strSql.Append(" FROM DVSVibrationCurveReturn ");
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
            strSql.Append(" CurveID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate ");
            strSql.Append(" FROM DVSVibrationCurveReturn ");
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
		public static List<DiBo.Model.DVSVibrationCurveReturn> GetModel(int dataNumber)
        {
            List<DiBo.Model.DVSVibrationCurveReturn> models = new List<Model.DVSVibrationCurveReturn>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurveID,CommandCode,DeviceID,ChannelID,CurveLength,Resolution,CurveData,CreateDate from DVSVibrationCurveReturn order by CurveID desc limit 0,"+ dataNumber + "");

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
        public static DiBo.Model.DVSVibrationCurveReturn DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSVibrationCurveReturn model = new DiBo.Model.DVSVibrationCurveReturn();
            if (row != null)
            {
                if (row["CurveID"] != null && row["CurveID"].ToString() != "")
                {
                    model.CurveID = int.Parse(row["CurveID"].ToString());
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


