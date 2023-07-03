using DiBo.CmdModel;
using System;
using System.Collections.Generic;

namespace DiBo.Model
{
    /// <summary>
    /// DVSZoneInfoUpdate: 2008 更新DVS分区参数（服务端主动）
    /// </summary>
    [Serializable]
    public partial class DVSZoneInfoUpdate
    {
        public DVSZoneInfoUpdate()
        { }
        #region Model
        private int _zoneinfoid;
        private string _commandcode = "2008";
        private string _deviceid;
        private List<Model.DVSZoneInfo> _zoneinfo;
        private DateTime _createdate = DateTime.Now;
        private DateTime _updatedate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int ZoneInfoID
        {
            set { _zoneinfoid = value; }
            get { return _zoneinfoid; }
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
        public List<Model.DVSZoneInfo> ZoneInfo
        {
            set { _zoneinfo = value; }
            get { return _zoneinfo; }
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


