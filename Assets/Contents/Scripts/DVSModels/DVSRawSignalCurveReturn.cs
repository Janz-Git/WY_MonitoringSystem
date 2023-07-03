using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSRawSignalCurveReturn: 1004 返回原始信号曲线（客户端被动）
    /// </summary>
    [Serializable]
    public partial class DVSRawSignalCurveReturn
    {
        public DVSRawSignalCurveReturn()
        { }
        #region Model
        private int _signalid;
        private string _commandcode = "1004";
        private string _deviceid;
        private int? _channelid;
        private int? _curvelength;
        private decimal? _resolution;
        private string _curvedata;
        private DateTime _createdate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int SignalID
        {
            set { _signalid = value; }
            get { return _signalid; }
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
        public string CurveData
        {
            set { _curvedata = value; }
            get { return _curvedata; }
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

