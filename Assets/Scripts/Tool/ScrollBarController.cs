using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{

    public Button UpBtn;
    public Button DownBtn;
    public Scrollbar Scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        UpBtn.onClick.AddListener(()=>OnClickUpAndDownBtn(true));
        DownBtn.onClick.AddListener(() => OnClickUpAndDownBtn(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickUpAndDownBtn(bool IsUp)
    {
        if (IsUp)
        {
            Scrollbar.value += 0.1f;

        }
        else
        {
            Scrollbar.value -= 0.1f;
        }
    
    }

    public void Test()
    {

        print(transform.GetComponent<Scrollbar>().value+"");


        print(GetOverUI(GameObject.Find("Canvas")));
    
    }
    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }
        return null;
    }
}
