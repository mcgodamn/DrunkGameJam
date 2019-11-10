using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour {
    float count = 0;
    RectTransform rectTransform;
    Vector2 defaultPosition;
    bool isDragged = false;
    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        defaultPosition = rectTransform.anchoredPosition;

	}
	
	// Update is called once per frame
	void Update () {
        if (!isDragged) {
            count += Time.deltaTime;
            rectTransform.anchoredPosition = new Vector2(defaultPosition.x + Mathf.Sin(count / 3) * 500, defaultPosition.y + Mathf.Sin(count * 3) * 60);
        }
	}

    public void OnDragging() {
        isDragged = true;
    }

    public void OnEndDrag() {
        isDragged = false;
    }
}
