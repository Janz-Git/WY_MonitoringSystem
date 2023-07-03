using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSUserInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DVSUserInfo
    {
        public DVSUserInfo()
        { }
        #region Model
        private int _userid;
        private int? _userlevel;
        private string _username;
        private string _userpassword;
        private string _userphone;
        private string _userdescription;
        private DateTime _userregdate = DateTime.Now;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? UserLevel
        {
            set { _userlevel = value; }
            get { return _userlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPhone
        {
            set { _userphone = value; }
            get { return _userphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserDescription
        {
            set { _userdescription = value; }
            get { return _userdescription; }
        }
        /// <summary>
        /// DEFAULT_GENERATED
        /// </summary>
        public DateTime UserRegDate
        {
            set { _userregdate = value; }
            get { return _userregdate; }
        }
        #endregion Model

    }

}