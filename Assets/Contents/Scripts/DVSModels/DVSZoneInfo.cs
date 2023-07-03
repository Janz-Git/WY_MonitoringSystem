using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSZoneInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DVSZoneInfo
    {
        public DVSZoneInfo()
        { }
        #region Model
        private int _zid;
        private int? _zoneinfoid;
        private int? _zoneid;
        private int? _channelid;
        private string _zonename;
        private int? _fiberstart;
        private int? _fiberend;
        private int? _threshold;
        private int? _isalarm;
        private int? _datatype=0;
        private DateTime _createdate = DateTime.Now;
        private DateTime _updatedate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int ZID
        {
            set { _zid = value; }
            get { return _zid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ZoneInfoID
        {
            set { _zoneinfoid = value; }
            get { return _zoneinfoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ZoneID
        {
            set { _zoneid = value; }
            get { return _zoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChannelID
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZoneName
        {
            set { _zonename = value; }
            get { return _zonename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberStart
        {
            set { _fiberstart = value; }
            get { return _fiberstart; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberEnd
        {
            set { _fiberend = value; }
            get { return _fiberend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Threshold
        {
            set { _threshold = value; }
            get { return _threshold; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsAlarm
        {
            set { _isalarm = value; }
            get { return _isalarm; }
        }
        /// <summary>
        /// 1007 的子数据类型为0 , 2008 的子数据类型为1
        /// </summary>
        public int? DataType
        {
            set { _datatype = value; }
            get { return _datatype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        #endregion Model

    }
}

