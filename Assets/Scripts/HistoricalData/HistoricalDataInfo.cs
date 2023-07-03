using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoricalDataInfo : MonoBehaviour
{

    public HistoricalData HistoricalDataInfo_;
    [Header("报警序号")]
    public Text _alarminfoid;
    [Header("通道ID")]
    public Text _channelid;

    [Header("分区ID（编号）")]
    public Text _zoneid;

    [Header("报警时间")]
    public Text _alarmdatetime;

    [Header("报警光纤位置")]
    public Text _fiberposition;

    [Header("报警类型")]
    public Text _alarmtype;

    [Header("最大振幅")]
    public Text _maxamptitude;

    [Header("报警最新时间")]
    public Text _alarmendtime;

    [Header("报警级别")]
    public Text _alarmlevel;

    [Header("报警状态")]
    public Text _alarmstatus;

    [Header("振动次数")]
    public Text _vcount;

    [Header("处理状态")]
    public Text _dealingstatus;

    [Header("分区名称")]
    public Text _zonename;
    void OnEnable()
    {

        //print("******************" + Data1002_._channelid);


    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {



    }


    public void UpdateInfo()
    {

        _alarminfoid.text = HistoricalDataInfo_._alarminfoid;
        _zoneid.text = HistoricalDataInfo_._zoneid;
        _alarmdatetime.text = HistoricalDataInfo_._alarmdatetime;
        _fiberposition.text = HistoricalDataInfo_._fiberposition;
        _alarmtype.text = HistoricalDataInfo_._alarmtype;
        _maxamptitude.text = HistoricalDataInfo_._maxamptitude;
        _alarmendtime.text = HistoricalDataInfo_._alarmendtime;
        _alarmlevel.text = HistoricalDataInfo_._alarmlevel;
        _alarmstatus.text = HistoricalDataInfo_._alarmstatus;
        _channelid.text = HistoricalDataInfo_._channelid;
        _vcount.text = HistoricalDataInfo_._vcount;
        _dealingstatus.text = HistoricalDataInfo_._dealingstatus;
        _zonename.text = HistoricalDataInfo_._zonename;
        //print("已经更新到了面板Text上");



    }


}
