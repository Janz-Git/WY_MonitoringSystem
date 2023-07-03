using System;
namespace DiBo.Model
{
    /// <summary>
    /// DVSCurveReturn: 1003 返回振动曲线（客户端被动）
    /// </summary>
    [Serializable]
    public partial class DVSVibrationCurveReturn
    {
        public DVSVibrationCurveReturn()
        { }
        #region Model
        private int _curveid;
        private string _commandcode = "1003";
        private string _deviceid;
        private int? _channelid;
        private int? _curvelength;
        private decimal? _resolution;
        private string _curvedata;
        private DateTime _createdate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int CurveID
        {
            set { _curveid = value; }
            get { return _curveid; }
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

