using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DotNet.FrameWork.Data;

namespace DiBo.DAL
{
    /// <summary>
    /// 数据访问类:FBGInfo
    /// </summary>
    public partial class FBGInfo
    {
        public FBGInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(DiBo.Model.FBGInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into fbginfo(");
            strSql.Append("channel_count,raw_data,real_data,data_type,status,dealing_user,dealing_desc,dealing_date,dealing_status,create_date,update_date)");
            strSql.Append(" values (");
            strSql.Append("@channel_count,@raw_data,@real_data,@data_type,@status,@dealing_user,@dealing_desc,@dealing_date,@dealing_status,now(),now())");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@channel_count", MySqlDbType.Int32),
                    new MySqlParameter("@raw_data", MySqlDbType.Text),
                    new MySqlParameter("@real_data", MySqlDbType.Text),
                    new MySqlParameter("@data_type", MySqlDbType.Int16),
                    new MySqlParameter("@status", MySqlDbType.Int16),
                    new MySqlParameter("@dealing_user", MySqlDbType.VarChar,20),
                    new MySqlParameter("@dealing_desc", MySqlDbType.VarChar,100),
                    new MySqlParameter("@dealing_date", MySqlDbType.DateTime),
                    new MySqlParameter("@dealing_status", MySqlDbType.Int16)};
            parameters[0].Value = model.channel_count;
            parameters[1].Value = model.raw_data;
            parameters[2].Value = model.real_data;
            parameters[3].Value = model.data_type;
            parameters[4].Value = model.status;
            parameters[5].Value = model.dealing_user;
            parameters[6].Value = model.dealing_desc;
            parameters[7].Value = model.dealing_date;
            parameters[8].Value = model.dealing_status;

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
        public static bool Update(DiBo.Model.FBGInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update fbginfo set ");
            strSql.Append("status=@status,");
            strSql.Append("dealing_user=@dealing_user,");
            strSql.Append("dealing_desc=@dealing_desc,");
            strSql.Append("dealing_date=@dealing_date,");
            strSql.Append("dealing_status=@dealing_status,");
            strSql.Append("update_date=now()");
            strSql.Append(" where info_id=@info_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@status", MySqlDbType.Int16),
                    new MySqlParameter("@dealing_user", MySqlDbType.VarChar,20),
                    new MySqlParameter("@dealing_desc", MySqlDbType.VarChar,100),
                    new MySqlParameter("@dealing_date", MySqlDbType.DateTime),
                    new MySqlParameter("@dealing_status", MySqlDbType.Int16),
                    new MySqlParameter("@info_id", MySqlDbType.Int32)};
            parameters[0].Value = model.status;
            parameters[1].Value = model.dealing_user;
            parameters[2].Value = model.dealing_desc;
            parameters[3].Value = model.dealing_date;
            parameters[4].Value = model.dealing_status;
            parameters[5].Value = model.info_id;

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
        public static DiBo.Model.FBGInfo GetModel(int info_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select info_id,channel_count,raw_data,real_data,data_type,status,dealing_user,dealing_desc,dealing_date,dealing_status,create_date,update_date from fbginfo ");
            strSql.Append(" where info_id=@info_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@info_id", MySqlDbType.Int32)
            };
            parameters[0].Value = info_id;

            DiBo.Model.FBGInfo model = new DiBo.Model.FBGInfo();
            DataTable dtbl = DbHelperMySQL.ExecuteDataTable(strSql.ToString(), parameters);
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
        public static DiBo.Model.FBGInfo DataRowToModel(DataRow row)
        {
            DiBo.Model.FBGInfo model = new DiBo.Model.FBGInfo();
            if (row != null)
            {
                if (row["info_id"] != null && row["info_id"].ToString() != "")
                {
                    model.info_id = int.Parse(row["info_id"].ToString());
                }
                if (row["channel_count"] != null && row["channel_count"].ToString() != "")
                {
                    model.channel_count = int.Parse(row["channel_count"].ToString());
                }
                if (row["raw_data"] != null)
                {
                    model.raw_data = row["raw_data"].ToString();
                }
                if (row["real_data"] != null)
                {
                    model.real_data = row["real_data"].ToString();
                }
                if (row["data_type"] != null && row["data_type"].ToString() != "")
                {
                    model.data_type = int.Parse(row["data_type"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["dealing_user"] != null)
                {
                    model.dealing_user = row["dealing_user"].ToString();
                }
                if (row["dealing_desc"] != null)
                {
                    model.dealing_desc = row["dealing_desc"].ToString();
                }
                if (row["dealing_date"] != null && row["dealing_date"].ToString() != "")
                {
                    model.dealing_date = DateTime.Parse(row["dealing_date"].ToString());
                }
                if (row["dealing_status"] != null && row["dealing_status"].ToString() != "")
                {
                    model.dealing_status = int.Parse(row["dealing_status"].ToString());
                }
                if (row["create_date"] != null && row["create_date"].ToString() != "")
                {
                    model.create_date = DateTime.Parse(row["create_date"].ToString());
                }
                if (row["update_date"] != null && row["update_date"].ToString() != "")
                {
                    model.update_date = DateTime.Parse(row["update_date"].ToString());
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
            strSql.Append("select info_id,channel_count,raw_data,real_data,data_type,status,dealing_user,dealing_desc,dealing_date,dealing_status,create_date,update_date ");
            strSql.Append(" FROM fbginfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public static int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM fbginfo ");
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
                strSql.Append("order by T.info_id desc");
            }
            strSql.Append(")AS Row, T.*  from fbginfo T ");
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

