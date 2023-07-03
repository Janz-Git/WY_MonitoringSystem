using System;
using System.Collections.Generic;

namespace DiBo.CmdModel
{
    /// <summary>
    /// ZoneInfoReturn: 命令码1007：返回DVS分区参数（客户端被动）
    /// </summary>
    [Serializable]
    public partial class ZoneInfoReturn
    {
        public ZoneInfoReturn()
        { }
        #region Model
        private string _commandcode = "1007";
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
