
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using LitJson;
using System.Data;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}
public class LocalDialog
{
    //链接指定系统函数       打开文件对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOFN([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }

    //链接指定系统函数        另存为对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
    public static bool GetSFN([In, Out] OpenFileName ofn)
    {
        return GetSaveFileName(ofn);
    }
}
[System.Serializable]
public class PageManagement : MonoBehaviour
{

    public InitializeAndUpdateData InitializeAndUpdateData_;
    // Start is called before the first frame update


    public Button PreviousPageBtn;
    public Button NextPageBtn;
    public Button FristPage;
    public Button LastPage;

    public InputField InputPageNum;
    public Button JumpPageBtn;

    public Transform PageNum;


    public static int WhichPage;

    public static string StartDate;
    public static string EndDate;
    public static string LastStartDate;
    public static string LastEndDate;
    public Transform ErroTip;


    public InputField InputStartDate_;
    public InputField InputEndDate_;
    public static int LastWhichPage;

    void OnEnable()
    {
        
     
      
    
    }
    public void Awake()
    {
        StartDate = "2023-06-06";
        EndDate = "2023-06-30";
      
    
    
    }
    void Start()
    {
     
        PreviousPageBtn.onClick.AddListener(()=>ClickPageTurning(false));

        NextPageBtn.onClick.AddListener(()=>ClickPageTurning(true));

        FristPage.onClick.AddListener(() => ClickPageNumbtn(1));
        LastPage.onClick.AddListener(() => ClickPageNumbtn(CalculatePages()));

        JumpPageBtn.onClick.AddListener(ClickJump);
      ClickPageTurning(true);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 获取 总长度
    /// </summary>
    /// <param name="InfoCount"></param>
    /// <returns></returns>
    public int CalculatePages()
    {
        //print(StartDate+"---------"+EndDate);
        int InfoCount = DiBo.DAL.DVSAlarmInfoReport.GetRecordCount(StartDate,EndDate);
  
        return Mathf.CeilToInt(InfoCount/15.0f);
    
    
    }

    /// <summary>
    /// 点击跳转到xxx页
    /// </summary>
    public void ClickJump()
    {
        if (InputPageNum.text!="")
        {

            if (int.Parse(InputPageNum.text)>CalculatePages())
            {
               
                ShowErroTip("已超出总页数，请重新输入");
            }
            else
            {
                
                ClickPageNumbtn(int.Parse(InputPageNum.text));
               
            }
        }
        else
        {
            ShowErroTip("输入的页码错误");
          
        }

        InputPageNum.text = "";
    }
  

    /// <summary>
    /// 点击下一页或上一页
    /// </summary>
    /// <param name="IsNext"></param>
    public void ClickPageTurning(bool IsNext)
    {
        if (IsNext)
        {
            int ToalLenth = +CalculatePages();
            
            if (WhichPage<ToalLenth)
            {                   
                WhichPage++;            
                UpdatePagesNum();
                InitializeAndUpdateData_.HistoryListUpdate(WhichPage);

            }
            else
            {
               
               StartDate = LastStartDate;
               EndDate = LastStartDate;
               WhichPage = LastWhichPage;
               ShowErroTip("该时间段内无数据");
            }
        }
        else
        {
           
            if (1<WhichPage)
            {
               
                WhichPage--;
                UpdatePagesNum();
                InitializeAndUpdateData_.HistoryListUpdate(WhichPage);

            }
        }
       

       
    
    }

   /// <summary>
   /// 计算两个日期相差多少天
   /// </summary>
   /// <param name="dateStr1"></param>
   /// <param name="dateStr2"></param>
   /// <returns></returns>
    public int GetSubDays(string dateStr1, string dateStr2)
    {

        DateTime startTimer = Convert.ToDateTime(dateStr1);

        DateTime endTimer = Convert.ToDateTime(dateStr2);
        TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

        TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

        TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

        //返回相差时长（仅返回相差的天数）
        //return subTimer.Days;
        //返回相差时长（返回相差的总天数）
        return (int)subTimer.TotalDays;
    }
    /// <summary>
    /// 更新数字页码
    /// </summary>
    public void UpdatePagesNum()
    {
       
       
        for (int i = 0; i < PageNum.childCount; i++)
        {
         
            if (WhichPage+i>CalculatePages())
            {
               
                PageNum.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
               
               
                PageNum.GetChild(i).gameObject.SetActive(true);
               

                PageNum.GetChild(i).Find("Text").GetComponent<Text>().text = (WhichPage + i).ToString();     
                PageNum.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();


            }

           


        }


        foreach (Transform item in PageNum)
        {

            int Num = int.Parse(item.Find("Text").GetComponent<Text>().text);
            if (Num == WhichPage)
            {
                item.Find("Text").GetComponent<Text>().color = Color.red;

            }
            else
            {
                item.Find("Text").GetComponent<Text>().color = Color.white;
            }
            item.GetComponent<Button>().onClick.AddListener(()=>ClickPageNumbtn(Num));
        }
    
    
    }
    /// <summary>
    /// 点击页码数字
    /// </summary>
    /// <param name="WhichNum"></param>
    void ClickPageNumbtn(int WhichNum)
    {     
        WhichPage = WhichNum;
        InitializeAndUpdateData_.HistoryListUpdate(WhichNum);
         UpdatePagesNum();

    }
    /// <summary>
    /// 错误提示
    /// </summary>
    /// <param name="ErroInfo"></param>
    public void ShowErroTip(string ErroInfo)
    { 
        ErroTip.gameObject.SetActive(true);
       ErroTip.Find("Text").GetComponent<Text>().text = ErroInfo;
        TimeManager.Instance.AddInterval(() => ErroTip.gameObject.SetActive(false), 1.5f);
    
    }

    /// <summary>
    /// 点击查询按钮或者导出查询结果
    /// </summary>
    public  void ClickQuery(bool IsExportExcel)
    {
       
        if (InputEndDate_.text!=""&&InputStartDate_.text!="")
        {

            if (GetSubDays(InputStartDate_.text, InputEndDate_.text)>=365)
            {
                ShowErroTip("日期选择区间不超过一年，请重新选择");
            }
            else
            {
                PageManagement.StartDate = InputStartDate_.text;
                PageManagement.EndDate = InputEndDate_.text;
                print(PageManagement.StartDate + "**********" + PageManagement.EndDate);
                WhichPage = 0;
                ClickPageTurning(true);
                if (IsExportExcel)
                {
                    CreatExcel();
                }

            }
            
        }
        else
        {
            ShowErroTip("日期选择错误，请继续选择");
           
        }
    
    }

    /// <summary>
    /// 创建Excel
    /// </summary>
    private void CreatExcel()
    {



        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "Excel文件(*.xlsx)\0*.xlsx";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "导出数据";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        if (LocalDialog.GetSaveFileName(openFileName))
        {
            string createPath = openFileName.file;
            FileInfo newFile = new FileInfo(createPath);



            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(createPath);
            }


            DataTable dataTable = new DataTable();

            dataTable = DiBo.DAL.DVSAlarmInfoReport.GetList(StartDate,EndDate, -1);

            JsonToExcel.DataTableToCsv(dataTable, createPath);




        }

    }

}
