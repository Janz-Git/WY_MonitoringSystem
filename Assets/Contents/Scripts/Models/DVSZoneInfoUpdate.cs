using System;
using System.Collections.Generic;

namespace DiBo.CmdModel
{
    /// <summary>
    /// UpdateZoneInfo: 命令码2008：更新DVS分区参数（服务端主动）
    /// </summary>
    [Serializable]
    public partial class ZoneInfoUpdate
    {
        public ZoneInfoUpdate()
        { }
        #region Model
        private string _commandcode = "2008";
        private string _deviceid;
        private List<ZoneInfo> _zoneinfo;
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
        public List<ZoneInfo> ZoneInfo
        {
            set { _zoneinfo = value; }
            get { return _zoneinfo; }
        }
        #endregion Model

    }
}

