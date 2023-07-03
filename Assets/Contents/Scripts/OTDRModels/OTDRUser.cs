using System;
namespace DiBo.Model
{
    /// <summary>
    /// OTDRUser:ʵ����(����������α�������)
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
        /// ���˾���/������λ��
        /// </summary>
        public string XAxisData
        {
            set { _xaxisdata = value; }
            get { return _xaxisdata; }
        }
        /// <summary>
        /// ���½��
        /// </summary>
        public string DemodulateResult
        {
            set { _demodulateresult = value; }
            get { return _demodulateresult; }
        }
        /// <summary>
        /// ����ԨƵ�ƣ���������α�����ȣ�ͨ�����ֶ����趨��ֵ�����ж�
        /// </summary>
        public string YAxisData
        {
            set { _yaxisdata = value; }
            get { return _yaxisdata; }
        }
        /// <summary>
        /// ϵͳ��ǰʱ��
        /// </summary>
        public DateTime? Curtime
        {
            set { _curtime = value; }
            get { return _curtime; }
        }
        /// <summary>
        /// ��ǰ�ź��ڹ��˴������Զ���룬Ϊ�̶�ֵ������4500������ֵ�����仯����ô˵�����˿����ж�
        /// </summary>
        public string distance
        {
            set { _distance = value; }
            get { return _distance; }
        }
        #endregion Model

    }
}

