
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using DotNet.FrameWork.Data;

public class LoginSystem : MonoBehaviour
{

    public GameObject Login_Obj;
    public GameObject Register_Obj;
    public InputField UserName;
    public InputField Password;
    public Button Loginbtn;
    public Button RigisterBtn;


    public InputField RegisterUserName;
    public InputField RegisterPassword;
    public InputField RegisterPhoneNum;

    public GameObject ErroTip;
    public Text ErroText;

    string Result;

    void Awake()
    {
        AppConfig config = AppConfig.Instance;
        config.LoadConfig(ConfigConstant.ConfigFile);

        DbHelperMySQL.connectionString = config.DefaultConfig["DBLinkMySQL"];
        //OTDR数据库链接字符串，处理纵向沉降和形变数据
        DbHelperMySQL2.connectionString = config.DefaultConfig["DBLinkMySQL2"];

    }
    // Start is called before the first frame update
    void Start()
    {
        Loginbtn.onClick.AddListener(ClickLoginbtn);


    }

    // Update is called once per frame
    void Update()
    {


       
    }

    void ClickLoginbtn()
    {
        if (UserName.text!=""&& Password.text!="")
        {
            print("进入");
            if (UserManager.Login(UserName.text, Password.text))
            {
                print("登陆成功");
                SceneManager.LoadScene("模块选择");
            }
            else
            {
                UserName.text = "";
                Password.text = "";
                ErroTipShow("用户名或者密码错误/未注册");
                print("登陆失败");
            }
        }
        else
        {
             print("用户名或密码不为空");
            ErroTipShow("用户名或密码不为空");
        }
        

        

    }

    public void ClickRegister()
    {
        if (RegisterUserName.text!=""&&RegisterPassword.text!=""&&RegisterPhoneNum.text!="")
        {

            if (UserManager.Register(out Result ,RegisterUserName.text,RegisterPassword.text,RegisterPhoneNum.text))
            {
                ErroTipShow("注册成功");
                RegisterUserName.text = ""; RegisterPassword.text = ""; RegisterPhoneNum.text = "";
                ClickRegisterBtn(false);
            }
            else
            {
                 ErroTipShow("注册信息有误请核对后重试");
                 RegisterUserName.text = ""; RegisterPassword.text = ""; RegisterPhoneNum.text = "";
                 // ClickRegisterBtn(false);
            }

        }
        else
        {
            ErroTipShow("注册信息不能为空");
        }
    
    
    
    }

   public  void ClickRegisterBtn(bool Open)
    { 
     RegisterUserName.text = ""; RegisterPassword.text = ""; RegisterPhoneNum.text = "";
    Login_Obj.SetActive(!Open);
    Register_Obj.SetActive(Open);
    
    }
    public void ErroTipShow(string Tip)
    {
        ErroText.text = Tip;
        ErroTip.SetActive(true);
        TimeManager.Instance.AddInterval(delegate { ErroTip.SetActive(false); }, 1.0f);

    }
}
