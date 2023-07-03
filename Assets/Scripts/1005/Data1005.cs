using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Data1005 : MonoBehaviour
{
    //[Header("命令码")]
    //public Text _commandcode;

    //[Header("主机ID")]
    //public Text _deviceid;
    [Header("数据源ID")]
    public Dropdown _dmaid;
    [Header("采样点数")]
    public InputField _totalpoints;
    [Header("帧数")]
    public InputField _framenumber;
    [Header("平均点数")]
    public InputField _avgnumber;

    //[Header("滤波参数")]
    //public InputField _highpasshz;
    //[Header("扫描频率")]
    //public InputField _scanrate;
    //[Header("脉冲宽度")]
    //public InputField _pulsewidth;
    [Header("光纤长度1")]
    public InputField _fiberlength1;
    [Header("光纤偏移1")]
    public InputField _fiberoffset1;
    [Header("噪声水平1")]
    public InputField _noiselevel1;
    [Header("光纤长度2")]
    public InputField _fiberlength2;
    [Header("光纤偏移2")]
    public InputField _fiberoffset2;
    [Header("噪声水平2")]
    public InputField _noiselevel2;
    [Header("报警位置分辨率")]
    public InputField _alarmresolution;
    [Header("至少报警次数")]
    public InputField _minimumalarmcnt;
    [Header("振动持续次数")]
    public InputField _durationcnt;
    [Header("是否进行大风处理")]
    public Toggle _dealingwind;
    [Header("大风窗口")]
    public InputField _windwindow;
    //[Header("大风报警点数")]
    //public InputField _windminimumcnt;

    [Header("滤波参数")]
    public Dropdown _highpassHz;

    [Header("扫描频率")]
     public InputField _sanrate;

    [Header("脉冲宽度")]
    public InputField _pulseWidth;


}
