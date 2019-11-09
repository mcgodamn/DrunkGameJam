using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingJitter : MonoBehaviour
{

    // Use this for initialization
    float count = 0;
    float horizonCount = 0;
    Vector2 defaultPosition;

    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime * 100;
        horizonCount += Time.deltaTime;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Mathf.Sin(count) * 15 + defaultPosition.y);
        // rectTransform.rotation = Quaternion.Euler(rectTransform.rotation.x, rectTransform.rotation.y, count);
    }
}
