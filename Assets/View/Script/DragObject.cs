using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

	RectTransform rectTransform;
	Vector2 originPos;

	float InputZ;
	// Use this for initialization
	void Start () {
        InputZ = -Camera.main.transform.position.z;
        rectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localpoint);
		Debug.Log(localpoint);
	}

	public void OnBeginDrag()
	{
		originPos = transform.position;
	}
	
	public void OnDrag()
    {
        var InputPos = Input.mousePosition;
        InputPos.z = InputZ;
        var pos = Camera.main.ScreenToWorldPoint(InputPos);
		Debug.Log(pos);
        rectTransform.position = pos;
	}

	public void OnEndDrag()
	{
        rectTransform.position = originPos;
	}
}
