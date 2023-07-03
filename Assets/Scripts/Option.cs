using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public static string EnterPage;
    
    // Start is called before the first frame update
    void Start()
    {
        EnterPage = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOption(string EnterPageName)
    {

        //if (EnterPageName!="")
        //{ 
        // EnterPage = EnterPageName;
        
        //}

       // print(EnterPage);

        SceneManager.LoadScene(EnterPageName);


    }


}
