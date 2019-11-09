using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    float count = 0;
    float lastTime = 0;
    RectTransform rectTransform;
    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        lastTime += Time.deltaTime;
        if (lastTime > 1)
        {
            count += 1;
        }
        rectTransform.anchoredPosition = new Vector2(Mathf.Cos(count), Mathf.Sin(count));
    }
}
