using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:OTDRUser，使用DB2，使用DB2，使用DB2
    /// </summary>
    public partial class OTDRUser
    {

        public OTDRUser()
        { }
        #region  BasicMethod

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static bool Add(DiBo.Model.OTDRUser model,string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into `" + tableName + "`(");
            strSql.Append("XAxisData,DemodulateResult,YAxisData,Curtime,distance)");
            strSql.Append(" values (");
            strSql.Append("@XAxisData,@DemodulateResult,@YAxisData,@Curtime,@distance)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@XAxisData", MySqlDbType.VarChar,255),
                    new MySqlParameter("@DemodulateResult", MySqlDbType.VarChar,255),
                    new MySqlParameter("@YAxisData", MySqlDbType.VarChar,255),
                    new MySqlParameter("@Curtime", MySqlDbType.DateTime),
                    new MySqlParameter("@distance", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.XAxisData;
            parameters[1].Value = model.DemodulateResult;
            parameters[2].Value = model.YAxisData;
            parameters[3].Value = model.Curtime;
            parameters[4].Value = model.distance;

            //使用 DB2，DB2，DB2
            int rows = DbHelperMySQL2.ExecuteNonQuery(strSql.ToString(), parameters);
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
        /// 得到一个对象实体列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">不带 where 的查询条件</param>
        /// <param name="order">不带 order by 的排序字段名，例如 create_date desc</param>
        /// <returns></returns>
        public static List<DiBo.Model.OTDRUser> GetModel(string tableName, string strWhere,string order="")
        {
            List<DiBo.Model.OTDRUser> models = new List<Model.OTDRUser>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select i,XAxisData,DemodulateResult,YAxisData,Curtime,distance from `" + tableName + "` ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (order.Trim() != "")
            {
                strSql.Append(" order by " + order);
            }

            //******************** 使用DB2 ********************
            DataTable dtbl = DbHelperMySQL2.ExecuteDataTable(strSql.ToString());

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
        public static DiBo.Model.OTDRUser DataRowToModel(DataRow row)
        {
            DiBo.Model.OTDRUser model = new DiBo.Model.OTDRUser();
            if (row != null)
            {
                if (row["i"] != null && row["i"].ToString() != "")
                {
                    model.i = int.Parse(row["i"].ToString());
                }
                if (row["XAxisData"] != null)
                {
                    model.XAxisData = row["XAxisData"].ToString();
                }
                if (row["DemodulateResult"] != null)
                {
                    model.DemodulateResult = row["DemodulateResult"].ToString();
                }
                if (row["YAxisData"] != null)
                {
                    model.YAxisData = row["YAxisData"].ToString();
                }
                if (row["Curtime"] != null && row["Curtime"].ToString() != "")
                {
                    model.Curtime = DateTime.Parse(row["Curtime"].ToString());
                }
                if (row["distance"] != null)
                {
                    model.distance = row["distance"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetList(string tableName, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select i,XAxisData,DemodulateResult,YAxisData,Curtime,distance from `" + tableName + "` ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL2.ExecuteDataTable(strSql.ToString());
        }

        #endregion  BasicMethod
    }
}


