using DiBo.DAL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    public static Handle Instance;
    public GameObject HanleObj;
    public Transform CurrentProcessingInfo;
    public string _alarminfoid;

    public InputField ProcessedBy;
    public InputField ProcessDescription;

    public  Button Sure;
    public Button Cancel;

    public void Awake()
    { 
    Instance = this;
    
    }
    // Start is called before the first frame update
    void Start()
    {

        Sure.onClick.AddListener(ClickSure);
        Cancel.onClick.AddListener(ClickCencle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ClickSure()
    {
        if (ProcessedBy.text!=""&&ProcessDescription.text!="")
        {
            foreach (Transform item in CurrentProcessingInfo)
            {
                if (item.Find("Text") != null)
                {
                    item.Find("Text").GetComponent<Text>().color = Color.white;
                }
                if (item.name.Equals("处理状态"))
                {
                    item.Find("Text").GetComponent<Text>().text = "已处理";
                }

            }
            DVSAlarmInfoReport.UpdateDealingStatus(int.Parse(_alarminfoid), ProcessedBy.text, ProcessDescription.text);
            HanleObj.SetActive(false);
            ProcessedBy.text = "";
            ProcessDescription.text = "";

        }
        else
        {
            print("处理人/处理描述不能为空");
        }
        
        
    
    
    }

    void ClickCencle()
    {

        HanleObj.SetActive(false);
        ProcessedBy.text = "";
        ProcessDescription.text = "";

    }
}
