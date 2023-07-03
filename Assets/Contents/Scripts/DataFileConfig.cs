using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFileConfig
{
    public static string CacheFilePath { get { return Application.streamingAssetsPath + "/CacheFiles/"; } }

    #region cache file name

    /// <summary>
    /// 1001：设备信息上报（客户端主动）
    /// </summary>
    public static string fDeviceInfoReport = "DeviceInfoReport.txt";

    /// <summary>
    /// 1002：报警信息上报（客户端主动）
    /// </summary>
    public static string fAlarmInfoReport = "AlarmInfoReport.txt";

    /// <summary>
    /// 1003：返回振动曲线（客户端被动）
    /// </summary>
    public static string fVibrationCurveReturn = "VibrationCurveReturn.txt";

    /// <summary>
    /// 1004：返回原始信号曲线（客户端被动）
    /// </summary>
    public static string fRawSignalCurveReturn = "RawSignalCurveReturn.txt";

    /// <summary>
    /// 1005：返回DVS设备基本参数（客户端被动）
    /// </summary>
    public static string fDVSBasicParamsReturn = "DVSBasicParamsReturn.txt";

    /// <summary>
    /// 1007：返回DVS分区参数（客户端被动）
    /// </summary>
    public static string fDVSZoneInfoReturn = "DVSZoneInfoReturn.txt";


    public static string MapImageName { get { return "Map.png"; } }
    #endregion
}
