using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AppConfig
{
    public Dictionary<string, Dictionary<string, string>> dicConfigList = new Dictionary<string, Dictionary<string, string>>();
    public string configFileSourcePath;
    public string configFilePath;

    private static AppConfig _instance;

    public static string FilePath{
        get { return Application.streamingAssetsPath; }
    }

    private AppConfig()
    {
        configFileSourcePath = Application.streamingAssetsPath;
#if !UNITY_EDITOR && !UNITY_STANDALONE
        configFilePath = Application.persistentDataPath;
#else
        configFilePath = configFileSourcePath;
#endif
    }
    public static AppConfig Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AppConfig();
            return _instance;
        }
    }

    public Dictionary<string,string> DefaultConfig
    {
        get { return Instance.dicConfigList[ConfigConstant.ConfigFile]; }
    }

    /// <summary>
    /// ��ָ�����Ƶ������ļ��л�ȡһ�����ã�����Ҳ��������򷵻�null
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetConfigByFileName(string fileName, string key)
    {
        return GetConfigByFileName(fileName, key, null);
    }

    /// <summary>
    /// ��ָ�����Ƶ������ļ��л�ȡһ�����ã�����Ҳ��������򷵻�defaultValue
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetConfigByFileName(string fileName, string key, string defaultValue)
    {
        string value = defaultValue;

        if (dicConfigList.ContainsKey(fileName))
        {
            Dictionary<string, string> dic = dicConfigList[fileName];

            if (dic.ContainsKey(key))
            {
                value = dic[key];
            }
        }

        return value;
    }

    public Vector3 GetVector3ConfigByFileName(string fileName, string key, Vector3 defaultValue)
    {
        string value = GetConfigByFileName(fileName, key, null);
        if (value == null)
            return defaultValue;

        string[] p = value.Split(',');
        if (p.Length != 3)
        {
            return defaultValue;
        }

        Vector3 rs = new Vector3();
        for (int i = 0; i < p.Length; i++)
        {
            float f;
            if (!float.TryParse(p[i], out f))
            {
                return defaultValue;
            }
            rs[i] = f;
        }

        return rs;
    }

    /// <summary>
    /// ����һ�������ļ� 
    /// </summary>
    /// <param name="FileName"></param>
    public void LoadConfig(string FileName)
    {
        string sourceFilePath = configFileSourcePath + "/" + FileName;
        string filePath = configFilePath + "/" + FileName;

        bool found = false;
        if (!File.Exists(filePath))
        {
            // �������Ŀ¼��û���ļ�����ȥ���һ��ԴĿ¼����������ƹ��� 
            if (configFilePath != configFileSourcePath)
            {
                if (File.Exists(sourceFilePath))
                {
                    File.Copy(sourceFilePath, filePath);
                    found = true;
                }
            }
        }
        else
        {
            found = true;
        }

        if (!found)
        {
            Debug.LogWarning("Can not found config file [" + FileName + "]");
            return;
        }

        //Debug.Log("Config path: " + filePath);

        Dictionary<string, string> Str_Dic = AnalyseConfigFile(filePath);
        if (Str_Dic == null)
            return;

        if (dicConfigList.ContainsKey(FileName))
        {
            dicConfigList.Remove(FileName);
        }

        dicConfigList.Add(FileName, Str_Dic);

    }

    /// <summary>
    /// ����һ�������ļ�������
    /// </summary>
    /// <param name="str">һ������</param>
    /// <param name="key">�����key���������ʧ����Ϊnull</param>
    /// <param name="value">�����value���������ʧ����Ϊnull</param>
    public static void AnalyseConfigString(string str, out string key, out string value)
    {
        key = null;
        value = null;

        if (str == null)
            return;

        str = str.Trim();

        // �ų����� 
        if (str == "")
            return;

        // �ų�ע���� 
        if (str.Substring(0, 1) == "#")
            return;

        //Debug.Log("Config str: " + str);

        // �õȺŲ��
        int index = str.IndexOf("=");
        if (index < 0)
            return;

        key = str.Substring(0, index).Trim();
        if (key == "")
            return;

        value = str.Substring(index + 1).Trim();
    }

    /// <summary>
    /// ��ָ�����ļ�·������ȡ������һ�������ļ�
    /// </summary>
    /// <param name="path">ָ�����ļ�·��</param>
    /// <returns></returns>
    public static Dictionary<string, string> AnalyseConfigFile(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        Dictionary<string, string> rs = new Dictionary<string, string>();
        string[] strs = File.ReadAllLines(path);

        for (int i = 0; i < strs.Length; i++)
        {
            string str = strs[i];

            string key, value;
            AnalyseConfigString(str, out key, out value);


            if (key != null && value != null)
            {
                //Debug.Log("Load Config [" + key + "]=" + value);
                rs.Add(key, value);
            }
            else
            {
                //Debug.Log("Config [" + key + "] not exist");
            }
        }

        return rs;
    }

    /// <summary>
    /// �������ļ�д��ָ�����ļ���
    /// </summary>
    /// <param name="path">ָ�����ļ�·��</param>
    /// <param name="data">���ã�key-value��</param>
    public static void SaveConfigFile(string path, Dictionary<string, string> data)
    {
        int index = path.LastIndexOf("/");
        if (index >= 0)
        {
            string dir = path.Substring(0, index + 1);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        string content = "";
        foreach (string key in data.Keys)
        {
            string value = data[key];
            content += key + " = " + value + "\n";
        }

        try
        {
            File.WriteAllText(path, content);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save File Exception! " + e);
        }
    }
}
