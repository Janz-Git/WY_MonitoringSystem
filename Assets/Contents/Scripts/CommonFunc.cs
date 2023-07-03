using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


/// <summary>
/// ͨ�ú�����
/// </summary>
public class CommonFunc
{
    #region ��������ת�����
    /// <summary>
    /// 2�ֽ�����תint16
    /// </summary>
    /// <param name="bytes">����Ϊ2���ֽ�����</param>
    /// <returns></returns>
    public static int ByteToInt16(byte[] bytes)
    {
        if (bytes.Length != 2)
            return -1;
        return BitConverter.ToInt16(bytes, 0);
    }

    /// <summary>
    /// 4�ֽ�����תint32
    /// </summary>
    /// <param name="bytes">����Ϊ4���ֽ�����</param>
    /// <returns></returns>
    public static int ByteToInt32(byte[] bytes)
    {
        if (bytes.Length != 4)
            return -1;
        return BitConverter.ToInt32(bytes, 0);
    }

    /// <summary>
    /// 4�ֽ�����תfloat
    /// </summary>
    /// <param name="bytes">����Ϊ4���ֽ�����</param>
    /// <returns></returns>
    public static float ByteToFloat(byte[] bytes)
    {
        if (bytes.Length != 4)
            return -1f;
        return BitConverter.ToSingle(bytes, 0);
    }

    /// <summary>
    /// �ֽ�����ת16�����ַ�����Ĭ���Կո�ָ�
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="addSpace">�Ƿ��Կո�ָ�</param>
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
    /// ���ֽ�����ת��Ϊ�Զ��ŷָ����ַ�����{1,2,3} �� "1,2,3"
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
    /// floatתΪ�ֽ�����
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static byte[] FloatToByte(float f)
    {
        return BitConverter.GetBytes(f);
    }

    /// <summary>
    /// 16�����Կո�ָ���ַ���ת�ֽ�����
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
    /// ʮ�������ַ���תʮ�����ַ���
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

    #region �ļ��������

    /// <summary>
    /// ���ֽ�����ԭ���ݱ��浽�ļ����Կո�ָ�
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="len">�ֽ���������Ч���ݳ���</param>
    /// <param name="strFileName"></param>
    /// <param name="maxSaveLength">��󱣴泤�ȣ���λK������1000�����ʾ�ļ����Ϊ1000Kb</param>
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
            if (file.Length > maxSaveLength * 1024)//�ļ������1000k
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
    /// ���ֽ��������ļ�����ʽ��Ĭ�ϱ��뱣�浽�ļ���ֻ�� append ����Ϊ true ʱ maxSaveLength �����Ż���Ч
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="strFileName"></param>
    /// <param name="maxSaveLength">��󱣴���ļ���С����λKb</param>
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
            if (append && (file.Length > maxSaveLength * 1024))//�ļ������ maxSaveLength  k
            {
                file.Delete();
                file.Create().Dispose();
            }
        }

        using (FileStream fs = new FileStream(DataFileConfig.CacheFilePath + strFileName, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write))
        {
            //��bytes����д�뵽fs
            fs.Write(bytes, 0, bytes.Length);
            //д�뻻��
            fs.WriteByte(13);
            fs.WriteByte(10);
            fs.Flush();
        }
    }

    /// <summary>
    /// �ֽ����鱣��Ϊ16�����ַ���
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
            if (file.Length > maxSaveLength * 1024)//�ļ������ maxSaveLength  k
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
    /// ���ļ���ȡ����
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

            //���ļ����ж�ȡ�ֽڿ鲢д�뵽bytes����
            fs.Read(bytes, 0, bytes.Length);

            fs.Flush();
            fs.Dispose();
        }
        else
            bytes = new byte[1];

        return bytes;
    }
    #endregion

    #region MD5 ����
    /// <summary>
    /// 32λMD5����
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string MD5Encrypt32(string password)
    {
        string cl = password;
        string pwd = "";
        MD5 md5 = MD5.Create(); //ʵ����һ��md5����
                                // ���ܺ���һ���ֽ����͵����飬����Ҫע�����UTF8/Unicode�ȵ�ѡ��
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        // ͨ��ʹ��ѭ�������ֽ����͵�����ת��Ϊ�ַ��������ַ����ǳ����ַ���ʽ������
        for (int i = 0; i < s.Length; i++)
        {
            // ���õ����ַ���ʹ��ʮ���������͸�ʽ����ʽ����ַ���Сд����ĸ�����ʹ�ô�д��X�����ʽ����ַ��Ǵ�д�ַ� 
            pwd = pwd + s[i].ToString("X");
        }
        return pwd;
    }

    #endregion
}
