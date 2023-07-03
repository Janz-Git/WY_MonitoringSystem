using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// BasicParamsGet: 命令码2005：获取DVS设备基本参数（服务端主动）
    /// </summary>
    [Serializable]
    public partial class BasicParamsGet : BaseDVS
    {
        public BasicParamsGet()
        { }
        #region Model
        private string _commandcode = "2005";
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

