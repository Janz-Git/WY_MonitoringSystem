

//将本地数据 初始化到界面上


using LitJson;
using Newtonsoft.Json;

using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XCharts.Runtime;



public class InitializeAndUpdateData :MonoBehaviour
{
    
    public DataManager dataInfo;
    public Transform DataParent_1002;
    public Transform DataParent_History;
    public Data1005 data1005;
    public Transform Data1007_;

    public static bool IsFristConnect;

    public string LoadedContentName;

   


    public string DeviceID;

    public  static int HistoryStarNum_;
    public  static int HistoryEndNum;

    public static bool IsUpdateHistoryData;

    public Scrollbar HistoyScrollBar_;
    Transform data;

  

    public  LineChart Vibrationchart;
    public  LineChart Signalchart;
     public  LineChart BigSignalchart;
   public  BarChart VibrationBarchart;
    private List<int> VibrationCurveData;
     private List<int> VibrationCurveBarChartData;
    List<int> SignalCurveData;
    List<int> BigSignalCurveData;

    public GameObject SignalCureDataShow;

    public Transform RunningStatusParent;
    void Awake()
    {


        dataInfo = GameObject.Find("服务器连接").GetComponent<DataManager>();
       dataInfo.InitializeAndUpdateData_ = transform.GetComponent<InitializeAndUpdateData>();





    }
    // Start is called before the first frame update
    void Start()
    {
      
        Init1002();

        if (data1005 != null)
        {

           
            Init1005();
            Init1007();
        }
        if (Vibrationchart!=null)
        {
           
            StartCoroutine(SendGet2003());
            InitVibrationCurveData();
            InitSignalCurveData();

             UpdateBarChart();
        }
       
        
       

    }

    // Update is called once per frame
    void Update()
    {
        //if (IsFristConnect&&data1005!=null)
        //{ 
        //  IsFristConnect = false;

        //  dataInfo.SendCommand(2005);

        //}

       

    }

    public void Init1002()
    {

        try
        {
          
            for (int i = 0;  i < dataInfo.lstAlarmInfo.Count; i++)
            {
                GameObject OBJ = Instantiate(Resources.Load(LoadedContentName)) as GameObject;
                Transform data = OBJ.transform;
                data.parent = DataParent_1002;
                Data1002 data1002 = data.GetComponent<ItemData>().Data1002_;
               


                if (dataInfo.lstAlarmInfo[i].ChannelID.ToString().Contains("0"))
                {
                    data1002._channelid = "通道一";
                }
                else
                {
                    data1002._channelid = "通道二";
                }

                data1002._zoneid = dataInfo.lstAlarmInfo[i].ZoneID.ToString();
                data1002._alarmdatetime = dataInfo.lstAlarmInfo[i].AlarmDateTime.ToString();
                data1002._fiberposition = dataInfo.lstAlarmInfo[i].FiberPosition.ToString();
                data1002._alarmtype = dataInfo.lstAlarmInfo[i].AlarmType.ToString();


                data1002._maxamptitude = dataInfo.lstAlarmInfo[i].MaxAmptitude.ToString();
                data1002._alarmendtime = dataInfo.lstAlarmInfo[i].AlarmEndTime.ToString();


               

                if (dataInfo.lstAlarmInfo[i].AlarmLevel.ToString().Contains("0"))
                {

                    data1002._alarmlevel = "预警";
                }
                else
                {
                    data1002._alarmlevel = "报警";
                }
               
                if (dataInfo.lstAlarmInfo[i].AlarmLevel.ToString().Contains("0") || dataInfo.lstAlarmInfo[i].DealingStatus == 1)
                {


                    //foreach (Transform item in data)
                    //{
                    //    if (item.GetComponent<Image>())
                    //    {
                    //        item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().NoSelectEffect;
                    //    }
                    //}
                    foreach (Transform item in data)
                    {
                        if (item.Find("Text")!=null)
                        {
                            item.Find("Text").GetComponent<Text>().color = Color.white;
                        }
                    }



                }
                else
                {
                   
                    Color color =new Color();
                    ColorUtility.TryParseHtmlString("#EF1F1F", out color);
                    foreach (Transform item in data)
                    {
                        if (item.Find("Text") != null)
                        {

                            item.Find("Text").GetComponent<Text>().color = color;
                        }
                    }
                    //foreach (Transform item in data)
                    //{
                    //    if ( item.GetComponent<Image>())
                    //    {
                    //         item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().SelectEffect;
                    //    }

                    //}
                }
                
                if (dataInfo.lstAlarmInfo[i].AlarmStatus.ToString().Contains("1"))
                {
                    data1002._alarmstatus = "报警进行中";
                }
                else if (dataInfo.lstAlarmInfo[i].AlarmStatus.ToString().Contains("0"))
                {
                    data1002._alarmstatus = "报警开始";
                }
                else
                {
                    data1002._alarmstatus = "报警结束";
                }


                //TODO

                data1002._alarminfoid = dataInfo.lstAlarmInfo[i].AlarmInfoID.ToString();

                data1002._vcount = dataInfo.lstAlarmInfo[i].VCount.ToString();

                if (dataInfo.lstAlarmInfo[i].DealingStatus==0)
                {
                    data1002._dealingstatus = "未处理";
                }
                else
                {
                     data1002._dealingstatus = "已处理";
                }

                 //Debug.Log(dataInfo.lstAlarmInfo[i].DeviceID.ToString() + "************");
               //data1002._zonename = dataInfo.lstAlarmInfo[i].ZoneName.ToString();

                data.GetComponent<ItemData>().UpdateInfo();
            }
        }
        catch (System.Exception  ex)
        {

            Debug.Log(ex.ToString());



        }

       
    
    }


    public void Update1002()
    {

       

        try
        {



            int Index = 0;

            for (int i = dataInfo.lstAlarmInfo.Count - 1; i >= 0; i--)
            {



                Transform data = DataParent_1002.GetChild(Index);
                if (Index < dataInfo.lstAlarmInfo.Count - 1)
                {
                    Index++;
                }
                Data1002 data1002 = data.GetComponent<ItemData>().Data1002_;
               //data1002._commandcode = dataInfo.lstAlarmInfo[i].CommandCode;
               //data1002._deviceid = dataInfo.lstAlarmInfo[i].DeviceID;


                if (dataInfo.lstAlarmInfo[i].ChannelID.ToString().Contains("0"))
                {
                    data1002._channelid = "通道一";
                }
                else
                {
                    data1002._channelid = "通道二";
                }


                data1002._zoneid = dataInfo.lstAlarmInfo[i].ZoneID.ToString();
                data1002._alarmdatetime = dataInfo.lstAlarmInfo[i].AlarmDateTime.ToString();
                data1002._fiberposition = dataInfo.lstAlarmInfo[i].FiberPosition.ToString();
                data1002._alarmtype = dataInfo.lstAlarmInfo[i].AlarmType.ToString();


                data1002._maxamptitude = dataInfo.lstAlarmInfo[i].MaxAmptitude.ToString();
                data1002._alarmendtime = dataInfo.lstAlarmInfo[i].AlarmEndTime.ToString();


                if (dataInfo.lstAlarmInfo[i].AlarmLevel.ToString().Contains("0"))
                {

                    data1002._alarmlevel = "预警";
                }
                else
                {
                    data1002._alarmlevel = "报警";
                }

                if (dataInfo.lstAlarmInfo[i].AlarmLevel.ToString().Contains("0") || dataInfo.lstAlarmInfo[i].DealingStatus == 1)
                {


                    //foreach (Transform item in data)
                    //{
                    //    if (item.GetComponent<Image>())
                    //    {
                    //        item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().NoSelectEffect;
                    //    }
                    //}
                    foreach (Transform item in data)
                    {
                        if (item.Find("Text") != null)
                        {
                            item.Find("Text").GetComponent<Text>().color = Color.white;
                        }
                    }



                }
                else
                {

                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("#EF1F1F", out color);
                    foreach (Transform item in data)
                    {
                        if (item.Find("Text") != null)
                        {

                            item.Find("Text").GetComponent<Text>().color = color;
                        }
                    }
                    //foreach (Transform item in data)
                    //{
                    //    if ( item.GetComponent<Image>())
                    //    {
                    //         item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().SelectEffect;
                    //    }

                    //}
                }
                if (dataInfo.lstAlarmInfo[i].AlarmStatus.ToString().Contains("1"))
                {
                    data1002._alarmstatus = "报警进行中";
                }
                else if (dataInfo.lstAlarmInfo[i].AlarmStatus.ToString().Contains("0"))
                {
                    data1002._alarmstatus = "报警开始";
                }
                else
                {
                    data1002._alarmstatus = "报警结束";
                }

                data1002._alarminfoid = dataInfo.lstAlarmInfo[i].AlarmInfoID.ToString();

                data1002._vcount = dataInfo.lstAlarmInfo[i].VCount.ToString();

                if (dataInfo.lstAlarmInfo[i].DealingStatus == 0)
                {
                    data1002._dealingstatus = "未处理";
                }
                else
                {
                    data1002._dealingstatus = "已处理";
                }



                // data1002._zonename = dataInfo.lstAlarmInfo[i].ZoneName.ToString();


                //print("数据已经更新到了配置文件");

                data.GetComponent<ItemData>().UpdateInfo();






            }
        }
        catch (System.Exception ex)
        {

           Debug.Log(ex.ToString()+"--------------");
        }
       

        
    }


  
    public void Init1005()
    {

          

        
        foreach (var item in dataInfo.lstDVSBasicParams)
        {

            if (item.DMAID.ToString().Contains("2"))
            {
                data1005._dmaid.value = 1;
            }
            else
            {

               
                data1005._dmaid.value = 0;
            }


            for (int i = 0; i < data1005._highpassHz.options.Count; i++)
            {
                float A = float.Parse(data1005._highpassHz.options[i].text);
                float B = float.Parse(item.HighpassHz.ToString());
                if (A.Equals(B))
                {
                    data1005._highpassHz.captionText.text = data1005._highpassHz.options[i].text;
                    //print(item.HighpassHz.ToString()+"************");
                }
            }

            data1005._totalpoints.text = item.TotalPoints.ToString();

            data1005._framenumber.text = item.FrameNumber.ToString();

            data1005._avgnumber.text = item.AvgNumber.ToString();

            data1005._fiberlength1.text = item.FiberLength1.ToString();

            data1005._fiberoffset1.text = item.FiberOffset1.ToString();

            data1005._noiselevel1.text = item.Noiselevel1.ToString();

            data1005._fiberlength2.text = item.FiberLength2.ToString();

            data1005._fiberoffset2.text = item.FiberOffset2.ToString();

            data1005._noiselevel2.text = item.Noiselevel2.ToString();

            data1005._alarmresolution.text = item.AlarmResolution.ToString();

            data1005._minimumalarmcnt.text = item.MinimumAlarmCnt.ToString();

            data1005._durationcnt.text = item.DurationCnt.ToString();


            data1005._dealingwind.isOn = item.DealingWind == 0 ? false : true;


            data1005._windwindow.text = item.WindWindow.ToString();





        }




    }

    
    //保存参数的时候 将面板上的数据更新到List上 并发送2006
    public void Upload1005()
    {

      

      


        int DMAID;
        //发送2006
        if (data1005._dmaid.value == 1)
        {
            DMAID = 2;
        }
        else
        {
            DMAID = 1;
        }

        DeviceID = dataInfo.lstDVSBasicParams[0].DeviceID;
        int Channel = (int)dataInfo.lstDVSZoneInfo[0].ZoneInfo[0].ChannelID;
       
        int _dealingwind = data1005._dealingwind.isOn ? 0 : 1;

        //print(DeviceID + "------" + DMAID + "********" + _dealingwind);

        dataInfo.SendCommand
            (
              DeviceID,
              DMAID,
              int.Parse(data1005._totalpoints.text),
              int.Parse(data1005._framenumber.text),
              int.Parse(data1005._avgnumber.text),
              float.Parse(data1005._highpassHz.captionText.text.ToString()),
              float.Parse(data1005._sanrate.text),
              int.Parse(data1005._pulseWidth.text),
              int.Parse(data1005._fiberlength1.text),
              int.Parse(data1005._fiberoffset1.text),
              int.Parse(data1005._noiselevel1.text),
              int.Parse(data1005._fiberlength2.text),
              int.Parse(data1005._fiberoffset2.text),
              int.Parse(data1005._noiselevel2.text),
              int.Parse(data1005._alarmresolution.text),
              int.Parse(data1005._minimumalarmcnt.text),
              int.Parse(data1005._durationcnt.text),
              _dealingwind,
               int.Parse(data1005._windwindow.text),
               10);



        //dataInfo.SendCommand(2005,DeviceID,Channel);
        // Init1005();


    }


    public void Init1007()
    {

       
        
        for (int i = 0; i <  dataInfo.lstDVSZoneInfo[0].ZoneInfo.Count; i++)
        {

            Data1007 data1007 =   Data1007_.GetChild(i).GetComponent<Data1007>();
            data1007.DeviceId.text = dataInfo.lstDVSZoneInfo[0].DeviceID;
            //print(data1007.transform.name + "***********" + dataInfo.lstDVSZoneInfo[0].DeviceID);
            data1007.ZoneId.text = dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].ZoneID.ToString();
            data1007.ZoneName.text = dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].ZoneName.ToString();
            data1007.FiberStart.text = dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].FiberStart.ToString();
            data1007.FiberEnd.text = dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].FiberEnd.ToString();
            data1007.Threshold.text = dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].Threshold.ToString();
            data1007.Channel.value = (int)dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].ChannelID;
            if (dataInfo.lstDVSZoneInfo[0].ZoneInfo[i].IsAlarm == 0)
            {
                data1007.IsAlarm.isOn = false;
            }
            else
            {
                data1007.IsAlarm.isOn = true;
            }

        }




    }

    public void Update1007()
    {

       
         List<string> zoneInfo_ = new List<string>();

        

        for (int i = 0; i < Data1007_.childCount; i++)
        {

          
            

             DiBo.Model.DVSZoneInfo dVSZoneInfo  = new DiBo.Model.DVSZoneInfo();

             Data1007 data1007 = Data1007_.GetChild(i).GetComponent<Data1007>();

            if (data1007.ZoneId.text !=""|| data1007.ZoneName.text!="" ||data1007.FiberStart.text!=""||data1007.FiberEnd.text!=""||data1007.Threshold.text!="")
            {
                DeviceID = data1007.DeviceId.text.ToString();
                
                dVSZoneInfo.ZoneID = int.Parse(data1007.ZoneId.text) ;
                dVSZoneInfo.ChannelID = data1007.Channel.value;
                dVSZoneInfo.ZoneName = data1007.ZoneName.text;
                dVSZoneInfo.FiberStart = int.Parse(data1007.FiberStart.text);
                dVSZoneInfo.FiberEnd = int.Parse(data1007.FiberEnd.text);
                dVSZoneInfo.Threshold = int.Parse(data1007.Threshold.text);            
                int IsAlarm;
                if (data1007.IsAlarm.isOn)
                {
                    IsAlarm = 1;
                }
                else
                {
                    IsAlarm = 0;
                }
                dVSZoneInfo.IsAlarm = IsAlarm;
              


                JsonSerializer jsonSerializer = new JsonSerializer();

                StringWriter stringWriter = new StringWriter();

                jsonSerializer.Serialize(new JsonTextWriter(stringWriter), dVSZoneInfo);

                string str = stringWriter.GetStringBuilder().ToString();



                zoneInfo_.Add(str);

               
            }
            else
            {
              //  print("没有输入不保存");
            }
          

        }

        dataInfo.SendCommand(DeviceID, zoneInfo_);


    }
    //todo
    //public string  Send2006()
    //{
    //    string msg = "";


    //    return msg;
    //}



    //public void HistoryListUpdate_(int WhichPage)
    //{

    //    //foreach (Transform item in DataParent_History)
    //    //{
    //    //    Destroy(item.gameObject);
    //    //}


    //    HistoryListUpdate(WhichPage);
       

    //}
   




     public void HistoryListUpdate(int WhichPage)
    {


        foreach (Transform item in DataParent_History)
        {
            Destroy(item.gameObject);
        }
            DataTable dataTable = new DataTable();
            dataTable = DiBo.DAL.DVSAlarmInfoReport.GetList(PageManagement.StartDate, PageManagement.EndDate, WhichPage);
            dataInfo.lstHistoryInfo = new List<DiBo.Model.DVSHistoricalData>();

            if (dataTable.Rows.Count > 0)
            {

            PageManagement.LastStartDate = PageManagement.StartDate;
            PageManagement.LastEndDate = PageManagement.EndDate;
            PageManagement.LastWhichPage = WhichPage;
            

            foreach (DataRow row in dataTable.Rows)
             {
             dataInfo.lstHistoryInfo.Add(DiBo.DAL.DVSHistoricalData.DataRowToModel(row));
             }


            
            
            try
            {





                for (int i = 0; i < dataInfo.lstHistoryInfo.Count; i++)
                {

                    HistoricalData lstHistoryInfo_ = new HistoricalData();

                    //print(DataParent_History.childCount + "**********" + dataInfo.lstHistoryInfo.Count);
                    GameObject OBJ = Instantiate(Resources.Load("历史数据")) as GameObject;
                    data = OBJ.transform;
                    data.parent = DataParent_History;
                    lstHistoryInfo_ = data.GetComponent<HistoricalDataInfo>().HistoricalDataInfo_;




                    if (dataInfo.lstHistoryInfo[i].ChannelID.ToString().Contains("0"))
                    {
                        lstHistoryInfo_._channelid = "通道一";
                    }
                    else
                    {
                        lstHistoryInfo_._channelid = "通道二";
                    }

                    lstHistoryInfo_._zoneid = dataInfo.lstHistoryInfo[i].ZoneID.ToString();
                    lstHistoryInfo_._alarmdatetime = dataInfo.lstHistoryInfo[i].AlarmDateTime.ToString();
                    lstHistoryInfo_._fiberposition = dataInfo.lstHistoryInfo[i].FiberPosition.ToString();
                    lstHistoryInfo_._alarmtype = dataInfo.lstHistoryInfo[i].AlarmType.ToString();


                    lstHistoryInfo_._maxamptitude = dataInfo.lstHistoryInfo[i].MaxAmptitude.ToString();
                    lstHistoryInfo_._alarmendtime = dataInfo.lstHistoryInfo[i].AlarmEndTime.ToString();


                    if (dataInfo.lstHistoryInfo[i].AlarmLevel.ToString().Contains("0"))
                    {

                        lstHistoryInfo_._alarmlevel = "预警";
                    }
                    else
                    {
                        lstHistoryInfo_._alarmlevel = "报警";
                    }
                    if (dataInfo.lstHistoryInfo[i].AlarmLevel.ToString().Contains("0") || dataInfo.lstHistoryInfo[i].DealingStatus == 1)
                    {


                        //foreach (Transform item in data)
                        //{
                        //    if (item.GetComponent<Image>())
                        //    {
                        //        item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().NoSelectEffect;
                        //    }
                        //}
                        foreach (Transform item in data)
                        {
                            if (item.Find("Text") != null)
                            {
                                item.Find("Text").GetComponent<Text>().color = Color.white;
                            }
                        }



                    }
                    else
                    {

                        Color color = new Color();
                        ColorUtility.TryParseHtmlString("#EF1F1F", out color);
                        foreach (Transform item in data)
                        {
                            if (item.Find("Text") != null)
                            {

                                item.Find("Text").GetComponent<Text>().color = color;
                            }
                        }
                        //foreach (Transform item in data)
                        //{
                        //    if ( item.GetComponent<Image>())
                        //    {
                        //         item.GetComponent<Image>().sprite = data.GetComponent<ItemData>().SelectEffect;
                        //    }

                        //}
                    }

                    if (dataInfo.lstHistoryInfo[i].AlarmStatus.ToString().Contains("1"))
                    {
                        lstHistoryInfo_._alarmstatus = "报警进行中";
                    }
                    else if (dataInfo.lstHistoryInfo[i].AlarmStatus.ToString().Contains("0"))
                    {
                        lstHistoryInfo_._alarmstatus = "报警开始";
                    }
                    else
                    {
                        lstHistoryInfo_._alarmstatus = "报警结束";
                    }


                    //TODO

                    lstHistoryInfo_._alarminfoid = dataInfo.lstHistoryInfo[i].AlarmInfoID.ToString();

                    lstHistoryInfo_._vcount = dataInfo.lstHistoryInfo[i].VCount.ToString();

                    if (dataInfo.lstHistoryInfo[i].DealingStatus == 0)
                    {
                        lstHistoryInfo_._dealingstatus = "未处理";
                    }
                    else
                    {
                        lstHistoryInfo_._dealingstatus = "已处理";
                    }

                    //Debug.Log(dataInfo.lstAlarmInfo[i].DeviceID.ToString() + "************");

                    //data1002._zonename = dataInfo.lstAlarmInfo[i].ZoneName.ToString();

                    data.GetComponent<HistoricalDataInfo>().UpdateInfo();
                }
            }
            catch (System.Exception ex)
            {

                Debug.Log(ex.ToString());
            }
            HistoyScrollBar_.value = 1;
            }

      




    }
    /// <summary>
    /// 更新和初始化振动曲线折线图
    /// </summary>
   public  void InitVibrationCurveData()
    {

        VibrationCurveData = new List<int>();
        int Index = dataInfo.lstVibrationCurve.Count - 1;
        int[] jsonData = JsonMapper.ToObject<int[]>(dataInfo.lstVibrationCurve[Index].CurveData);

        foreach (int i in jsonData)
        {
           
            VibrationCurveData.Add(i);
        }
        int  Resolution = int.Parse(dataInfo.lstVibrationCurve[Index].Resolution.ToString());
        var xAxis = Vibrationchart.GetChartComponent<XAxis>();
        xAxis.ClearData();
    
        Vibrationchart.GetChartComponent<DataZoom>().Reset();

        if (Vibrationchart.GetSerie(0).dataCount.Equals(VibrationCurveData.Count))
        {
          

           
            for (int i = 0; i < VibrationCurveData.Count; i++)
            {

                print(VibrationCurveData[i]);
                Vibrationchart.AddXAxisData((i * Resolution+1).ToString());
                Vibrationchart.UpdateData(0, i, VibrationCurveData[i]);
            }

        }
        else
        {


            print("初始化数据");
            Vibrationchart.GetSerie(0).ClearData();

           



            for (int i = 0; i < VibrationCurveData.Count; i++)
            {

                Vibrationchart.AddXAxisData((i * Resolution+1).ToString());//增加X轴向下的数据
                Vibrationchart.AddData(0, VibrationCurveData[i]);
            }

        }
         Vibrationchart.GetChartComponent<Tooltip>().Reset();


    }
    /// <summary>
    /// 初始化和更新原始信号数据
    /// </summary>
    public void InitSignalCurveData()
    {



        SignalCurveData = new List<int>();

        int[] jsonData = JsonMapper.ToObject<int[]>(dataInfo.lstRawSignalCurve[0].CurveData);

        foreach (int i in jsonData)
        {

            SignalCurveData.Add(i);
        }
        int Resolution = int.Parse(dataInfo.lstRawSignalCurve[0].Resolution.ToString());
        var xAxis = Signalchart.GetChartComponent<XAxis>();
        xAxis.ClearData();

        Signalchart.GetChartComponent<DataZoom>().Reset();

        if (Signalchart.GetSerie(0).dataCount.Equals(SignalCurveData.Count))
        {



            for (int i = 0; i < SignalCurveData.Count; i++)
            {
                Signalchart.AddXAxisData((i * Resolution + 1).ToString());
                Signalchart.UpdateData(0, i, SignalCurveData[i]);
            }

        }
        else
        {


            print("初始化数据");
            Signalchart.GetSerie(0).ClearData();
            for (int i = 0; i < SignalCurveData.Count; i++)
            {

                Signalchart.AddXAxisData((i * Resolution + 1).ToString());//增加X轴向下的数据
                Signalchart.AddData(0, SignalCurveData[i]);
            }

        }
        Signalchart.GetChartComponent<Tooltip>().Reset();


    }
    public void InitBigSignalCurveData()
    {

        print(SignalCureDataShow.activeSelf);
        if (SignalCureDataShow.activeSelf)
        {
           
            BigSignalCurveData = new List<int>();

            int[] jsonData = JsonMapper.ToObject<int[]>(dataInfo.lstRawSignalCurve[0].CurveData);

            foreach (int i in jsonData)
            {

                BigSignalCurveData.Add(i);
            }
            int Resolution = int.Parse(dataInfo.lstRawSignalCurve[0].Resolution.ToString());
            var xAxis = BigSignalchart.GetChartComponent<XAxis>();
            xAxis.ClearData();

            BigSignalchart.GetChartComponent<DataZoom>().Reset();
             print(BigSignalchart.GetSerie(0).dataCount + "-----------" + BigSignalCurveData.Count);
            if (BigSignalchart.GetSerie(0).dataCount.Equals(BigSignalCurveData.Count))
            {



                for (int i = 0; i < BigSignalCurveData.Count; i++)
                {
                    BigSignalchart.AddXAxisData((i * Resolution + 1).ToString());
                    BigSignalchart.UpdateData(0, i, BigSignalCurveData[i]);
                }

            }
            else
            {


                print("初始化数据");
                BigSignalchart.GetSerie(0).ClearData();
                for (int i = 0; i < BigSignalCurveData.Count; i++)
                {

                    BigSignalchart.AddXAxisData((i * Resolution + 1).ToString());//增加X轴向下的数据
                    BigSignalchart.AddData(0, BigSignalCurveData[i]);
                }

            }
            BigSignalchart.GetChartComponent<Tooltip>().Reset();
        }

       


    }


    public IEnumerator SendGet2003()
    { 
    
    

        while (true)
        {

            yield return new WaitForSeconds(10.0f);
            dataInfo.SendCommand(2003);
            dataInfo.SendCommand(2004);
        }
    }

    public void ClickSignalCurveBtn()
    { 
    
      SignalCureDataShow.SetActive(true);
      InitBigSignalCurveData();


    
    }



    public void UpdateBarChart()
    {

        for (int i = dataInfo.lstVibrationCurve.Count - 1; i >= 0; i--)
        {

            InitVibrationCurveBarChartData(i);
        }
    }

    /// <summary>
    /// 更新柱状图
    /// </summary>
    public void InitVibrationCurveBarChartData(int Index)
    {
        print(Index+"***************");
        VibrationCurveBarChartData = new List<int>();
      
        int[] jsonData = JsonMapper.ToObject<int[]>(dataInfo.lstVibrationCurve[Index].CurveData);

        //foreach (int i in jsonData)
        //{

        //    VibrationCurveBarChartData.Add(i);
        //}
        for (int i = 0; i < jsonData.Length; i++)
        {


            if (i<10)
            {
                VibrationCurveBarChartData.Add(jsonData[i]);
            }
        }

        int Resolution = 1;
       
        var xAxis = VibrationBarchart.GetChartComponent<XAxis>();

        xAxis.ClearData();



      
        

        if (dataInfo.lstVibrationCurve[Index].Resolution.ToString()!=null)
        {
              Resolution = int.Parse(dataInfo.lstVibrationCurve[Index].Resolution.ToString());
        }
            
        if (VibrationBarchart.GetSerie(Index).dataCount.Equals(VibrationCurveBarChartData.Count))
        {



            for (int i = 0; i < VibrationCurveBarChartData.Count; i++)
            {

                print(VibrationCurveBarChartData[i]);

                VibrationBarchart.AddXAxisData((i * Resolution + 1).ToString());
                VibrationBarchart.UpdateData(Index, i, VibrationCurveBarChartData[i]);
            }

        }
        else
        {


            print("初始化数据");
            VibrationBarchart.GetSerie(Index).ClearData();





            for (int i = 0; i < VibrationCurveBarChartData.Count; i++)
            {

                VibrationBarchart.AddXAxisData((i * Resolution + 1).ToString());//增加X轴向下的数据
                VibrationBarchart.AddData(Index, VibrationCurveBarChartData[i]);
            }

        }
       // Vibrationchart.GetChartComponent<Tooltip>().Reset();


    }


    public void UpdateRunningStatus()
    { 
      GameObject RunningStatus = GameObject.Instantiate(Resources.Load("运行状态")) as GameObject;
    
    
    
    
    
    
    }
}
