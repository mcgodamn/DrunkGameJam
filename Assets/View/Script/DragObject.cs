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

	public void OnBeginDrag()
	{
		originPos = transform.position;
	}
	
	public void OnDrag()
    {
        var InputPos = Input.mousePosition;
        InputPos.z = InputZ;
        rectTransform.position = Camera.main.ScreenToWorldPoint(InputPos);
	}

	public void OnEndDrag()
	{
        rectTransform.position = originPos;
	}
}
