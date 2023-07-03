using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPage : MonoBehaviour
{
    public Transform[] LeftIcon;


    public GameObject[] OpenPage; 
    // Start is called before the first frame update
    void Start()
    {

        if (Option.EnterPage=="")
        {
            Option.EnterPage = "主界面";
        }

        ClickIcon(Option.EnterPage);



    }

    // Update is called once per frame
    void Update()
    {

        


    }

    public void ClickIcon(string ChoosePage)
    {
        bool isTrue;
        foreach (Transform icon in LeftIcon)
        {
            isTrue = icon.name.Equals(ChoosePage)?true:false;
            icon.Find("选中").gameObject.SetActive(isTrue);         

        }


        foreach (GameObject Page in OpenPage)
        {

            isTrue = Page.name.Equals(ChoosePage)?true:false;
            Page.gameObject.SetActive(isTrue);
            
        }

    }


}
