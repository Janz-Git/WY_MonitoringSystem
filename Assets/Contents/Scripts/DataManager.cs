using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

using DiBo.CmdModel;
using DiBo.Model;
using DotNet.FrameWork.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update

    private string host = "";
    private int port = 6000;
    private int dataNumber = 15;
    private Encoding encoding = Encoding.Default;

    private static bool needUpdateAlarmInfo = false;

    public static float timeCounter = 0;
    public string DeviceID;

    #region UDP related
    UDPManager udpClient;

    /// <summary>
    /// ��ͬ���������Ͷ�Ӧ��չʾ��ţ����� ���1 : 0�¶��࣬���2 : 1Ӧ���࣬���3 : 8�²���
    /// </summary>
    private static Dictionary<int, int> dicNumberType = new Dictionary<int, int>();

    /// <summary>
    /// ��������(0~12) : Ԥ����ֵ(float),
    /// </summary>
    private static Dictionary<int, float> dicAlarm1 = new Dictionary<int, float>();

    /// <summary>
    /// ��������(0~12) : ������ֵ(float),
    /// </summary>
    private static Dictionary<int, float> dicAlarm2 = new Dictionary<int, float>();

    /// <summary>
    /// ���� ���ݶ���
    /// </summary>
    public Queue<List<FBGW>> FBGWQueue = new Queue<List<FBGW>>();

    /// <summary>
    /// ���� + ������ ���ݶ���
    /// </summary>
    public Queue<List<FBGV>> FBGVQueue = new Queue<List<FBGV>>();
    #endregion

    #region TCP/DVS related
    TCPServer tcpServer;
    TCPClient tcpClient;

    
   

    #region Local Data
    [HideInInspector]
    /// <summary>
    /// 1001 �豸��Ϣ�ϱ����ͻ���������
    /// </summary>
    public List<DVSDeviceInfoReport> lstDeviceInfo;
    [HideInInspector]
    /// <summary>
    /// 1002 ������Ϣ�ϱ����ͻ���������
    /// </summary>
    public List<DVSAlarmInfoReport> lstAlarmInfo;
    [HideInInspector]
    /// <summary>
    /// 1003 ���������ߣ��ͻ��˱�����
    /// </summary>
    public List<DVSVibrationCurveReturn> lstVibrationCurve;
    
   
    [HideInInspector]
    /// <summary>
    /// 1004 ����ԭʼ�ź����ߣ��ͻ��˱�����
    /// </summary>
    public List<DVSRawSignalCurveReturn> lstRawSignalCurve;
    [HideInInspector]
    /// <summary>
    /// 1005 ����DVS�豸�����������ͻ��˱�����
    /// </summary>
    public List<DVSBasicParamsReturn> lstDVSBasicParams;
    [HideInInspector]
    /// <summary>
    /// 1007 ����DVS�����������ͻ��˱�����
    /// </summary>
    public List<DVSZoneInfoReturn> lstDVSZoneInfo;
    [HideInInspector]
    /// <summary>
    /// ��ʷ��¼��Ϣ
    /// </summary>
    public List<DVSHistoricalData> lstHistoryInfo;
    [HideInInspector]
    /// <summary>
    /// 2006 ����DVS�豸��������������������������ڼ��غͱ��� �������õ��豸��������
    /// </summary>
    public List<DVSBasicParamsUpdate> lstBasicParamsUpdate;
    [HideInInspector]
    /// <summary>
    /// 2008 ����DVS��������������������������ڼ��غͱ��� �������õķ�������
    /// </summary>
    public List<DVSZoneInfoUpdate> lstDVSZoneInfoUpdate;
    #endregion
    #endregion
    public InitializeAndUpdateData InitializeAndUpdateData_;

    #region OTDR related
    /// <summary>
    /// ����OTDR���ݿ���ʱʱ��
    /// </summary>
    private int OTDRDelay = 120;
    [HideInInspector]
    public List<DiBo.Model.OTDRUser> lstOTDR = new List<OTDRUser>();
  
    #endregion

    private void Awake()
    {
        if (GameObject.Find("�����������") != null)
        {
            InitializeAndUpdateData_ = GameObject.Find("�����������").GetComponent<InitializeAndUpdateData>();
        }
        AppConfig config = AppConfig.Instance;
        config.LoadConfig(ConfigConstant.ConfigFile);
        Dictionary<string, string> data = config.dicConfigList[ConfigConstant.ConfigFile];

        host = data["Host"];
        port = int.Parse(data["Port"]);
        dataNumber = int.Parse(data["DataNumber"]);
        encoding = Encoding.GetEncoding(data["Encoding"]);//ASCII, Unicode, UTF-32, UTF-7, UTF-8

        //UDP���ݿ������ַ�����������η�λ�ƺͻ���Ӧ������
        DbHelperMySQL.connectionString = data["DBLinkMySQL"];
        //OTDR���ݿ������ַ�������������������α�����
        DbHelperMySQL2.connectionString = data["DBLinkMySQL2"];

        dicNumberType = JsonConvert.DeserializeObject<Dictionary<int, int>>(config.DefaultConfig["FBGNumberType"]);
        //Debug.Log(dicNumberType.Count);

        dicAlarm1 = JsonConvert.DeserializeObject<Dictionary<int, float>>(config.DefaultConfig["FBGAlarm1"]);
        //Debug.Log(dicAlarm1.Keys.Count + "-" + dicAlarm1.Values.Count);

        dicAlarm2 = JsonConvert.DeserializeObject<Dictionary<int, float>>(config.DefaultConfig["FBGAlarm2"]);
        //Debug.Log(dicAlarm2.Keys.Count + "-" + dicAlarm2.Values.Count);

        OTDRDelay = int.Parse(config.DefaultConfig["OTDRDelay"]);

        InitList();
    }

    void Start()
    {
        InitDefaultDVSData();

        udpClient = GetComponent<UDPManager>();
        if (udpClient == null)
        {
            udpClient = gameObject.AddComponent<UDPManager>();
        }
        //���ý�������
        udpClient.OnMessageReceived += HandleUDPMessage;


        tcpServer = new TCPServer(host, port);
        tcpServer.OnMessageReceived += HandleDVSMessage;
        tcpServer.OnClientConnected += SendCommand;
        tcpServer.Start();

        //tcpClient = new TCPClient(host, port);
        //tcpClient.Start();

        OTDRManager otdr= new OTDRManager();
        otdr.OnDataObtained += HandleOTDRData;


        //����UDP������Ϣ
        StartCoroutine(TestUDPMessage());

        //OTDR��������
        StartCoroutine(TestOTDRData());
    }

    // Update is called once per frame
    void Update()
    {
        //sending test message
        //byte[] msgFBGW = { 70, 66, 71, 87, 32, 1, 242, 75, 1, 122, 117, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //byte[] msgFBGV = { 70, 66, 71, 86, 32, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 78, 76, 193, 101, 41, 196, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //udpClient.SendMessage(host, port, "������Ϣ " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
    }

    public void ShowBytes(byte[] data)
    {

        Debug.Log("���ȣ�" + data.Length + " - " + encoding.GetString(data));
    }

    private void OnDestroy()
    {
        if (tcpServer != null)
        {
            tcpServer.Stop();
            tcpServer = null;
        }
    }



    #region UDP ��Ϣ���
    /// <summary>
    /// ����UDP������Ϣ
    /// </summary>
    /// <returns></returns>
    IEnumerator TestUDPMessage()
    {
        //load test files
        List<byte[]> lstFBGV = new List<byte[]>();
        List<byte[]> lstFBGW = new List<byte[]>();

        string FBGVFile = Application.streamingAssetsPath + "/FBGV.txt";
        string FBGWFile = Application.streamingAssetsPath + "/FBGW.txt";

        lstFBGV = AnalyseUDPFile(FBGVFile);
        lstFBGW = AnalyseUDPFile(FBGWFile);

        int countFBGV = lstFBGV.Count;
        int countFBGW = lstFBGW.Count;
        int indexFBGV = 0, indexFBGW = 0;

        float t = 0;

        while (true)
        {
            t += Time.deltaTime;
            //ÿ5�뷢��һ�β�������
            if (t > 5.0f)
            {
                if (indexFBGV == countFBGV)
                    indexFBGV = 0;
                if (indexFBGW == countFBGW)
                    indexFBGW = 0;
                byte[] msgFBGV = lstFBGV[indexFBGV];
                byte[] msgFBGW = lstFBGW[indexFBGW];

                udpClient.SendMessage(host, port, msgFBGV);
                udpClient.SendMessage(host, port, msgFBGW);

                Debug.Log("Sending udp Messages.");


                t = 0;
                indexFBGV++;
                indexFBGW++;
            }
            yield return null;
        }
    }

    private List<byte[]> AnalyseUDPFile(string fullName)
    {
        List<byte[]> lst = new List<byte[]>();
        if (File.Exists(fullName))
        {
            using (StreamReader sr = new StreamReader(fullName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(","))
                    {
                        string[] strDatas = line.Trim().Split(',');

                        int len = strDatas.Length;
                        byte[] bytes = new byte[len];
                        for (int i = 0; i < len; i++)
                        {
                            bytes[i] = byte.Parse(strDatas[i]);
                        }
                        lst.Add(bytes);
                    }
                }
            }
        }
        return lst;
    }


    /// <summary>
    /// ������յ��� UDP ��Ϣ
    /// </summary>
    /// <param name="bytes">ԭʼ����</param>
    public void HandleUDPMessage(byte[] bytes)
    {
        if (bytes.Length < 5)
            return;

        byte[] msgType = new byte[4];
        byte[] msgContent = new byte[bytes.Length - 4];

        Array.Copy(bytes, 0, msgType, 0, 4);
        Array.Copy(bytes, 4, msgContent, 0, bytes.Length - 4);

        string strType = Encoding.ASCII.GetString(msgType);

        Debug.Log("Received udp message��" + strType + " - " + encoding.GetString(bytes));

        int channelCount = 0;//��ͨ����
        int channelIndex = 0;//ͨ��������0-31��
        int channelValue = 0;//ͨ��ֵ��1-x��

        List<FBGW> lstFBGW = new List<FBGW>();
        List<FBGV> lstFBGV = new List<FBGV>();

        /*
         * ���������㹫ʽ= decֵ/1000+1520
         * ����Ϊ10���ƣ�ǰ4���̶���FBGW��32��ͨ������1��ʾ��1��ͨ���Ϲ���1�鴫���������ֽڱ�ʾһ������ֵ����2��ͨ����1�鴫����������ֵ��3ͨ��0��������4ͨ��0������......
         * F  B  G  W       1  ��  ��   2  ��   ��  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 ͨ��
         * 70,66,71,87, 32, 1, 242,75, 1, 122,117, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
         * 
         * ��32ͨ��     ��1ͨ��2�鴫����       ��2ͨ��3�鴫����                ��3ͨ��1�鴫����
         *  32,        2, 242,75,|230,70,   3, 231,75,|119,117,|122,117   1, 121,117,
         *  0          1  2   3   4   5     6  7   8   9   10   11  12    13 14  15  ��������
         */
        if (strType == "FBGW")
        {
            channelCount = msgContent[0];
            channelIndex = 1;

            //�ж��ٸ�ͨ����ѭ�����ٴ�
            for (int i = 0; i < channelCount; i++)
            {
                channelValue = msgContent[channelIndex];
                if (channelValue != 0)
                {
                    //�� i ͨ���Ϲ��� channelValue ��������
                    for (int j = 0; j < channelValue; j++)
                    {
                        byte[] w = new byte[2];
                        //������ʼλ����ͨ������һλ�����+1
                        Array.Copy(msgContent, channelIndex + 1, w, 0, 2);

                        int iValue = CommonFunc.ByteToInt16(w);
                        float wValue = 0;
                        if (iValue != -1 && iValue != 0)
                        {
                            wValue = iValue / 1000.00000f + 1520;
                        }

                        FBGW fBGW = new FBGW();
                        fBGW.channelID = i;//0,1,2...31
                        fBGW.waveValue = wValue;
                        lstFBGW.Add(fBGW);

                        channelIndex += 2;//�����������ֽ���ɣ�����ÿ�κ�����λ
                    }
                }
                channelIndex += 1;
            }
            if (lstFBGW.Count > 0)
                FBGWQueue.Enqueue(lstFBGW);
            Debug.Log("FBGW lst.Count: " + lstFBGW.Count);
        }
        /*
         * ����+���������������㹫ʽ=ֵ/1000+1520.
         * ����Ϊ10���ƣ�ǰ4���̶���FBGV��32��ͨ������3��ʾ��1��ͨ���Ϲ���3�鴫������֮��ÿ�����ֽڱ�ʾһ������ֵ���ĸ��ֽڱ�ʾ������
         * F  B  G  V     1  ���� �� �� ��  ����  �� �� ��   �� ��    ��    ��    ��   2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32
         * 70,66,71,86,32,3, 0,0,|0,0,0,0,|0,0,|0,0,0,0, |78,76, |193,101,41,196, |0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
         * 
         * �����1ͨ����1��2�鴫���������ݣ���3�鴫�������ݱ�ʾ�²��࣬��Ҫ��DBSensor��������ʾ��ţ�������²������������1����ô1ͨ���ĵ�1�鴫�����������ݣ���3�鴫������������
         * 
         * ��32ͨ��   ��1ͨ��3�鴫����  ����  ������   ����  ������     ����     ��   ��   ��
         * 32,       3,              0,0,|0,0,0,0,|0,0,|0, 0, 0, 0, |78,76, |193,101,41,196, |0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
         * 0         1               2 3  4 5 6 7  8 9  10,11,12,13  14 15   16  17  18 19    20  �������� 
         */
        else if (strType == "FBGV") //����+������
        {
            channelCount = msgContent[0];
            channelIndex = 1;

            //�ж��ٸ�ͨ����ѭ�����ٴ�
            for (int i = 0; i < channelCount; i++)
            {
                channelValue = msgContent[channelIndex];
                if (channelValue != 0)
                {
                    //�� i ͨ���Ϲ��� channelValue ��������
                    for (int j = 0; j < channelValue; j++)
                    {
                        byte[] w = new byte[2];//����
                        byte[] p = new byte[4];//������

                        //������ʼλ����ͨ������һλ����� + 1
                        Array.Copy(msgContent, channelIndex + 1, w, 0, 2);
                        //��������ʼλ���ڲ���������λ����� + 2 + 1
                        Array.Copy(msgContent, channelIndex + 3, p, 0, 4);

                        int iValue = CommonFunc.ByteToInt16(w);
                        float wValue = 0;
                        if (iValue != -1 & iValue != 0)
                        {
                            wValue = iValue / 1000.00000f + 1520;
                        }

                        float pValue = CommonFunc.ByteToFloat(p);
                        if (pValue == -1f)
                        {
                            pValue = 0.0f;
                        }

                        int alarmStatus = 0;
                        if ((j + 1) <= dicNumberType.Count && dicNumberType.ContainsKey(j + 1))
                        {
                            int dataType = dicNumberType[j + 1];//��ȡ�������ͣ��¶ȡ�Ӧ�䡢λ�Ƶ�

                            float alarm1 = 0.0f, alarm2 = 0.0f;

                            if (dicAlarm1.ContainsKey(dataType))
                                alarm1 = dicAlarm1[dataType];
                            if (dicAlarm2.ContainsKey(dataType))
                                alarm2 = dicAlarm2[dataType];

                            if (pValue >= alarm1 && pValue < alarm2)
                                alarmStatus = 1;
                            else if (pValue >= alarm2)
                                alarmStatus = 2;
                        }

                        FBGV fBGV = new FBGV();
                        fBGV.channelID = i;//0,1,2...31
                        fBGV.waveValue = wValue;
                        fBGV.physicValue = pValue;
                        fBGV.alarmStatus = alarmStatus;
                        lstFBGV.Add(fBGV);

                        channelIndex += 6;//����+��������6�ֽڣ�ÿ�κ���6λ
                    }
                }
                channelIndex += 1;
            }
            if (lstFBGV.Count > 0)
                FBGVQueue.Enqueue(lstFBGV);
            Debug.Log("FBGV lst.Count: " + lstFBGV.Count);
        }

        DiBo.Model.FBGInfo fbgInfo = new FBGInfo();
        string json = string.Empty;
        if (strType == "FBGW")
        {
            json = JsonConvert.SerializeObject(lstFBGW);
        }
        else
        {
            json = JsonConvert.SerializeObject(lstFBGV);
        }
        fbgInfo.channel_count = channelCount;
        fbgInfo.raw_data = CommonFunc.ByteToString(bytes);
        fbgInfo.real_data = json;
        fbgInfo.data_type = strType == "FBGW" ? 0 : 1;
        DiBo.DAL.FBGInfo.Add(fbgInfo);
    }

    #endregion


    #region TCP DVS ��Ϣ���
    private void InitList()
    {
        lstDeviceInfo = new List<DVSDeviceInfoReport>();
        lstAlarmInfo = new List<DVSAlarmInfoReport>();
        lstVibrationCurve = new List<DVSVibrationCurveReturn>();
        lstRawSignalCurve = new List<DVSRawSignalCurveReturn>();
        lstDVSBasicParams = new List<DVSBasicParamsReturn>();
        lstDVSZoneInfo = new List<DVSZoneInfoReturn>();
        lstBasicParamsUpdate = new List<DVSBasicParamsUpdate>();
        lstDVSZoneInfoUpdate = new List<DVSZoneInfoUpdate>();
        lstHistoryInfo = new List<DVSHistoricalData>();
    }


    /// <summary>
    /// ����Ĭ������
    /// </summary>
    private void InitDefaultDVSData()
    {
        //1001���豸��Ϣ�ϱ����ͻ���������
        lstDeviceInfo = DiBo.DAL.DVSDeviceInfoReport.GetModels(dataNumber);
        //Debug.Log("DVSDeviceInfoReport Count: " + lstDeviceInfo.Count);

        //1002��������Ϣ�ϱ����ͻ���������
        lstAlarmInfo = DiBo.DAL.DVSAlarmInfoReport.GetModels(dataNumber);
        //Debug.Log("DVSAlarmInfoReport Count: " + lstAlarmInfo.Count);
        //��ʷ����
        lstHistoryInfo = DiBo.DAL.DVSHistoricalData.GetModels(dataNumber);
        // Debug.Log("DVSHistoricalData Count: " + lstHistoryInfo.Count);
        //1003�����������ߣ��ͻ��˱�����
        lstVibrationCurve = DiBo.DAL.DVSVibrationCurveReturn.GetModel(6);
       // Debug.Log("DVSVibrationCurveReturn Count: " + lstVibrationCurve.Count + lstVibrationCurve[0].CurveData);



        //1004������ԭʼ�ź����ߣ��ͻ��˱�����
        lstRawSignalCurve = DiBo.DAL.DVSRawSignalCurveReturn.GetModel(1);
        //Debug.Log("DVSRawSignalCurveReturn Count: " + lstRawSignalCurve.Count);

        //1005������DVS�豸�����������ͻ��˱�����
        DVSBasicParamsReturn dvsBasicParams = DiBo.DAL.DVSBasicParamsReturn.GetModel();
        if (dvsBasicParams != null)
            lstDVSBasicParams.Add(dvsBasicParams);
        //Debug.Log("DVSBasicParamsReturn Count: " + lstDVSBasicParams.Count);

        //1007������DVS�����������ͻ��˱�����
        DVSZoneInfoReturn dvsZoneInfoReturn = DiBo.DAL.DVSZoneInfoReturn.GetModel();
        if (dvsZoneInfoReturn != null)
            lstDVSZoneInfo.Add(dvsZoneInfoReturn);
        //Debug.Log("DVSZoneInfoReturn Count: " + lstDVSZoneInfo.Count + " , ZoneInfo: " + dvsZoneInfoReturn.ZoneInfo.Count);

        //2006
        DVSBasicParamsUpdate dvsBasicParamsUpdate = DiBo.DAL.DVSBasicParamsUpdate.GetModel();
        lstBasicParamsUpdate.Add(dvsBasicParamsUpdate);
        //Debug.Log("lstBasicParamsUpdate: " + lstBasicParamsUpdate.Count);

        //2008
        DVSZoneInfoUpdate dvsZoneInfoUpdate = DiBo.DAL.DVSZoneInfoUpdate.GetModel();
        lstDVSZoneInfoUpdate.Add(dvsZoneInfoUpdate);
        //Debug.Log("lstDVSZoneInfoUpdate: " + lstDVSZoneInfoUpdate.Count+ " , ZoneInfo: " + dvsZoneInfoUpdate.ZoneInfo.Count);

        Debug.Log("���ݼ������");
    }

    /// <summary>
    /// ������յ��� TCP(DVS) ��Ϣ
    /// </summary>
    /// <param name="message"></param>
    private void HandleDVSMessage(byte[] bytes, int len)
    {

        //todo  ����Tcp��״̬
        if (PromptStatus.Instance != null)
        {
            Action UpdateStatus = () => PromptStatus.Instance.UpdateStatus("����", "����");
            UnityThread.executeInUpdate(UpdateStatus);
            Action UpdateStatusTCP = () =>   PromptStatus.Instance.UpdateStatus("TCP", "����");
            UnityThread.executeInUpdate(UpdateStatusTCP);
          
        }
        /*
         * ǰ��8��������4���ֽ�Ϊ�ָ���
         * ��Ϣ��ʽ��
         * 255 255 238 238 00 00 01 01 xxxxxxxxxx 238 238 255 255
         * 255 255 238 238 00 00 00 7C xxxxxxxxxx 238 238 255 255
         */
        byte[] arrMsg = new byte[len - 12];
        Array.Copy(bytes, 8, arrMsg, 0, len - 12);

        CommonFunc.SaveBytesToFile(arrMsg, "TCPServerReceive.txt");

        string message = Encoding.UTF8.GetString(arrMsg);

        if (message.Replace(" ", "").Contains("\"CommandCode\":\"1001\""))
        {
            if (lstDeviceInfo.Count >= dataNumber)
            {
                lstDeviceInfo.RemoveAt(0);
            }

            DVSDeviceInfoReport model = new DVSDeviceInfoReport();

            var temp = JsonConvert.DeserializeObject<DeviceInfoReport>(message);
            if (temp != null)
            {
                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                model.DeviceName = temp.DeviceName;
                model.Version = temp.Version;
                model.ChannelCount = temp.ChannelCount;
                model.StatusOK = temp.StatusOK;
                model.CreateDate = DateTime.Now;

                model.DeviceInfoID = DiBo.DAL.DVSDeviceInfoReport.Add(model);

                lstDeviceInfo.Add(model);
            }
        }
        else if (message.Replace(" ", "").Contains("\"CommandCode\":\"1002\""))
        {
            if (lstAlarmInfo.Count >= dataNumber)
                lstAlarmInfo.RemoveAt(0);

            DVSAlarmInfoReport model = new DVSAlarmInfoReport();

            var temp = JsonConvert.DeserializeObject<AlarmInfoReport>(message);
            if (temp != null)
            {
                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                model.ChannelID = temp.ChannelID;
                model.ZoneID = temp.ZoneID;
                model.AlarmDateTime = Convert.ToDateTime(temp.AlarmDateTime);
                model.FiberPosition = temp.FiberPosition;
                model.AlarmType = temp.AlarmType;
                model.MaxAmptitude = temp.MaxAmptitude;
                model.AlarmEndTime = Convert.ToDateTime(temp.AlarmEndTime);
                model.AlarmLevel = temp.AlarmLevel;
                model.AlarmStatus = temp.AlarmStatus;
                model.CreateDate = DateTime.Now;

                string zoneName = "";
                var obj = DbHelperMySQL.ExecuteScalar("select ZoneName from DVSZoneInfo where ZoneID='" + temp.ZoneID + "' and ChannelID='" + temp.ChannelID + "' and ZoneInfoID in(select ZoneInfoID from DVSZoneInfoReturn where DeviceID='" + temp.DeviceID + "') limit 0,1");
                if (obj != null)
                    zoneName = obj.ToString().Trim();
                if (string.IsNullOrEmpty(zoneName))
                {
                    //�����������������ݿ⣬���ͻ�ȡ�����ȡָ���ķ������ƣ��յ�1007��Ϣ֮���ٸ��±������ZoneName
                    needUpdateAlarmInfo = true;
                    SendCommand(2007, temp.DeviceID, (int)temp.ChannelID);
                }
                else
                {
                    model.ZoneName = zoneName;
                }

                model.AlarmInfoID = DiBo.DAL.DVSAlarmInfoReport.Add(model);

                lstAlarmInfo.Add(model);

                if (InitializeAndUpdateData_ != null)
                {
                    //print("���յ���1002����Ϣ");
                    Action UpdateLstAlarmInfo = InitializeAndUpdateData_.Update1002;
                    UnityThread.executeInUpdate(UpdateLstAlarmInfo);

                    if (InitializeAndUpdateData_.HistoyScrollBar_ != null)
                    {

                        Action UpdateHistory = () => InitializeAndUpdateData_.HistoryListUpdate(PageManagement.WhichPage);
                        UnityThread.executeInUpdate(UpdateHistory);
                    }

                }


            }
        }
        else if (message.Replace(" ", "").Contains("\"CommandCode\":\"1003\""))
        {
            if (lstVibrationCurve.Count >= 6)
                lstVibrationCurve.RemoveAt(0);

            var temp = JsonConvert.DeserializeObject<VibrationCurveReturn>(message);
            if (temp != null)
            {
                DVSVibrationCurveReturn model = new DVSVibrationCurveReturn();
                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                model.ChannelID = temp.ChannelID;
                model.CurveLength = temp.CurveLength;
                model.Resolution = temp.Resolution;

                string curveData = "";
                foreach (var item in temp.CurveData)
                {
                    curveData += item.ToString() + ",";
                }
                model.CurveData = "[" + curveData.TrimEnd(',') + "]";
                model.CreateDate = DateTime.Now;

                model.CurveID = DiBo.DAL.DVSVibrationCurveReturn.Add(model);

                lstVibrationCurve.Add(model);
                if ( InitializeAndUpdateData_!=null&&InitializeAndUpdateData_ .Vibrationchart!= null)
                {
                    print("���յ���1003����Ϣ");
                    Action UpdateVibrationCurve = InitializeAndUpdateData_.InitVibrationCurveData;
                    UnityThread.executeInUpdate(UpdateVibrationCurve);

                    Action UpdateVibrationBar = InitializeAndUpdateData_.UpdateBarChart;
;
                    UnityThread.executeInUpdate(UpdateVibrationBar);

                }
            }
        }
        else if (message.Replace(" ", "").Contains("\"CommandCode\":\"1004\""))
        {
            if (lstRawSignalCurve.Count >= 1)
                lstRawSignalCurve.RemoveAt(0);

            var temp = JsonConvert.DeserializeObject<RawSignalCurveReturn>(message);
            if (temp != null)
            {
                DVSRawSignalCurveReturn model = new DVSRawSignalCurveReturn();
                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                model.ChannelID = temp.ChannelID;
                model.CurveLength = temp.CurveLength;
                model.Resolution = temp.Resolution;

                string curveData = "";
                foreach (var item in temp.CurveData)
                {
                    curveData += item.ToString() + ",";
                }
                model.CurveData = "[" + curveData.TrimEnd(',') + "]";
                model.CreateDate = DateTime.Now;

                model.SignalID = DiBo.DAL.DVSRawSignalCurveReturn.Add(model);

                lstRawSignalCurve.Add(model);

                if (InitializeAndUpdateData_!=null&&InitializeAndUpdateData_.Vibrationchart != null)
                {
                    print("���յ���1004����Ϣ");
                    Action UpdateSignalCurveData = InitializeAndUpdateData_.InitSignalCurveData;
                    UnityThread.executeInUpdate(UpdateSignalCurveData);

                    Action UpdateBigSignalCurveData = InitializeAndUpdateData_.InitBigSignalCurveData;
                    UnityThread.executeInUpdate(UpdateBigSignalCurveData);

                }
            }
        }
        else if (message.Replace(" ", "").Contains("\"CommandCode\":\"1005\""))
        {
            if (lstDVSBasicParams.Count >= 1)
                lstDVSBasicParams.RemoveAt(0);

            var temp = JsonConvert.DeserializeObject<BasicParamsReturn>(message);
            if (temp != null)
            {
                DVSBasicParamsReturn model = new DVSBasicParamsReturn();
                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                model.DMAID = temp.DMAID;
                model.TotalPoints = temp.TotalPoints;
                model.FrameNumber = temp.FrameNumber;
                model.AvgNumber = temp.AvgNumber;
                model.HighpassHz = temp.HighpassHz;
                model.Scanrate = temp.Scanrate;
                model.PulseWidth = temp.PulseWidth;
                model.FiberLength1 = temp.FiberLength1;
                model.FiberOffset1 = temp.FiberOffset1;
                model.Noiselevel1 = temp.Noiselevel1;
                model.FiberLength2 = temp.FiberLength2;
                model.FiberOffset2 = temp.FiberOffset2;
                model.Noiselevel2 = temp.Noiselevel2;
                model.AlarmResolution = temp.AlarmResolution;
                model.MinimumAlarmCnt = temp.MinimumAlarmCnt;
                model.DurationCnt = temp.DurationCnt;
                model.DealingWind = temp.DealingWind;
                model.WindWindow = temp.WindWindow;
                model.WindMinimumCnt = temp.WindMinimumCnt;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                model = DiBo.DAL.DVSBasicParamsReturn.SaveDVSBasicParamsReturn(model);

                lstDVSBasicParams.Add(model);
                Debug.Log("���ܵ���1005������");
                // �޸�***********  �˴����õ��˸����������1005������
                if (InitializeAndUpdateData_ != null && InitializeAndUpdateData_.data1005 != null)
                {
                    Debug.Log("************");
                    Action Update1005Data = InitializeAndUpdateData_.Init1005;
                    UnityThread.executeInUpdate(Update1005Data);
                }
            }
        }
        else if (message.Replace(" ", "").Contains("\"CommandCode\":\"1007\""))
        {
            Debug.Log("Receive 2007:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff"));
            if (lstDVSZoneInfo.Count >= 1)
                lstDVSZoneInfo.RemoveAt(0);

            var temp = JsonConvert.DeserializeObject<ZoneInfoReturn>(message);
            if (temp != null)
            {
                DVSZoneInfoReturn model = new DVSZoneInfoReturn();

                model.CommandCode = temp.CommandCode;
                model.DeviceID = temp.DeviceID;
                List<DiBo.Model.DVSZoneInfo> dvsZoneInfos = new List<DiBo.Model.DVSZoneInfo>();
                for (int i = 0; i < temp.ZoneInfo.Count; i++)
                {
                    var zoneInfo = temp.ZoneInfo[i];
                    DiBo.Model.DVSZoneInfo dvsZoneInfo = new DVSZoneInfo();
                    dvsZoneInfo.ZoneID = zoneInfo.ZoneID;
                    dvsZoneInfo.ChannelID = zoneInfo.ChannelID;
                    dvsZoneInfo.ZoneName = zoneInfo.ZoneName;
                    dvsZoneInfo.FiberStart = zoneInfo.FiberStart;
                    dvsZoneInfo.FiberEnd = zoneInfo.FiberEnd;
                    dvsZoneInfo.Threshold = zoneInfo.Threshold;
                    dvsZoneInfo.IsAlarm = zoneInfo.IsAlarm;
                    dvsZoneInfo.DataType = 0;// 1007 ������������Ϊ0 , 2008 ������������Ϊ1
                    dvsZoneInfos.Add(dvsZoneInfo);
                }

                model.ZoneInfo = dvsZoneInfos;

                model = DiBo.DAL.DVSZoneInfoReturn.SaveDVSZoneInfoReturn(model);

                lstDVSZoneInfo.Add(model);

                //���·�������
                if (needUpdateAlarmInfo)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    DataTable dtblAlarmInfo = DbHelperMySQL.ExecuteDataTable("select AlarmInfoID,DeviceID,ChannelID,ZoneID from DVSAlarmInfoReport where ifnull(ZoneName,'')=''");

                    foreach (DataRow row in dtblAlarmInfo.Rows)
                    {
                        string alarmInfoID = row["AlarmInfoID"].ToString();
                        string deviceID = row["DeviceID"].ToString();
                        string channelID = row["ChannelID"].ToString();
                        string zoneID = row["ZoneID"].ToString();
                        string key = deviceID + "-" + channelID + "-" + zoneID;

                        string zoneName = "";

                        if (!dic.ContainsKey(key))
                        {
                            var obj = DbHelperMySQL.ExecuteScalar("select ZoneName from DVSZoneInfo where ZoneID='" + zoneID + "' and ChannelID='" + channelID + "' and ZoneInfoID in(select  ZoneInfoID from DVSZoneInfoReturn where DeviceID='" + deviceID + "' limit 0,1) limit 0,1");

                            if (obj != null)//�ҵ���������
                            {
                                zoneName = obj.ToString();
                                dic.Add(key, zoneName);
                            }
                        }
                        else
                        {
                            zoneName = dic[key];
                        }
                        if (!string.IsNullOrEmpty(zoneName))
                        {
                            DbHelperMySQL.ExecuteNonQuery("update DVSAlarmInfoReport set ZoneName='" + zoneName + "' where AlarmInfoID='" + alarmInfoID + "'");
                        }
                    }

                    if (InitializeAndUpdateData_ != null)
                    {
                        //print("���յ���1007����Ϣ");
                        Action UpdateZoneInfo = InitializeAndUpdateData_.Init1007;
                        UnityThread.executeInUpdate(UpdateZoneInfo);
                    }
                }
            }
        }
        else
        {
            Debug.Log("unkown data type." + message.Substring(0, 21));
        }

        needUpdateAlarmInfo = false;
    }


    /// <summary>
    /// ����Ĭ��DVS����
    /// </summary>
    private void SendCommand()
    {
        SendCommand(2007);
        SendCommand(2003);
        SendCommand(2004);
        SendCommand(2005);
        //SendCommand(2006);
        //SendCommand(2008);
    }

    public void SendCommand(int commandType, string deviceId = "DVS-GS-001", int channelId = 1)
    {
        if (tcpServer != null && tcpServer.lstClients.Count > 0)
        {
            if (commandType == 2003)
            {
                string msg = string.Format(DVSCommands.Cmd2003, deviceId, channelId);
                tcpServer.SendCommand(msg);
            }
            else if (commandType == 2004)
            {
                string msg = string.Format(DVSCommands.Cmd2004, deviceId, channelId);
                tcpServer.SendCommand(msg);
            }
            else if (commandType == 2005)
            {
                string msg = string.Format(DVSCommands.Cmd2005, deviceId);
                tcpServer.SendCommand(msg);
            }
            else if (commandType == 2006)
            {

                //����2006����������ݵ����ݿ�
                Action Send2006 = InitializeAndUpdateData_.Upload1005;
                UnityThread.executeInUpdate(Send2006);
            }
            else if (commandType == 2007)
            {
                string msg = string.Format(DVSCommands.Cmd2007, deviceId);
                tcpServer.SendCommand(msg);
            }
            else if (commandType == 2008)
            {
                string cmd2008sub1 = string.Format(DVSCommands.Cmd2008Sub, 1, 0, "Zone1", 100, 200, 2000, 0);
                string cmd2008sub2 = string.Format(DVSCommands.Cmd2008Sub, 2, 1, "Zone2", 300, 400, 2000, 1);
                string cmd2008sub3 = string.Format(DVSCommands.Cmd2008Sub, 3, 0, "Zone3", 300, 400, 3000, 1);

                List<string> zoneInfos = new List<string>();
                zoneInfos.Add(cmd2008sub1);
                zoneInfos.Add(cmd2008sub2);
                zoneInfos.Add(cmd2008sub3);

                SendCommand(deviceId, zoneInfos);
            }
        }
    }

    /// <summary>
    /// ����DVS�豸��������2006ר�ã����������ݵ����ݿ�
    /// </summary>
    public void SendCommand(string deviceId, int DMAID = 1, int TotalPoints = 8192, int FrameNumber = 256, int AvgNumber = 8, float HighpassHz = 30.2f, float Scanrate = 3000f, int PulseWidth = 100, int FiberLength1 = 8000, int FiberOffset1 = 10, int Noiselevel1 = 2000, int FiberLength2 = 800, int FiberOffset2 = 5, int Noiselevel2 = 2100, int AlarmResolution = 5, int MinimumAlarmCnt = 10, int DurationCnt = 12, int DealingWind = 1, int WindWindow = 5, int WindMinimumCnt = 10)
    {
        string msg = string.Format(DVSCommands.Cmd2006, deviceId, DMAID, TotalPoints, FrameNumber, AvgNumber, HighpassHz, Scanrate, PulseWidth, FiberLength1, FiberOffset1, Noiselevel1, FiberLength2, FiberOffset2, Noiselevel2, AlarmResolution, MinimumAlarmCnt, DurationCnt, DealingWind, WindWindow, WindMinimumCnt);

        tcpServer.SendCommand(msg);

        DiBo.Model.DVSBasicParamsUpdate model = new DiBo.Model.DVSBasicParamsUpdate();
        model.DeviceID = deviceId;
        model.DMAID = DMAID;
        model.TotalPoints = TotalPoints;
        model.FrameNumber = FrameNumber;
        model.AvgNumber = AvgNumber;
        model.HighpassHz = HighpassHz;
        model.Scanrate = Scanrate;
        model.PulseWidth = PulseWidth;
        model.FiberLength1 = FiberLength1;
        model.FiberOffset1 = FiberOffset1;
        model.Noiselevel1 = Noiselevel1;
        model.FiberLength2 = FiberLength2;
        model.FiberOffset2 = FiberOffset2;
        model.Noiselevel2 = Noiselevel2;
        model.AlarmResolution = AlarmResolution;
        model.MinimumAlarmCnt = MinimumAlarmCnt;
        model.DurationCnt = DurationCnt;
        model.DealingWind = DealingWind;
        model.WindWindow = WindWindow;
        model.WindMinimumCnt = WindMinimumCnt;

        model = DiBo.DAL.DVSBasicParamsUpdate.SaveDVSBasicParamsUpdate(model);
        if (lstBasicParamsUpdate.Count > 0)
            lstBasicParamsUpdate.Clear();
        lstBasicParamsUpdate.Add(model);

        DiBo.Model.DVSBasicParamsReturn returnModel = new DVSBasicParamsReturn();
        returnModel.DeviceID = deviceId;
        returnModel.DMAID = DMAID;
        returnModel.TotalPoints = TotalPoints;
        returnModel.FrameNumber = FrameNumber;
        returnModel.AvgNumber = AvgNumber;
        returnModel.HighpassHz = (decimal)HighpassHz;
        returnModel.Scanrate = (decimal)Scanrate;
        returnModel.PulseWidth = PulseWidth;
        returnModel.FiberLength1 = FiberLength1;
        returnModel.FiberOffset1 = FiberOffset1;
        returnModel.Noiselevel1 = Noiselevel1;
        returnModel.FiberLength2 = FiberLength2;
        returnModel.FiberOffset2 = FiberOffset2;
        returnModel.Noiselevel2 = Noiselevel2;
        returnModel.AlarmResolution = AlarmResolution;
        returnModel.MinimumAlarmCnt = MinimumAlarmCnt;
        returnModel.DurationCnt = DurationCnt;
        returnModel.DealingWind = DealingWind;
        returnModel.WindWindow = WindWindow;
        returnModel.WindMinimumCnt = WindMinimumCnt;
        returnModel = DiBo.DAL.DVSBasicParamsReturn.SaveDVSBasicParamsReturn(returnModel);
        if (lstDVSBasicParams.Count >= 1)
            lstDVSBasicParams.RemoveAt(0);
        lstDVSBasicParams.Add(returnModel);
    }

    /// <summary>
    /// ����DVS��������2008ר�ã����������ݵ����ݿ�
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="zoneInfos">��ʽ��{"ZoneID":1,"ChannelID":0,"ZoneName":"����һ","FiberStart":100,"FiberEnd":200,"Threshold":2000,"IsAlarm":0},{xxxx},{xxx},...</param>
    public void SendCommand(string deviceId, List<string> zoneInfos)
    {
        string msg = string.Format(DVSCommands.Cmd2008, deviceId, zoneInfos);
        tcpServer.SendCommand(msg);

        DVSZoneInfoUpdate model = new DVSZoneInfoUpdate();

        model.CommandCode = "2008";
        model.DeviceID = deviceId;
        List<DiBo.Model.DVSZoneInfo> dvsZoneInfos = new List<DiBo.Model.DVSZoneInfo>();
        for (int i = 0; i < zoneInfos.Count; i++)
        {
            DiBo.CmdModel.ZoneInfo zoneInfo = JsonConvert.DeserializeObject<DiBo.CmdModel.ZoneInfo>(zoneInfos[i]);

            DiBo.Model.DVSZoneInfo dvsZoneInfo = new DVSZoneInfo();
            dvsZoneInfo.ZoneID = zoneInfo.ZoneID;
            dvsZoneInfo.ChannelID = zoneInfo.ChannelID;
            dvsZoneInfo.ZoneName = zoneInfo.ZoneName;
            dvsZoneInfo.FiberStart = zoneInfo.FiberStart;
            dvsZoneInfo.FiberEnd = zoneInfo.FiberEnd;
            dvsZoneInfo.Threshold = zoneInfo.Threshold;
            dvsZoneInfo.IsAlarm = zoneInfo.IsAlarm;
            dvsZoneInfo.DataType = 1;// 1007 ������������Ϊ0 , 2008 ������������Ϊ1
            dvsZoneInfos.Add(dvsZoneInfo);
        }

        model.ZoneInfo = dvsZoneInfos;

        model = DiBo.DAL.DVSZoneInfoUpdate.SaveDVSZoneInfoUpdate(model);
        if (lstDVSZoneInfoUpdate.Count > 0)
            lstDVSZoneInfoUpdate.Clear();
        lstDVSZoneInfoUpdate.Add(model);


        DVSZoneInfoReturn modelReturn = new DVSZoneInfoReturn();

        modelReturn.CommandCode = "1007";
        modelReturn.DeviceID = deviceId;

        int countZoneInfo = dvsZoneInfos.Count;
        for (int i = 0; i < countZoneInfo; i++)
        {
            dvsZoneInfos[i].DataType = 0;// 1007 ������������Ϊ0 , 2008 ������������Ϊ1
        }

        modelReturn.ZoneInfo = dvsZoneInfos;

        modelReturn = DiBo.DAL.DVSZoneInfoReturn.SaveDVSZoneInfoReturn(modelReturn);

        int modelReturnIndex = -1;
        for (int i = 0; i < lstDVSZoneInfo.Count; i++)
        {
            if (lstDVSZoneInfo[i].DeviceID == deviceId)
            {
                modelReturnIndex = i;
                break;
            }
        }
        if (modelReturnIndex != -1)
        {
            lstDVSZoneInfo[modelReturnIndex] = modelReturn;
        }
    }
    #endregion

    #region OTDR

    /// <summary>
    /// ����OTDR��������
    /// </summary>
    /// <returns></returns>
    IEnumerator TestOTDRData()
    {
        float t = 0;
        System.Random random= new System.Random();
        string[] testYAxisData = { "950.0","970.0","1020.0","890","1030","920","978"};
        while (true)
        {
            t += Time.deltaTime;
            //ÿ����ʱ��������һ�β�������
            if (t > OTDRDelay)
            {
                string tableName = DateTime.Now.ToString("yyyy-MM-dd&&HH:mm:ss");

                //ʹ��DB2��DB2��DB2
                DbHelperMySQL2.ExecuteNonQuery("CREATE TABLE `"+tableName+"`(i int PRIMARY KEY AUTO_INCREMENT,XAxisData varchar(255),DemodulateResult varchar(255) ,YAxisData varchar(255),Curtime datetime,distance varchar(255))CHARSET=utf8;" );

                DiBo.Model.OTDRUser model = new OTDRUser();
                model.XAxisData = "4500";
                model.DemodulateResult = "26.3";
                model.YAxisData = testYAxisData[random.Next(0,7)];
                model.Curtime=DateTime.Now;
                model.distance = "2000";

                DiBo.DAL.OTDRUser.Add(model, tableName);

                t = 0;
            }
            yield return null;
        }
    }

    /// <summary>
    /// ����OTDR���ݣ�����ȡ��OTDR����ʱ��ִ�и÷���
    /// </summary>
    /// <param name="lst"></param>
    private void HandleOTDRData(List<DiBo.Model.OTDRUser> lst)
    {
        lstOTDR = lst;

        //todo

    }
    #endregion
}
