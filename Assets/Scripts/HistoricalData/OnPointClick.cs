using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointClick : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
      

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("按下");

      //  InitializeAndUpdateData.IsUpdateHistoryData = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("抬起");
        //InitializeAndUpdateData.IsUpdateHistoryData = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
