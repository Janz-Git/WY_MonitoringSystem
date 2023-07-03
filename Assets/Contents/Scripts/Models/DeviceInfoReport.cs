using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// DeviceInfoReport: 命令码1001：设备信息上报（客户端主动）
    /// </summary>
    [Serializable]
    public partial class DeviceInfoReport
    {
        public DeviceInfoReport()
        { }
        #region Model
        private string _commandcode = "1001";
        private string _deviceid;
        private string _devicename;
        private string _version;
        private int? _channelcount;
        private int? _statusok;
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
        #endregion Model

    }
}

