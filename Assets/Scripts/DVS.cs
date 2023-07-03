using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVS : MonoBehaviour
{

    public GameObject[] Option;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ClickOptionBtn(string optionName)
    {
        bool isTrue;
        foreach (GameObject option in Option) 
        {

         isTrue = option.name.Equals(optionName)?true:false;

         option.SetActive(isTrue);

        }
    
    
    }
}
