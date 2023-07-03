using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// ZoneInfoGet: 命令码2007：获取DVS分区信息（服务端主动）
    /// </summary>
    [Serializable]
    public partial class ZoneInfoGet : BaseDVS
    {
        public ZoneInfoGet()
        { }
        #region Model
        private string _commandcode = "2007";
        /// <summary>
        /// 
        /// </summary>
        public override string CommandCode
        {
            set { _commandcode = value; }
            get { return _commandcode; }
        }
        #endregion Model

    }
}


