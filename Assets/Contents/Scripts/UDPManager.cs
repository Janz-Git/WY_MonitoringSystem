using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPManager : MonoBehaviour
{
    [HideInInspector]
    public int port { get; set; }

    private Encoding encoding = Encoding.Default;
    public Action<byte[]> OnMessageReceived = null;
    private Thread ReceiveThread;
    private UdpClient udpReceive;

    private void Start()
    {
        AppConfig config = AppConfig.Instance;
        port = int.Parse(config.DefaultConfig["Port"]);
        encoding = Encoding.GetEncoding(config.DefaultConfig["Encoding"]);//ASCII, Unicode, UTF-32, UTF-7, UTF-8

        //���������߳�
        StartReceiveThread();
    }
    private void Update()
    {

    }
    private void OnDestroy()
    {
        if (ReceiveThread != null)
        {
            ReceiveThread.Abort();
        }
    }

    private void OnApplicationQuit()
    {
        if (ReceiveThread != null)
        {
            ReceiveThread.Abort();
        }
    }

    /// <summary>
    /// ��ʼ�߳̽���
    /// </summary>
    private void StartReceiveThread()
    {
        //��һ�����߳̽���UDP���͵�����
        ReceiveThread = new Thread(() =>
        {
            //ʵ����һ��IPEndPoint������IP�Ͷ�Ӧ�˿� �˿������޸�
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            udpReceive = new UdpClient(endPoint);
            UDPData data = new UDPData(endPoint, udpReceive);
            //�����첽����
            udpReceive.BeginReceive(ReceiveCallBack, data);
        })
        {
            //����Ϊ��̨�߳�
            IsBackground = true
        };
        //�����߳�
        ReceiveThread.Start();
    }

    /// <summary>
    /// �첽���ջص�
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            //�����������첽���תΪ��Ҫ����������
            UDPData state = ar.AsyncState as UDPData;

            IPEndPoint ipEndPoint = state.EndPoint;
            //�����첽���� �������ᵼ���ظ������߳̿���
            byte[] bytes = state.UDPClient.EndReceive(ar, ref ipEndPoint);

            //�������ݣ��������ͻ��˴������ı������

            if (OnMessageReceived != null)
            {
                OnMessageReceived(bytes);
            }
            //Debug.Log(encoding.GetString(data));

            //���ݵĽ�����Update��ִ�� Unity��Thread�޷��������̵߳ķ���
            //�ٴο����첽��������
            state.UDPClient.BeginReceive(ReceiveCallBack, state);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="remoteIP">���͵�ַ</param>
    /// <param name="remotePort">���Ͷ˿�</param>
    /// <param name="message">��Ҫ���͵���Ϣ</param>
    public void SendMessage(string remoteIP, int remotePort, string message)
    {
        //����Ҫ���͵�����תΪbyte���� ���������ն�Ϊ���������޸�
        byte[] bytes = encoding.GetBytes(message);
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //�������ݵ���ӦĿ��
        udpSend.Send(bytes, bytes.Length, remoteIPEndPoint);
        //�ر�
        udpSend.Close();
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="remoteIP">���͵�ַ</param>
    /// <param name="remotePort">���Ͷ˿�</param>
    /// <param name="bytes">��Ҫ���͵���Ϣ</param>
    public void SendMessage(string remoteIP, int remotePort, byte[] bytes)
    {
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //�������ݵ���ӦĿ��
        udpSend.Send(bytes, bytes.Length, remoteIPEndPoint);
        //�ر�
        udpSend.Close();
    }
    public decimal ByteArrayToDecimal(byte[] src)
    {
        using (MemoryStream stream = new MemoryStream(src))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                return reader.ReadDecimal();
            }
        }
    }

    class UDPData
    {
        private readonly UdpClient udpClient;
        public UdpClient UDPClient
        {
            get { return udpClient; }
        }
        private readonly IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get { return endPoint; }
        }
        //���캯��
        public UDPData(IPEndPoint endPoint, UdpClient udpClient)
        {
            this.endPoint = endPoint;
            this.udpClient = udpClient;
        }
    }
}

/// <summary>
/// ���� �ṹ��
/// </summary>
public class FBGW
{
    /// <summary>
    /// ͨ��ID
    /// </summary>
    public int channelID;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public float waveValue;
}

/// <summary>
/// ����+������ �ṹ��
/// </summary>
public class FBGV
{
    /// <summary>
    /// ͨ��ID
    /// </summary>
    public int channelID;
    /// <summary>
    /// ����
    /// </summary>
    public float waveValue;
    /// <summary>
    /// ������
    /// </summary>
    public float physicValue;
    /// <summary>
    /// 0������1Ԥ����2����
    /// </summary>
    public int alarmStatus;
}
