using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;


public class ParameterSettings : MonoBehaviour
{

    

    public GameObject[] P_Setting_Pages;//参数设置页面和光功率调整

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_D_P_Setting(string OpenName)
    {
        bool IsTrue;
        if (OpenName != "DVS光功率调整")
        {
            foreach (GameObject p in P_Setting_Pages)
            {
                IsTrue = p.name.Equals(OpenName) ? true : false;


                p.SetActive(IsTrue);

            }
        }
        else
        {
            P_Setting_Pages[2].SetActive(true);
        }
    
    }



}
