using DotNet.FrameWork.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class OTDRManager : MonoBehaviour
{
    /// <summary>
    /// 访问OTDR数据库延时时间
    /// </summary>
    private int OTDRDelay = 120;

    /// <summary>
    /// 报警阈值，超过该值则报警
    /// </summary>
    public static float OTDRAlarm = 1000.00f;

    /// <summary>
    /// 记录上一次读取的数据表名称，防止重复获取数据
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
    /// 开始请求OTDR数据
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
            //每5秒发送一次测试数据
            if (t > delay)
            {
                string sqlGetTable = "select table_name from information_schema.tables where table_schema = 'otdruser' order by table_name desc limit 0,1;";
                //使用DB2，使用DB2，使用DB2
                DataTable dtbl = DbHelperMySQL2.ExecuteDataTable(sqlGetTable);

                if (dtbl.Rows.Count > 0)
                {
                    string tableName = dtbl.Rows[0][0].ToString();
                    if (tableName == lastTableName)//当前获取的表名为上一次的表，没有新增表，短暂延迟（tempDelay）后重新读取
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
