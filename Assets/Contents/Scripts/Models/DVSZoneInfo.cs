using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// DVSZoneInfo: DVS分区信息
    /// </summary>
    [Serializable]
    public partial class ZoneInfo
    {
        public ZoneInfo()
        { }
        #region Model
        private int? _zoneid;
        private int? _channelid;
        private string _zonename;
        private int? _fiberstart;
        private int? _fiberend;
        private int? _threshold;
        private int? _isalarm;
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
        #endregion Model

    }
}

