using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.SocialPlatforms;

public class UI_FloatingEffect : MonoBehaviour
{

    float DelyTime;
    // Start is called before the first frame update
    void Start()
    {

        DelyTime = Random.Range(0, 10.0f);
        transform.DOLocalMoveY(transform.localPosition.y + 50.0f, 5.0f).SetLoops(-1,LoopType.Yoyo).SetDelay(DelyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
