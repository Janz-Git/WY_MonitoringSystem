using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFileConfig
{
    public static string CacheFilePath { get { return Application.streamingAssetsPath + "/CacheFiles/"; } }

    #region cache file name

    /// <summary>
    /// 1001���豸��Ϣ�ϱ����ͻ���������
    /// </summary>
    public static string fDeviceInfoReport = "DeviceInfoReport.txt";

    /// <summary>
    /// 1002��������Ϣ�ϱ����ͻ���������
    /// </summary>
    public static string fAlarmInfoReport = "AlarmInfoReport.txt";

    /// <summary>
    /// 1003�����������ߣ��ͻ��˱�����
    /// </summary>
    public static string fVibrationCurveReturn = "VibrationCurveReturn.txt";

    /// <summary>
    /// 1004������ԭʼ�ź����ߣ��ͻ��˱�����
    /// </summary>
    public static string fRawSignalCurveReturn = "RawSignalCurveReturn.txt";

    /// <summary>
    /// 1005������DVS�豸�����������ͻ��˱�����
    /// </summary>
    public static string fDVSBasicParamsReturn = "DVSBasicParamsReturn.txt";

    /// <summary>
    /// 1007������DVS�����������ͻ��˱�����
    /// </summary>
    public static string fDVSZoneInfoReturn = "DVSZoneInfoReturn.txt";


    public static string MapImageName { get { return "Map.png"; } }
    #endregion
}
