using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Button LeftInout;
    public RectTransform[] Tips;   
    public  RectTransform LeftPage;
    public bool IsClick;
    
    // Start is called before the first frame update
    void Start()
    {
        IsClick = true;
        print(LeftPage.rect.size);
       // LeftInout.onClick.AddListener(OnClickLeftInOut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnClickLeftInOut()
    {
        if (IsClick)
        {
            IsClick = false;
            LeftPage.gameObject.SetActive(false);
            foreach (RectTransform t in Tips)
            {
                t.sizeDelta = new Vector2(t.sizeDelta.x + (80 / 12), 0);
                print(t.sizeDelta.x);

            }
        }
        else
        {
            IsClick=true;
            LeftPage.gameObject.SetActive(true);
            foreach (RectTransform t in Tips)
            {
                t.sizeDelta = new Vector2(t.sizeDelta.x - (80 / 12), 0);
                print(t.sizeDelta.x);

            }
        }
       



    }
}
