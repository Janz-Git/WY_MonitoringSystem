using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


/// <summary>
/// 通用函数集
/// </summary>
public class CommonFunc
{
    #region 数据类型转换相关
    /// <summary>
    /// 2字节数组转int16
    /// </summary>
    /// <param name="bytes">长度为2的字节数组</param>
    /// <returns></returns>
    public static int ByteToInt16(byte[] bytes)
    {
        if (bytes.Length != 2)
            return -1;
        return BitConverter.ToInt16(bytes, 0);
    }

    /// <summary>
    /// 4字节数组转int32
    /// </summary>
    /// <param name="bytes">长度为4的字节数组</param>
    /// <returns></returns>
    public static int ByteToInt32(byte[] bytes)
    {
        if (bytes.Length != 4)
            return -1;
        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// 4字节数组转float
    /// </summary>
    /// <param name="bytes">长度为4的字节数组</param>
    /// <returns></returns>
    public static float ByteToFloat(byte[] bytes)
    {
        if (bytes.Length != 4)
            return -1f;
        return BitConverter.ToSingle(bytes, 0);
    }

    /// <summary>
    /// 字节数组转16进制字符串，默认以空格分割
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="addSpace">是否以空格分割</param>
    /// <returns></returns>
    public static string ByteToHex(byte[] bytes, bool addSpace = true)
    {
        int capacity = bytes.Length * 2;
        StringBuilder sb = new StringBuilder(capacity);

        if (bytes != null)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
                if (addSpace)
                    sb.Append(" ");
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 将字节数组转换为以逗号分隔的字符串，{1,2,3} → "1,2,3"
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string ByteToString(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString());
            sb.Append(',');
        }
        return sb.ToString().TrimEnd(',');
    }

    /// <summary>
    /// float转为字节数组
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static byte[] FloatToByte(float f)
    {
        return BitConverter.GetBytes(f);
    }

    /// <summary>
    /// 16进制以空格分割的字符串转字节数组
    /// </summary>
    /// <param name="hexstring"></param>
    /// <returns></returns>
    public static byte[] HexToByte(string hexstring)
    {

        string[] tmpary = hexstring.Trim().Split(' ');
        byte[] buff = new byte[tmpary.Length];
        for (int i = 0; i < buff.Length; i++)
        {
            buff[i] = Convert.ToByte(tmpary[i], 16);
        }
        return buff;
    }

    /// <summary>
    /// 十六进制字符串转十进制字符串
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static string HexToDecString(string hex, bool addSpace = true)
    {
        string[] strs = hex.Split(' ');
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < strs.Length; i++)
        {
            sb.Append(Convert.ToInt32(strs[i], 16));
            if (addSpace)
                sb.Append(" ");
        }
        return sb.ToString();
    }
    #endregion

    #region 文件操作相关

    /// <summary>
    /// 将字节数组原内容保存到文件，以空格分割
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="len">字节数组中有效数据长度</param>
    /// <param name="strFileName"></param>
    /// <param name="maxSaveLength">最大保存长度，单位K，例如1000，这表示文件最大为1000Kb</param>
    public static void SaveRawBytesToFile(byte[] bytes, int len, string strFileName, int maxSaveLength = 1000)
    {
        DirectoryInfo di = new DirectoryInfo(DataFileConfig.CacheFilePath);
        if (!di.Exists)
            di.Create();

        FileInfo file = new FileInfo(DataFileConfig.CacheFilePath + strFileName);

        if (!file.Exists)
            file.Create().Dispose();
        else
        {
            if (file.Length > maxSaveLength * 1024)//文件最大保留1000k
            {
                file.Delete();
                file.Create().Dispose();
            }
        }

        string byteInfo = "";
        for (int i = 0; i < len; i++)
        {
            byteInfo += bytes[i].ToString() + " ";
        }

        using (FileStream fs = new FileStream(DataFileConfig.CacheFilePath + strFileName, FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(byteInfo.TrimEnd());
            }
        }
    }


    /// <summary>
    /// 将字节数组以文件流形式以默认编码保存到文件，只有 append 参数为 true 时 maxSaveLength 参数才会生效
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="strFileName"></param>
    /// <param name="maxSaveLength">最大保存的文件大小，单位Kb</param>
    public static void SaveBytesToFile(byte[] bytes, string strFileName, bool append = true, int maxSaveLength = 1000)
    {
        DirectoryInfo di = new DirectoryInfo(DataFileConfig.CacheFilePath);
        if (!di.Exists)
            di.Create();

        FileInfo file = new FileInfo(DataFileConfig.CacheFilePath + strFileName);

        if (!file.Exists)
            file.Create().Dispose();
        else
        {
            if (append && (file.Length > maxSaveLength * 1024))//文件最大保留 maxSaveLength  k
            {
                file.Delete();
                file.Create().Dispose();
            }
        }

        using (FileStream fs = new FileStream(DataFileConfig.CacheFilePath + strFileName, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write))
        {
            //将bytes数组写入到fs
            fs.Write(bytes, 0, bytes.Length);
            //写入换行
            fs.WriteByte(13);
            fs.WriteByte(10);
            fs.Flush();
        }
    }

    /// <summary>
    /// 字节数组保存为16进制字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static void SaveBytesToHex(byte[] bytes, string fileName, int maxSaveLength)
    {
        DirectoryInfo di = new DirectoryInfo(DataFileConfig.CacheFilePath);
        if (!di.Exists)
            di.Create();

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(string.Format("{0:X2} ", bytes[i]));
        }
        string path = DataFileConfig.CacheFilePath + fileName;
        FileInfo file = new FileInfo(path);

        if (!file.Exists)
            file.Create().Dispose();
        else
        {
            if (file.Length > maxSaveLength * 1024)//文件最大保留 maxSaveLength  k
            {
                file.Delete();
                file.Create().Dispose();
            }
        }

        using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(builder.ToString());
            }
        }
    }

    /// <summary>
    /// 从文件读取数据
    /// </summary>
    /// <param name="strFileName"></param>
    /// <returns></returns>
    public static byte[] LoadDataFromFile(string strFileName)
    {
        byte[] bytes;

        FileInfo file = new FileInfo(DataFileConfig.CacheFilePath + strFileName);

        if (file.Exists)
        {
            FileStream fs = new FileStream(DataFileConfig.CacheFilePath + strFileName, FileMode.Open);

            long lSize = fs.Length;
            bytes = new byte[lSize];

            //从文件流中读取字节块并写入到bytes数组
            fs.Read(bytes, 0, bytes.Length);

            fs.Flush();
            fs.Dispose();
        }
        else
            bytes = new byte[1];

        return bytes;
    }
    #endregion

    #region MD5 加密
    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string MD5Encrypt32(string password)
    {
        string cl = password;
        string pwd = "";
        MD5 md5 = MD5.Create(); //实例化一个md5对像
                                // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int i = 0; i < s.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            pwd = pwd + s[i].ToString("X");
        }
        return pwd;
    }

    #endregion
}
