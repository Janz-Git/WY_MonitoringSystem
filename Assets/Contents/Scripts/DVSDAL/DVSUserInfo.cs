using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DotNet.FrameWork.Data;
using System.Collections.Generic;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:DVSUserInfo
    /// </summary>
    public partial class DVSUserInfo
    {
        public DVSUserInfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dvsuserinfo");
            strSql.Append(" where UserName=@UserName");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserName", MySqlDbType.VarChar,100)
            };
            parameters[0].Value = UserName;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(out int UserID,string UserName,string Password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID from dvsuserinfo");
            strSql.Append(" where UserName=@UserName and UserPassword=@UserPassword");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserName", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserPassword", MySqlDbType.VarChar,100)
            };
            parameters[0].Value = UserName;
            parameters[1].Value = Password;

            var obj=DbHelperMySQL.ExecuteScalar(strSql.ToString(), parameters);

            if (obj == null)
            {
                UserID = -1;
                return false;
            }
            else
            {
                UserID = int.Parse(obj.ToString());
                return true;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(DiBo.Model.DVSUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dvsuserinfo(");
            strSql.Append("UserLevel,UserName,UserPassword,UserPhone,UserDescription,UserRegDate)");
            strSql.Append(" values (");
            strSql.Append("@UserLevel,@UserName,@UserPassword,@UserPhone,@UserDescription,@UserRegDate)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserLevel", MySqlDbType.Int32),
                    new MySqlParameter("@UserName", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserPassword", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserPhone", MySqlDbType.VarChar,20),
                    new MySqlParameter("@UserDescription", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserRegDate", MySqlDbType.DateTime)};
            parameters[0].Value = model.UserLevel;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserPassword;
            parameters[3].Value = model.UserPhone;
            parameters[4].Value = model.UserDescription;
            parameters[5].Value = model.UserRegDate;

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
        /// 更新一条数据
        /// </summary>
        public static bool Update(DiBo.Model.DVSUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvsuserinfo set ");
            strSql.Append("UserLevel=@UserLevel,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserPassword=@UserPassword,");
            strSql.Append("UserPhone=@UserPhone,");
            strSql.Append("UserDescription=@UserDescription,");
            strSql.Append("UserRegDate=@UserRegDate");
            strSql.Append(" where UserID=@UserID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserLevel", MySqlDbType.Int32),
                    new MySqlParameter("@UserName", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserPassword", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserPhone", MySqlDbType.VarChar,20),
                    new MySqlParameter("@UserDescription", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserRegDate", MySqlDbType.DateTime),
                    new MySqlParameter("@UserID", MySqlDbType.Int32)};
            parameters[0].Value = model.UserLevel;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserPassword;
            parameters[3].Value = model.UserPhone;
            parameters[4].Value = model.UserDescription;
            parameters[5].Value = model.UserRegDate;
            parameters[6].Value = model.UserID;

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
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dvsuserinfo ");
            strSql.Append(" where UserID=@UserID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserID", MySqlDbType.Int32)
            };
            parameters[0].Value = UserID;

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
        /// 批量删除数据
        /// </summary>
        public static bool DeleteList(string UserIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dvsuserinfo ");
            strSql.Append(" where UserID in (" + UserIDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteNonQuery(strSql.ToString());
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
        public static List<DiBo.Model.DVSUserInfo> GetModel(int UserID)
        {
            List<DiBo.Model.DVSUserInfo> models = new List<Model.DVSUserInfo>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,UserLevel,UserName,UserPassword,UserPhone,UserDescription,UserRegDate from dvsuserinfo ");
            strSql.Append(" where UserID=@UserID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserID", MySqlDbType.Int32)
            };
            parameters[0].Value = UserID;

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
        public static DiBo.Model.DVSUserInfo DataRowToModel(DataRow row)
        {
            DiBo.Model.DVSUserInfo model = new DiBo.Model.DVSUserInfo();
            if (row != null)
            {
                if (row["UserID"] != null && row["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["UserLevel"] != null && row["UserLevel"].ToString() != "")
                {
                    model.UserLevel = int.Parse(row["UserLevel"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["UserPassword"] != null)
                {
                    model.UserPassword = row["UserPassword"].ToString();
                }
                if (row["UserPhone"] != null)
                {
                    model.UserPhone = row["UserPhone"].ToString();
                }
                if (row["UserDescription"] != null)
                {
                    model.UserDescription = row["UserDescription"].ToString();
                }
                if (row["UserRegDate"] != null && row["UserRegDate"].ToString() != "")
                {
                    model.UserRegDate = DateTime.Parse(row["UserRegDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserID,UserLevel,UserName,UserPassword,UserPhone,UserDescription,UserRegDate ");
            strSql.Append(" FROM dvsuserinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        #endregion  BasicMethod

    }
}

