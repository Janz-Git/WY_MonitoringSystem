using System;
using System.Data;
using System.Text;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;

public class UserManager
{

    public static int UserID;
    public static int UserLevel;
    public static string UserName;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public static bool Login(string userName, string password)
    {
        //密码加密
        string pwdEncrypt = CommonFunc.MD5Encrypt32(password).Substring(0, 15);

        StringBuilder strSql = new StringBuilder();
        strSql.Append("select UserID,UserLevel,UserName from DVSUserInfo");
        strSql.Append(" where UserName=@UserName and UserPassword=@UserPassword");
        MySqlParameter[] parameters = {
                    new MySqlParameter("@UserName", MySqlDbType.VarChar,50),
                    new MySqlParameter("@UserPassword", MySqlDbType.VarChar,50)
            };
        parameters[0].Value = userName;
        parameters[1].Value = pwdEncrypt;

        bool blExists = false;
        DataTable dtbl = DbHelperMySQL.ExecuteDataTable(strSql.ToString(), parameters);
        if (dtbl.Rows.Count > 0)
        {
            UserID = int.Parse(dtbl.Rows[0]["UserID"].ToString());
            UserLevel = int.Parse(dtbl.Rows[0]["UserLevel"].ToString());
            UserName = userName;
            blExists = true;
        }

        return blExists;
    }

    public static bool Register(out string result, string name, string password, string phoneNumber)
    {
        if (DiBo.DAL.DVSUserInfo.Exists(name))
        {
            result = "用户名已存在";
            return false;
        }

        //密码加密之后再进行截取
        string pwdEncrypt = CommonFunc.MD5Encrypt32(password).Substring(0, 15);

        DiBo.Model.DVSUserInfo model = new DiBo.Model.DVSUserInfo();
        model.UserLevel = 1;//0超级管理员，1管理员，通过程序注册的只能是管理员。
        model.UserName = name.Trim();
        model.UserPassword = pwdEncrypt;
        model.UserPhone = phoneNumber.Trim();
        model.UserRegDate = DateTime.Now;

        bool bl = false;
        string msg = "";
        try
        {
            bl = DiBo.DAL.DVSUserInfo.Add(model);
        }
        catch (Exception ex)
        {
            msg ="注册失败。"+ ex.Message;
        }

        if (bl)
        {
            result = "注册成功";
            return true;
        }
        else
        {
            result = msg;
            return false;
        }
    }

    public static bool UpdatePassword(out string result, string name, string oldPwd,string newPwd)
    {
        string pwdEncrypt = CommonFunc.MD5Encrypt32(oldPwd).Substring(0, 15);
        int userId = -1;
        bool bl=DiBo.DAL.DVSUserInfo.Exists(out userId, name, pwdEncrypt);
        if (bl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dvsuserinfo set ");
            strSql.Append("UserPassword=@UserPassword,");
            strSql.Append(" where UserID=@UserID");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@UserPassword", MySqlDbType.VarChar,100),
                    new MySqlParameter("@UserID", MySqlDbType.Int32)};
            parameters[0].Value = CommonFunc.MD5Encrypt32(newPwd).Substring(0, 15);
            parameters[1].Value = userId;

            int rows = DbHelperMySQL.ExecuteNonQuery(strSql.ToString(), parameters);
            result = "修改成功";
            return true;
        }
        else
        {
            result = "用户名或密码错误";
            return false;
        }
    }
}
