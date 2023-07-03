using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// 设备基本参数
    /// </summary>
    public class BasicParams
    {
        public BasicParams()
        { }
        #region Model
        private string _commandcode;
        private string _deviceid;
        private int? _dmaid;
        private int? _totalpoints;
        private int? _framenumber;
        private int? _avgnumber;
        private decimal? _highpasshz;
        private decimal? _scanrate;
        private int? _pulsewidth;
        private int? _fiberlength1;
        private int? _fiberoffset1;
        private int? _noiselevel1;
        private int? _fiberlength2;
        private int? _fiberoffset2;
        private int? _noiselevel2;
        private int? _alarmresolution;
        private int? _minimumalarmcnt;
        private int? _durationcnt;
        private int? _dealingwind;
        private int? _windwindow;
        private int? _windminimumcnt;
        /// <summary>
        /// 
        /// </summary>
        public virtual string CommandCode
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
        public int? DMAID
        {
            set { _dmaid = value; }
            get { return _dmaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TotalPoints
        {
            set { _totalpoints = value; }
            get { return _totalpoints; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FrameNumber
        {
            set { _framenumber = value; }
            get { return _framenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AvgNumber
        {
            set { _avgnumber = value; }
            get { return _avgnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HighpassHz
        {
            set { _highpasshz = value; }
            get { return _highpasshz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Scanrate
        {
            set { _scanrate = value; }
            get { return _scanrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PulseWidth
        {
            set { _pulsewidth = value; }
            get { return _pulsewidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberLength1
        {
            set { _fiberlength1 = value; }
            get { return _fiberlength1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberOffset1
        {
            set { _fiberoffset1 = value; }
            get { return _fiberoffset1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Noiselevel1
        {
            set { _noiselevel1 = value; }
            get { return _noiselevel1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberLength2
        {
            set { _fiberlength2 = value; }
            get { return _fiberlength2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FiberOffset2
        {
            set { _fiberoffset2 = value; }
            get { return _fiberoffset2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Noiselevel2
        {
            set { _noiselevel2 = value; }
            get { return _noiselevel2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AlarmResolution
        {
            set { _alarmresolution = value; }
            get { return _alarmresolution; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MinimumAlarmCnt
        {
            set { _minimumalarmcnt = value; }
            get { return _minimumalarmcnt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DurationCnt
        {
            set { _durationcnt = value; }
            get { return _durationcnt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DealingWind
        {
            set { _dealingwind = value; }
            get { return _dealingwind; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WindWindow
        {
            set { _windwindow = value; }
            get { return _windwindow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WindMinimumCnt
        {
            set { _windminimumcnt = value; }
            get { return _windminimumcnt; }
        }
        #endregion Model

    }
}

