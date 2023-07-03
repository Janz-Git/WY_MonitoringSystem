
public class DVSCommands
{
    public enum CommandType
    {
        Type2003 = 2003,
        Type2004 = 2004,
        Type2005 = 2005,
        Type2006 = 2006,
        Type2007 = 2007,
        Type2008 = 2008
    }

    #region DVS Commands
    //�������ű��������������Ǹ�ʽ������

    /// <summary>
	/// 2003����ȡ�����ߣ������������������1��DeviceID:String������2��ChannelID:Int
	/// </summary>
	public static string Cmd2003 { get { return "{{\"CommandCode\":\"2003\",\"DeviceID\":\"{0}\",\"ChannelID\":{1}}}"; } }

    /// <summary>
    /// 2004����ȡԭʼ�ź����ߣ������������������1��DeviceID:String������2��ChannelID:Int
    /// </summary>
    public static string Cmd2004 { get { return "{{\"CommandCode\":\"2004\",\"DeviceID\":\"{0}\",\"ChannelID\":{1}}}"; } }

    /// <summary>
    /// 2005: ��ȡDVS�豸���������������������������1��DeviceID:String
    /// </summary>
	public static string Cmd2005 { get { return "{{\"CommandCode\":\"2005\",\"DeviceID\":\"{0}\"}}"; } }

    /// <summary>
    /// 2006������DVS�豸���������������б�DeviceID:����ID:String��DMAID:����ԴID:Int��TotalPoints:��������:Int��FrameNumber:֡��:Int��AvgNumber:ƽ������:Int��HighpassHz:�˲�����:Float��Scanrate:ɨ��Ƶ��:Float��PulseWidth:������:Int��FiberLength1:���˳���1:Int��FiberOffset1:����ƫ��1:Int��Noiselevel1:����ˮƽ1:Int��FiberLength2:���˳���2:Int��FiberOffset2:����ƫ��2:Int��Noiselevel2:����ˮƽ2:Int��AlarmResolution:����λ�÷ֱ���:Int��MinimumAlarmCnt:���ٱ�������:Int��DurationCnt:�񶯳�������:Int��DealingWind:�Ƿ���д�紦��:Int��WindWindow:��細��:Int��WindMinimumCnt:��籨������:Int
    /// </summary>
    public static string Cmd2006 { get { return "{{\"CommandCode\":\"2006\",\"DeviceID\":\"{0}\",\"DMAID\":{1},\"TotalPoints\":{2},\"FrameNumber\":{3},\"AvgNumber\":{4},\"HighpassHz\":{5},\"Scanrate\":{6},\"PulseWidth\":{7},\"FiberLength1\":{8},\"FiberOffset1\":{9},\"Noiselevel1\":{10},\"FiberLength2\":{11},\"FiberOffset2\":{12},\"Noiselevel2\":{13},\"AlarmResolution\":{14},\"MinimumAlarmCnt\":{15},\"DurationCnt\":{16},\"DealingWind\":{17},\"WindWindow\":{18},\"WindMinimumCnt\":{19}}}"; } }

    /// <summary>
    /// 2007����ȡDVS������Ϣ�������������������1��DeviceID:String
    /// </summary>
    public static string Cmd2007 { get { return "{{\"CommandCode\":\"2007\",\"DeviceID\":\"{0}\"}}"; } }

    /// <summary>
    /// 2008������DVS��������������1��DeviceID:String������2��ZoneInfo:Array,����Ϊ Cmd2008Sub,Cmd2008Sub,Cmd2008Sub,...
    /// </summary>
    public static string Cmd2008 { get { return "{{\"CommandCode\":\"2008\",\"DeviceID\":\"{0}\",\"ZoneInfo\":[{1}]}}"; } }

    /// <summary>
    /// ZoneInfo ���������������б�ZoneID:����ID:Int��ChannelID:ͨ��ID:Int��ZoneName:��������:String��FiberStart:�����������:Int��FiberEnd:���������յ�:Int��Threshold:������ֵ:Int��IsAlarm:����/����:Int
    /// </summary>
    public static string Cmd2008Sub { get { return "{{\"ZoneID\":{0},\"ChannelID\":{1},\"ZoneName\":\"{2}\",\"FiberStart\":{3},\"FiberEnd\":{4},\"Threshold\":{5},\"IsAlarm\":{6}}}"; } }
	#endregion
}
