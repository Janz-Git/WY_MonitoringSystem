using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Fold : MonoBehaviour
{

    public Button LeftFold;//左折叠按钮
    public Button RightFold;//右折叠按钮
    public bool IsLeftFold;//左侧是否收起
    public bool IsRightFold;//右侧是否收起
    public Transform LeftFoldTransform;//左折叠面板
    public Transform RightFoldTransform;//右折叠面板

    public Transform LateralControl;//布局控制的父物体
    // Start is called before the first frame update
    void Start()
    {
        LeftFold.onClick.AddListener(ClickLeftFold);
        RightFold.onClick.AddListener(ClickRightFold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 点击左折叠按钮
    /// </summary>
    void ClickLeftFold()
    {
        LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = false;
        if (IsLeftFold)
        {


            IsLeftFold = false;
            LeftFoldTransform.DOMoveX(-90, 0.1f).OnComplete(delegate
            {

                LeftFold.transform.DORotate(new Vector3(0, 0, 180), 0);
                LeftFoldTransform.gameObject.SetActive(false);
                LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = true;
            
            
            } );
            
        }
        else
        { 
            IsLeftFold = true;
            LeftFoldTransform.gameObject.SetActive(true);
             LeftFold.transform.DORotate(Vector3.zero, 0);
            LeftFoldTransform.DOMoveX(0, 0.1f).OnComplete(delegate
           {
              
               LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = true;


           });


        }


    }
  public      void ClickRightFold()
    {
        LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = false;
        if (IsRightFold)
        {


            IsRightFold = false;
            RightFoldTransform.DOMoveX(2287, 0.1f).OnComplete(delegate
            {

               
                 RightFold.transform.DORotate(new Vector3(0, 0, 180), 0);
                RightFoldTransform.gameObject.SetActive(false);
                LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = true;


            });

        }
        else
        {
            IsRightFold = true;
            RightFoldTransform.gameObject.SetActive(true);
            RightFold.transform.DORotate(Vector3.zero, 0);
           // RightFold.transform.DORotate(new Vector3(0, 0, 180), 0);
            RightFoldTransform.DOMoveX(1920, 0.1f).OnComplete(delegate
           {

               LateralControl.GetComponent<HorizontalLayoutGroup>().enabled = true;


           });


        }


    }
}
