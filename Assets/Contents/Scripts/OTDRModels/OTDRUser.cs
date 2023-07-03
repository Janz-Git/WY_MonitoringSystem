using System;
namespace DiBo.Model
{
    /// <summary>
    /// OTDRUser:实体类(纵向沉降和形变数据类)
    /// </summary>
    [Serializable]
    public partial class OTDRUser
    {
        public OTDRUser()
        { }
        #region Model
        private int _i;
        private string _xaxisdata;
        private string _demodulateresult;
        private string _yaxisdata;
        private DateTime? _curtime;
        private string _distance;
        /// <summary>
        /// auto_increment
        /// </summary>
        public int i
        {
            set { _i = value; }
            get { return _i; }
        }
        /// <summary>
        /// 光纤距离/传感器位置
        /// </summary>
        public string XAxisData
        {
            set { _xaxisdata = value; }
            get { return _xaxisdata; }
        }
        /// <summary>
        /// 测温结果
        /// </summary>
        public string DemodulateResult
        {
            set { _demodulateresult = value; }
            get { return _demodulateresult; }
        }
        /// <summary>
        /// 布里渊频移，与沉降或形变成正比，通过该字段与设定阈值进行判断
        /// </summary>
        public string YAxisData
        {
            set { _yaxisdata = value; }
            get { return _yaxisdata; }
        }
        /// <summary>
        /// 系统当前时间
        /// </summary>
        public DateTime? Curtime
        {
            set { _curtime = value; }
            get { return _curtime; }
        }
        /// <summary>
        /// 当前信号在光纤传输的最远距离，为固定值，例如4500，当该值发生变化，那么说明光纤可能中断
        /// </summary>
        public string distance
        {
            set { _distance = value; }
            get { return _distance; }
        }
        #endregion Model

    }
}

