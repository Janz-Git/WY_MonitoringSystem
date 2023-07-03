using System;
namespace DiBo.Model
{
    /// <summary>
    /// FBGInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FBGInfo
    {
        public FBGInfo()
        { }
        #region Model
        private int _info_id;
        private int? _channel_count;
        private string _raw_data;
        private string _real_data;
        private int? _data_type;
        private int _status = 0;
        private string _dealing_user;
        private string _dealing_desc;
        private DateTime? _dealing_date;
        private int _dealing_status = 0;
        private DateTime _create_date = DateTime.Now;
        private DateTime _update_date = DateTime.Now;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int info_id
        {
            set { _info_id = value; }
            get { return _info_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? channel_count
        {
            set { _channel_count = value; }
            get { return _channel_count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string raw_data
        {
            set { _raw_data = value; }
            get { return _raw_data; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string real_data
        {
            set { _real_data = value; }
            get { return _real_data; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? data_type
        {
            set { _data_type = value; }
            get { return _data_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dealing_user
        {
            set { _dealing_user = value; }
            get { return _dealing_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dealing_desc
        {
            set { _dealing_desc = value; }
            get { return _dealing_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dealing_date
        {
            set { _dealing_date = value; }
            get { return _dealing_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int dealing_status
        {
            set { _dealing_status = value; }
            get { return _dealing_status; }
        }
        /// <summary>
        /// DEFAULT_GENERATED
        /// </summary>
        public DateTime create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        /// <summary>
        /// DEFAULT_GENERATED
        /// </summary>
        public DateTime update_date
        {
            set { _update_date = value; }
            get { return _update_date; }
        }
        #endregion Model

    }
}

