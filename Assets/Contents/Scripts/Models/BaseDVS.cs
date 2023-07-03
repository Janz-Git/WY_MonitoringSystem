using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// BaseDVS: 获取DVS设备基本参数，获取DVS分区信息 基类
    /// </summary>
    public partial class BaseDVS
    {
        public BaseDVS()
        { }
        #region Model
        private string _commandcode;
        private string _deviceid;
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
        #endregion Model

    }
}