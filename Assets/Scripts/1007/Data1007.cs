using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Data1007 : MonoBehaviour
{


    [Header("主机ID")]
    public InputField DeviceId;
     [Header("分区ID")]
    public InputField ZoneId;
    [Header("分区名称")]
    public InputField ZoneName;
    [Header("通道ID")]
    public Dropdown Channel;
    [Header("分区光纤终点")]
    public InputField FiberStart;
    [Header("分区光纤终点")]
    public InputField FiberEnd;
    [Header("分区阈值")]
    public InputField Threshold;
    [Header("撤防/布防")]
    public  Toggle IsAlarm;


}
