using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField]
    RectTransform targetObject;
    RectTransform rectTransform;
    [SerializeField]
    int speed;
    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.rotation = Quaternion.Euler(new Vector3(rectTransform.rotation.x, rectTransform.rotation.eulerAngles.y + Time.deltaTime * speed, rectTransform.rotation.z));
        targetObject.rotation = Quaternion.Euler(Vector3.zero);
    }
}
