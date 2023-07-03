using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSAlarmInfoReport: 1002 报警信息上报（客户端主动）
    /// </summary>
    [Serializable]
    public partial class DVSAlarmInfoReport
    {
        public DVSAlarmInfoReport()
        { }
        #region Model
        private int _alarminfoid;
        private string _commandcode = "1002";
        private string _deviceid;
        private int? _channelid;
        private int? _zoneid;
        private DateTime? _alarmdatetime;
        private int? _fiberposition;
        private string _alarmtype;
        private int? _maxamptitude;
        private DateTime? _alarmendtime;
        private int? _alarmlevel;
        private int? _alarmstatus;
        private string _zonename;
        private string _dealinguser;
        private string _dealingdesc;
        private DateTime? _dealingdate;
        private int? _dealingstatus = 0;
        private int? _vcount;
        private DateTime _createdate = DateTime.Now;
        private DateTime _updatedate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int AlarmInfoID
        {
            set { _alarminfoid = value; }
            get { return _alarminfoid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CommandCode
        {
            set { _commandcode = value; }
            get { return _commandcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
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
        public int? ZoneID
        {
            set { _zoneid = value; }
            get { return _zoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AlarmDateTime
        {
            set { _alarmdatetime = value; }
            get { return _alarmdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberPosition
        {
            set { _fiberposition = value; }
            get { return _fiberposition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AlarmType
        {
            set { _alarmtype = value; }
            get { return _alarmtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MaxAmptitude
        {
            set { _maxamptitude = value; }
            get { return _maxamptitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AlarmEndTime
        {
            set { _alarmendtime = value; }
            get { return _alarmendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AlarmLevel
        {
            set { _alarmlevel = value; }
            get { return _alarmlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AlarmStatus
        {
            set { _alarmstatus = value; }
            get { return _alarmstatus; }
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
        public string DealingUser
        {
            set { _dealinguser = value; }
            get { return _dealinguser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DealingDesc
        {
            set { _dealingdesc = value; }
            get { return _dealingdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DealingDate
        {
            set { _dealingdate = value; }
            get { return _dealingdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DealingStatus
        {
            set { _dealingstatus = value; }
            get { return _dealingstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? VCount
        {
            set { _vcount = value; }
            get { return _vcount; }
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


