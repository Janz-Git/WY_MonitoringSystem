using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// AlarmInfoReport: 命令码1002：报警信息上报（客户端主动）
    /// </summary>
    [Serializable]
    public partial class AlarmInfoReport
    {
        public AlarmInfoReport()
        { }
        #region Model
        private string _commandcode = "1002";
        private string _deviceid;
        private int? _channelid;
        private int? _zoneid;
        private string _alarmdatetime;
        private int? _fiberposition;
        private string _alarmtype;
        private int? _maxamptitude;
        private string _alarmendtime;
        private int? _alarmlevel;
        private int? _alarmstatus;
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
        public string AlarmDateTime
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
        public string AlarmEndTime
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
        #endregion Model

    }
}

