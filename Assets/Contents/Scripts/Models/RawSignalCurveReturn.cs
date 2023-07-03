using System;
using System.Collections.Generic;

namespace DiBo.CmdModel
{
    /// <summary>
    /// RawSignalCurveReturn: 命令码1004：返回原始信号曲线（客户端被动）
    /// </summary>
    [Serializable]
    public partial class RawSignalCurveReturn
    {
        public RawSignalCurveReturn()
        { }
        #region Model
        private string _commandcode = "1004";
        private string _deviceid;
        private int? _channelid;
        private int? _curvelength;
        private decimal? _resolution;
        private List<int> _curvedata;
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
        public int? CurveLength
        {
            set { _curvelength = value; }
            get { return _curvelength; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Resolution
        {
            set { _resolution = value; }
            get { return _resolution; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> CurveData
        {
            set { _curvedata = value; }
            get { return _curvedata; }
        }
        #endregion Model

    }
}

