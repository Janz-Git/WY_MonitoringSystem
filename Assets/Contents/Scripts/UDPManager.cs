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

        //开启接收线程
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
    /// 开始线程接收
    /// </summary>
    private void StartReceiveThread()
    {
        //开一个新线程接收UDP发送的数据
        ReceiveThread = new Thread(() =>
        {
            //实例化一个IPEndPoint，任意IP和对应端口 端口自行修改
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            udpReceive = new UdpClient(endPoint);
            UDPData data = new UDPData(endPoint, udpReceive);
            //开启异步接收
            udpReceive.BeginReceive(ReceiveCallBack, data);
        })
        {
            //设置为后台线程
            IsBackground = true
        };
        //开启线程
        ReceiveThread.Start();
    }

    /// <summary>
    /// 异步接收回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            //将传过来的异步结果转为需要解析的类型
            UDPData state = ar.AsyncState as UDPData;

            IPEndPoint ipEndPoint = state.EndPoint;
            //结束异步接受 不结束会导致重复挂起线程卡死
            byte[] bytes = state.UDPClient.EndReceive(ar, ref ipEndPoint);

            //解析数据，编码依客户端传过来的编码而定

            if (OnMessageReceived != null)
            {
                OnMessageReceived(bytes);
            }
            //Debug.Log(encoding.GetString(data));

            //数据的解析再Update里执行 Unity中Thread无法调用主线程的方法
            //再次开启异步接收数据
            state.UDPClient.BeginReceive(ReceiveCallBack, state);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="remoteIP">发送地址</param>
    /// <param name="remotePort">发送端口</param>
    /// <param name="message">需要发送的信息</param>
    public void SendMessage(string remoteIP, int remotePort, string message)
    {
        //将需要发送的内容转为byte数组 编码依接收端为主，自行修改
        byte[] bytes = encoding.GetBytes(message);
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //发送数据到对应目标
        udpSend.Send(bytes, bytes.Length, remoteIPEndPoint);
        //关闭
        udpSend.Close();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="remoteIP">发送地址</param>
    /// <param name="remotePort">发送端口</param>
    /// <param name="bytes">需要发送的信息</param>
    public void SendMessage(string remoteIP, int remotePort, byte[] bytes)
    {
        IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
        UdpClient udpSend = new UdpClient();
        //发送数据到对应目标
        udpSend.Send(bytes, bytes.Length, remoteIPEndPoint);
        //关闭
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
        //构造函数
        public UDPData(IPEndPoint endPoint, UdpClient udpClient)
        {
            this.endPoint = endPoint;
            this.udpClient = udpClient;
        }
    }
}

/// <summary>
/// 波长 结构类
/// </summary>
public class FBGW
{
    /// <summary>
    /// 通道ID
    /// </summary>
    public int channelID;
    /// <summary>
    /// 波长值
    /// </summary>
    public float waveValue;
}

/// <summary>
/// 波长+物理量 结构类
/// </summary>
public class FBGV
{
    /// <summary>
    /// 通道ID
    /// </summary>
    public int channelID;
    /// <summary>
    /// 波长
    /// </summary>
    public float waveValue;
    /// <summary>
    /// 物理量
    /// </summary>
    public float physicValue;
    /// <summary>
    /// 0正常，1预警，2报警
    /// </summary>
    public int alarmStatus;
}
