using System;
namespace DiBo.CmdModel
{
    /// <summary>
    /// VibrationCurveGet: 命令码2003：获取振动曲线（服务端主动）
    /// </summary>
    [Serializable]
    public partial class VibrationCurveGet : BaseCurve
    {
        public VibrationCurveGet()
        { }
        #region Model
        private string _commandcode = "2003";
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

