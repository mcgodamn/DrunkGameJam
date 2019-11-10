using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class PropController : MonoBehaviour {

    public UnityEvent eatenEvent = new UnityEvent();
    public string id;
    [SerializeField]
    private Vector3 dropPosition;
    [SerializeField]
    private bool dragable;
    [SerializeField]
    private bool evolutionable;
    [SerializeField]
    private bool edible;
    [SerializeField]
    private bool haveInitiateAnimation;

    public bool isBelch;

    [SerializeField]
    private AudioSource vomitSoundEffect;
    [SerializeField]
    private AudioSource weedSoundEffect;
    private Image img;
    private Rect mouthRect;

    private RectTransform rectTransform;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        mouthRect = new Rect(-95, -85, 200, 200);
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDragging()
    {
        if (!dragable)
            return;
        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //print(new Vector2(mousePosition.x * 1920f - 960f, mousePosition.y * 1080 - 540f));
        //mousePosition.z = 0;
        rectTransform.anchoredPosition = new Vector2(mousePosition.x * 1920f - 960f, mousePosition.y * 1080 - 540f);
    }

    private void Eaten()
    {
        eatenEvent.Invoke();

        if (evolutionable)
        {
            //call eatsystem singleton (str id,image img)
            EatSystemController.instance.Eat(id, img);
        }
    }
    private bool CheckIsInMouth()
    {
        //print(mouthRect);
        //print(rectTransform.rect);
        Rect r = new Rect(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.rect.width, rectTransform.rect.height);
        //print(r);
        return r.Overlaps(mouthRect);
    }

    public void OnEndDragging()
    {
        if (!dragable)
            return;
        if (CheckIsInMouth() && edible)
        {
            //print("eat");
            InstantiateAnimation();
            //Eaten();
        }

        //reset position
        ResetPosition();
    }
    private void ResetPosition()
    {
        rectTransform.anchoredPosition = dropPosition;
    }

    private void InstantiateAnimation()
    {
        /*if(weedSoundEffect.clip!=null)//&& isWeed)
        {
            weedSoundEffect.Play();
        }
        else
        {
            vomitSoundEffect.Play();
        }*/
        CoroutineUtility.GetInstance().Do().MoveUI(gameObject, new Vector2(526, 752), 0.8f)
            .MoveUI(gameObject, new Vector2(-565, 825), 0.2f)
            
            .MoveUI(gameObject, dropPosition, 0.5f).Then(() =>
            {
                //On Initialized
            }).Go();
        //rectTransform.anchoredPosition = dropPosition;
    }
}
