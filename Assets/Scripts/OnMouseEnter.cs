using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OnMouseEnter : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
          transform.DOScale(Vector3.one, 0.1f);
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
