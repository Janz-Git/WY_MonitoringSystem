using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// RawSignalCurveGet: 命令码2004：获取原始信号曲线（服务端主动）
    /// </summary>
    [Serializable]
    public partial class RawSignalCurveGet : BaseCurve
    {
        public RawSignalCurveGet()
        { }
        #region Model
        private string _commandcode = "2004";
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