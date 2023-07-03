using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// BaseCurve: 获取振动曲线，获取原始信号曲线 基类
    /// </summary>
    [Serializable]
    public partial class BaseCurve
    {
        public BaseCurve()
        { }
        #region Model
        private string _commandcode;
        private string _deviceid;
        private int? _channelid;
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
        public int? ChannelID
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        #endregion Model

    }
}