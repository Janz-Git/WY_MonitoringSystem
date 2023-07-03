using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class HistoricalData : ScriptableObject
{

    [Header("报警序号")]
    public string _alarminfoid;
    [Header("通道ID")]
    public string _channelid;

    [Header("分区ID（编号）")]
    public string _zoneid;

    [Header("报警时间")]
    public string _alarmdatetime;

    [Header("报警光纤位置")]
    public string _fiberposition;

    [Header("报警类型")]
    public string _alarmtype;

    [Header("最大振幅")]
    public string _maxamptitude;

    [Header("报警最新时间")]
    public string _alarmendtime;

    [Header("报警级别")]
    public string _alarmlevel;

    [Header("报警状态")]
    public string _alarmstatus;

    [Header("振动次数")]
    public string _vcount;

    [Header("处理状态")]
    public string _dealingstatus;

    [Header("分区名称")]
    public string _zonename;
}
