using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public   class PromptStatus : MonoBehaviour
{

    public static PromptStatus Instance;
    public Sprite Defult;
    public Sprite Erro;
    public Sprite Run;

    public  Image Run_;
    public  Image Erro_;
    public  Image TCP_;

    public void Awake()
    {
        Instance = this;
        Run_ = transform.Find("运行").GetComponent<Image>();
        Erro_ = transform.Find("错误").GetComponent<Image>();
        TCP_ = transform.Find("TCP").GetComponent<Image>();
    
    
    }
    // Start is called before the first frame update
    void Start()
    {
        Run_.sprite = Defult;
        Erro_.sprite = Defult;
        TCP_.sprite = Defult;

       // print(Sprite_("运行"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void UpdateStatus(string Name,string status)
    {

        switch (Name)
        {
            case "运行":
                Run_.sprite = Sprite_(status);
               
                break;
            case "错误":
                Erro_.sprite = Sprite_(status);

                break;
            case "TCP":
               
                TCP_.sprite = Sprite_(status);
                break;
            default:
                break;

        }


    }


    public  Sprite Sprite_(string name)
    {
        

        switch (name)
        {
            case "运行":
                return Run;
                
               
            case "错误":
               return Erro;
                
            case "不运行":
                return Defult;
                

            default:
                break;
        }

        return Sprite_(name);



    }

}
