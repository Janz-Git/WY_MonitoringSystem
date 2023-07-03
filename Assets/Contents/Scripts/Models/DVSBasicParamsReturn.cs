using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// BasicParamsReturn: 命令码1005：返回DVS设备基本参数（客户端被动）
    /// </summary>
    [Serializable]
    public class BasicParamsReturn : BasicParams
    {
        public BasicParamsReturn()
        { }
        #region Model
        private string commandcode = "1005";

        /// <summary>
        /// 
        /// </summary>
        public override string CommandCode
        {
            set { commandcode = value; }
            get { return commandcode; }
        }

        #endregion Model

    }
}

