using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// BasicParamsUpdate: 命令码2006：更新DVS设备基本参数（服务端主动）
    /// </summary>
    [Serializable]
    public partial class BasicParamsUpdate:BasicParams
    {
        public BasicParamsUpdate()
        { }
        #region Model
        private string _commandcode = "2006";
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

