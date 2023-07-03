using DotNet.FrameWork.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OTDRManager : MonoBehaviour
{
    /// <summary>
    /// ����OTDR���ݿ���ʱʱ��
    /// </summary>
    private int OTDRDelay = 120;

    /// <summary>
    /// ������ֵ��������ֵ�򱨾�
    /// </summary>
    public static float OTDRAlarm = 1000.00f;

    /// <summary>
    /// ��¼��һ�ζ�ȡ�����ݱ����ƣ���ֹ�ظ���ȡ����
    /// </summary>
    private static string lastTableName = "";


    public delegate void DataObtained(List<DiBo.Model.OTDRUser> lst);
    public event DataObtained OnDataObtained;


    // Start is called before the first frame update
    void Start()
    {
        AppConfig config = AppConfig.Instance;
        OTDRDelay = int.Parse(config.DefaultConfig["OTDRDelay"]);
        OTDRAlarm = float.Parse(config.DefaultConfig["OTDRAlarm"]);

        StartRequest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ��ʼ����OTDR����
    /// </summary>
    public void StartRequest()
    {
        StopCoroutine(RequestData());
        StartCoroutine(RequestData());
    }

    IEnumerator RequestData()
    {
        float tempDelay = 10.0f, delay = OTDRDelay;
        float t = 0;

        while (true)
        {
            t += Time.deltaTime;
            //ÿ5�뷢��һ�β�������
            if (t > delay)
            {
                string sqlGetTable = "select table_name from information_schema.tables where table_schema = 'otdruser' order by table_name desc limit 0,1;";
                //ʹ��DB2��ʹ��DB2��ʹ��DB2
                DataTable dtbl = DbHelperMySQL2.ExecuteDataTable(sqlGetTable);

                if (dtbl.Rows.Count > 0)
                {
                    string tableName = dtbl.Rows[0][0].ToString();
                    if (tableName == lastTableName)//��ǰ��ȡ�ı���Ϊ��һ�εı�û�������������ӳ٣�tempDelay�������¶�ȡ
                    {
                        delay = tempDelay;
                    }
                    else
                    {
                        List<DiBo.Model.OTDRUser> models = DiBo.DAL.OTDRUser.GetModel(tableName, "", "Curtime desc");

                        if (OnDataObtained != null)
                            OnDataObtained.Invoke(models);

                        lastTableName = tableName;
                        delay = OTDRDelay;
                    }
                }
                t = 0;
            }
            yield return null;
        }
    }

}
