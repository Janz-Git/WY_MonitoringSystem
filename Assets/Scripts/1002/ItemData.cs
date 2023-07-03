using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{

    public Data1002 Data1002_;
    [Header("报警序号")]
    public Text _alarminfoid;
    [Header("通道ID")]
    public Text _channelid;

    [Header("分区ID（编号）")]
    public Text _zoneid;

    [Header("报警时间")]
    public Text _alarmdatetime;

    [Header("报警光纤位置")]
    public Text _fiberposition;

    [Header("报警类型")]
    public Text _alarmtype;

    [Header("最大振幅")]
    public Text _maxamptitude;

    [Header("报警最新时间")]
    public Text _alarmendtime;

    [Header("报警级别")]
    public Text _alarmlevel;

    [Header("报警状态")]
    public Text _alarmstatus;

    [Header("振动次数")]
    public Text _vcount;

    [Header("处理状态")]
    public Text _dealingstatus;

    [Header("分区名称")]
    public Text _zonename;


    public Sprite SelectEffect;
    public Sprite NoSelectEffect;
    void OnEnable()
    {

        //print("******************" + Data1002_._channelid);

    
    }

    void Awake()
    {

        if (transform.Find("处理状态").GetComponent<Button>())
        {
            Button _dealingstatusBtn = transform.Find("处理状态").GetComponent<Button>();

            _dealingstatusBtn.onClick.AddListener(ClickHandleBtn);

        }

    }
    // Start is called before the first frame update
    void Start()
    {

       
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }


    public  void UpdateInfo()
    {

        _alarminfoid.text = Data1002_._alarminfoid;
        _zoneid.text = Data1002_._zoneid;
        _alarmdatetime.text = Data1002_._alarmdatetime;
        _fiberposition.text = Data1002_._fiberposition;
        _alarmtype.text = Data1002_._alarmtype;
        _maxamptitude.text = Data1002_._maxamptitude;
        _alarmendtime.text = Data1002_._alarmendtime;
        _alarmlevel.text = Data1002_._alarmlevel;
        _alarmlevel.text = Data1002_._alarmlevel;
        _alarmstatus.text = Data1002_._alarmstatus;
        _channelid.text = Data1002_._channelid;
        _vcount.text = Data1002_._vcount;
        _dealingstatus.text = Data1002_._dealingstatus;
        _zonename .text = Data1002_._zonename;
       



    }


    void ClickHandleBtn()
    {
        Handle.Instance.HanleObj.SetActive(true);

        Handle.Instance._alarminfoid = _alarminfoid.text;

       
        Handle.Instance.CurrentProcessingInfo = transform;

        print(transform.name + "-----" + Handle.Instance.CurrentProcessingInfo);
    
    }

}
