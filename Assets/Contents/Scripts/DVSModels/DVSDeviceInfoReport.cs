using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSDeviceInfoReport: 1001 设备信息上报（客户端主动）
    /// </summary>
    [Serializable]
    public partial class DVSDeviceInfoReport
    {
        public DVSDeviceInfoReport()
        { }
        #region Model
        private int _deviceinfoid;
        private string _commandcode = "1001";
        private string _deviceid;
        private string _devicename;
        private string _version;
        private int? _channelcount;
        private int? _statusok;
        private DateTime _createdate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int DeviceInfoID
        {
            set { _deviceinfoid = value; }
            get { return _deviceinfoid; }
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
        public string DeviceName
        {
            set { _devicename = value; }
            get { return _devicename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ChannelCount
        {
            set { _channelcount = value; }
            get { return _channelcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusOK
        {
            set { _statusok = value; }
            get { return _statusok; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

    }
}

