
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
    //外层大括号必须是两个，否是格式化出错

    /// <summary>
	/// 2003：获取振动曲线（服务端主动），参数1：DeviceID:String，参数2：ChannelID:Int
	/// </summary>
	public static string Cmd2003 { get { return "{{\"CommandCode\":\"2003\",\"DeviceID\":\"{0}\",\"ChannelID\":{1}}}"; } }

    /// <summary>
    /// 2004：获取原始信号曲线（服务端主动），参数1：DeviceID:String，参数2：ChannelID:Int
    /// </summary>
    public static string Cmd2004 { get { return "{{\"CommandCode\":\"2004\",\"DeviceID\":\"{0}\",\"ChannelID\":{1}}}"; } }

    /// <summary>
    /// 2005: 获取DVS设备基本参数（服务端主动），参数1：DeviceID:String
    /// </summary>
	public static string Cmd2005 { get { return "{{\"CommandCode\":\"2005\",\"DeviceID\":\"{0}\"}}"; } }

    /// <summary>
    /// 2006：更新DVS设备基本参数，参数列表：DeviceID:主机ID:String，DMAID:数据源ID:Int，TotalPoints:采样点数:Int，FrameNumber:帧数:Int，AvgNumber:平均点数:Int，HighpassHz:滤波参数:Float，Scanrate:扫描频率:Float，PulseWidth:脉冲宽度:Int，FiberLength1:光纤长度1:Int，FiberOffset1:光纤偏移1:Int，Noiselevel1:噪声水平1:Int，FiberLength2:光纤长度2:Int，FiberOffset2:光纤偏移2:Int，Noiselevel2:噪声水平2:Int，AlarmResolution:报警位置分辨率:Int，MinimumAlarmCnt:至少报警次数:Int，DurationCnt:振动持续次数:Int，DealingWind:是否进行大风处理:Int，WindWindow:大风窗口:Int，WindMinimumCnt:大风报警点数:Int
    /// </summary>
    public static string Cmd2006 { get { return "{{\"CommandCode\":\"2006\",\"DeviceID\":\"{0}\",\"DMAID\":{1},\"TotalPoints\":{2},\"FrameNumber\":{3},\"AvgNumber\":{4},\"HighpassHz\":{5},\"Scanrate\":{6},\"PulseWidth\":{7},\"FiberLength1\":{8},\"FiberOffset1\":{9},\"Noiselevel1\":{10},\"FiberLength2\":{11},\"FiberOffset2\":{12},\"Noiselevel2\":{13},\"AlarmResolution\":{14},\"MinimumAlarmCnt\":{15},\"DurationCnt\":{16},\"DealingWind\":{17},\"WindWindow\":{18},\"WindMinimumCnt\":{19}}}"; } }

    /// <summary>
    /// 2007：获取DVS分区信息（服务端主动），参数1：DeviceID:String
    /// </summary>
    public static string Cmd2007 { get { return "{{\"CommandCode\":\"2007\",\"DeviceID\":\"{0}\"}}"; } }

    /// <summary>
    /// 2008：更新DVS分区参数，参数1：DeviceID:String，参数2：ZoneInfo:Array,内容为 Cmd2008Sub,Cmd2008Sub,Cmd2008Sub,...
    /// </summary>
    public static string Cmd2008 { get { return "{{\"CommandCode\":\"2008\",\"DeviceID\":\"{0}\",\"ZoneInfo\":[{1}]}}"; } }

    /// <summary>
    /// ZoneInfo 分区参数，参数列表：ZoneID:分区ID:Int，ChannelID:通道ID:Int，ZoneName:分区名称:String，FiberStart:分区光纤起点:Int，FiberEnd:分区光纤终点:Int，Threshold:分区阈值:Int，IsAlarm:撤防/布防:Int
    /// </summary>
    public static string Cmd2008Sub { get { return "{{\"ZoneID\":{0},\"ChannelID\":{1},\"ZoneName\":\"{2}\",\"FiberStart\":{3},\"FiberEnd\":{4},\"Threshold\":{5},\"IsAlarm\":{6}}}"; } }
	#endregion
}
