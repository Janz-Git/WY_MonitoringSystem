using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

namespace DiBo.DAL
{
    /// <summary>
    /// ���ݷ�����:OTDRUser��ʹ��DB2��ʹ��DB2��ʹ��DB2
    /// </summary>
    public partial class OTDRUser
    {

        public OTDRUser()
        { }
        #region  BasicMethod

        /// <summary>
		/// ����һ������
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

            //ʹ�� DB2��DB2��DB2
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
        /// �õ�һ������ʵ���б�
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="strWhere">���� where �Ĳ�ѯ����</param>
        /// <param name="order">���� order by �������ֶ��������� create_date desc</param>
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

            //******************** ʹ��DB2 ********************
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
        /// �õ�һ������ʵ��
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
        /// ��������б�
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


