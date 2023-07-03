using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPManager : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        
    }
}

public class TCPServer
{
    private string host = "127.0.0.1";
    private int port = 6000;

    private Queue<byte[]> queue = new Queue<byte[]>();//��ͻ��˷��͵���Ϣ

    private Socket socketListen;//���Լ������׽���
    private Socket socketSend;//���ԺͿͻ���ͨ�ŵ��׽���

    public List<string> lstClients;//��¼���ӵĿͻ���

    public delegate void MessageReceived(byte[] bytes, int len);
    public event MessageReceived OnMessageReceived;

    public delegate void ClientConnected();
    public event ClientConnected OnClientConnected;

    public TCPServer(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    /// <summary>
    /// ����Socket���Ӽ���
    /// </summary>
    public void Start()
    {
        try
        {
            lstClients = new List<string>();
            //�����ʼ����ʱ �ڷ���˴���һ���������IP�Ͷ˿ںŵ�Socket
            socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(host), port);   //��������˿�
            socketListen.Bind(point);                        //�󶨶˿ں�

            Debug.Log("TCP Server starts listening!");

            socketListen.Listen(100);                         //���ü��������ͬʱ������

            //���������߳�
            Thread thread = new Thread(Listen);
            thread.IsBackground = true;
            thread.Start(socketListen);
        }
        catch (Exception ex)
        {
            Debug.Log("Start TCP Server Error: " + ex.ToString());
        }
    }

    /*
     * ǰ�� 8 ������ 4 ������������ 12 ���ֽ�
     * 255 255 238 238 0 0 0 60   2003 xxxxxx  238 238 255 255
     * 255 255 238 238 0 0 0 60   2004 xxxxxx  238 238 255 255
     * 255 255 238 238 0 0 0 46   2005 xxxxxx  238 238 255 255
     * 255 255 238 238 0 0 1 95   2006 xxxxxx  238 238 255 255
     * 255 255 238 238 0 0 0 46   2007 xxxxxx  238 238 255 255
     * 255 255 238 238 0 0 1 124  2008 xxxxxx  238 238 255 255
     */
    /// <summary>
    /// ���ͷ���������������������Ϊ2003��2004��2005��2006��2007��2008
    /// </summary>
    /// <param name="commandStr"></param>
    public void SendCommand(string commandStr)
    {
        commandStr = commandStr.Replace(" ", "");
        byte[] commandBytes = Encoding.UTF8.GetBytes(commandStr);
        int len = commandBytes.Length;

        byte[] bytes = new byte[len + 12];

        byte[] before = { 255, 255, 238, 238, 0, 0, 0, 60 };
        byte[] after = { 238, 238, 255, 255 };
        if (commandStr.Contains("\"CommandCode\":\"2003\"") || commandStr.Contains("\"CommandCode\":\"2004\""))
        {
            before[7] = 60;
        }
        else if (commandStr.Contains("\"CommandCode\":\"2005\"") || commandStr.Contains("\"CommandCode\":\"2007\""))
        {
            before[7] = 46;
        }
        else if (commandStr.Contains("\"CommandCode\":\"2006\""))
        {
            before[6] = 1;
            before[7] = 95;
        }
        else if (commandStr.Contains("\"CommandCode\":\"2008\""))
        {
            before[6] = 1;
            before[7] = 124;
        }
        Array.Copy(before, 0, bytes, 0, 8);
        Array.Copy(commandBytes, 0, bytes, 8, commandBytes.Length);
        Array.Copy(after, 0, bytes, commandBytes.Length + 8, 4);

        CommonFunc.SaveRawBytesToFile(bytes, bytes.Length, "TCPServerSend.txt");
        //SaveBytesToHex(bytes);

        queue.Enqueue(bytes);
    }
    public void SendMessage(byte[] bytes)
    {
        queue.Enqueue(bytes);
    }
    /// <summary>
    /// �ȴ��ͻ��˵����� ���Ҵ�����֮ͨ�ŵ�Socket
    /// </summary>
    void Listen(object o)
    {
        try
        {
            Socket socket = o as Socket;
            while (true)
            {
                socketSend = socket.Accept();           //�ȴ����տͻ�������

                string client = socketSend.RemoteEndPoint.ToString();
                if (!lstClients.Contains(client))
                    lstClients.Add(client);


               
                Debug.Log(client + ":" + " connected!");
                


                Thread r_thread = new Thread(ReceiveMessage);      //����һ�����̣߳�ִ�н�����Ϣ����
                r_thread.IsBackground = true;
                r_thread.Start(socketSend);

                Thread s_thread = new Thread(SendMessage);   //����һ�����̣߳�ִ�з�����Ϣ����
                s_thread.IsBackground = true;
                s_thread.Start(socketSend);

                if (OnClientConnected != null)
                    OnClientConnected.Invoke();
            }
        }
        catch { }
    }

    /// <summary>
    /// �������˲�ͣ�Ľ��տͻ��˷�������Ϣ
    /// </summary>
    void ReceiveMessage(object o)
    {

       
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                
                byte[] buffer = new byte[1024 * 20];         //�ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ
                int len = socketSend.Receive(buffer);       //ʵ�ʽ��յ�����Ч�ֽ���
                if (len == 0)
                {
                    break;
                }

                string str = Encoding.UTF8.GetString(buffer, 0, len);
                Debug.Log($"TCP Server received message:��{len}����{socketSend.RemoteEndPoint}:{str}");
               

                if (OnMessageReceived != null)
                    OnMessageReceived.Invoke(buffer, len);
            }
        }
        catch (Exception ex)
        {

            if (PromptStatus.Instance != null)
            {
                Action UpdateStatus = () => PromptStatus.Instance.UpdateStatus("����", "����");
                UnityThread.executeInUpdate(UpdateStatus);
               

            }
            Debug.Log("TCPManager - ReceiveMessage: " + ex.ToString());


        }
    }

    /// <summary>
    /// �������˲�ͣ����ͻ��˷�����Ϣ
    /// </summary>
    void SendMessage(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                if (queue.Count > 0)
                {
                    byte[] bytes = queue.Dequeue();
                   
                    Debug.Log("TCP Server send message :" + Encoding.UTF8.GetString(bytes));

                    socketSend.Send(bytes);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("TCPManager - SendMessage: " + ex.ToString());
            if (PromptStatus.Instance != null)
            {
                Action UpdateStatus = () => PromptStatus.Instance.UpdateStatus("����", "����");
                UnityThread.executeInUpdate(UpdateStatus);


            }
        }
    }

    public void Stop()
    {
        lstClients.Clear();

        if (socketListen != null)
        {
            try { socketListen.Shutdown(SocketShutdown.Both); } catch { }
            try { socketListen.Close(); } catch { }
            try { socketListen.Dispose(); } catch { }
        }

        if (socketSend != null)
        {
            try { socketSend.Shutdown(SocketShutdown.Both); } catch { }
            try { socketSend.Close(); } catch { }
            try { socketSend.Dispose(); } catch { }
        }
    }
}

public class TCPClient
{
    private string host = "127.0.0.1";
    private int port = 6000;
    private string msg = "";

    private Socket socketSend;

    public delegate void MessageReceived(string msg);
    public event MessageReceived OnMessageReceived;

    public TCPClient(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    public void Start()
    {
        try
        {
            //�����ͻ���Socket�����Զ��ip�Ͷ˿ں�
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(host), port);

            socketSend.Connect(point);
            Debug.Log("Connection successful , " + " ip = " + host + " port = " + port);

            Thread r_thread = new Thread(ReceiveMessage);             //�����µ��̣߳���ͣ�Ľ��շ�������������Ϣ
            r_thread.IsBackground = true;
            r_thread.Start();

            Thread s_thread = new Thread(SendMessage);          //�����µ��̣߳���ͣ�ĸ�������������Ϣ
            s_thread.IsBackground = true;
            s_thread.Start();
        }
        catch (Exception)
        {
            Debug.Log("IP address or port number error......");
        }
    }

    void ReceiveMessage()
    {
      
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 6];
                //ʵ�ʽ��յ�����Ч�ֽ���
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                CommonFunc.SaveRawBytesToFile(buffer, len, "TCPClientReceive.txt");
                string temp = Encoding.ASCII.GetString(buffer, 0, len);

                Debug.Log("TCP Client received message �� " + temp);
            }
            catch { }
        }
    }

    void SendMessage()
    {
        try
        {
            while (true)
            {
                if (!string.IsNullOrEmpty(msg))                              //�������˷��Ͱ�ť
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(msg);
                    socketSend.Send(buffer);
                    Debug.Log("TCP Client send message��" + msg);
                    msg = "";
                }
            }
        }
        catch { }
    }
}