using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class PropController : MonoBehaviour
{

    public UnityEvent animationDoneEvent = new UnityEvent();
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
    void Awake()
    {
        img = GetComponent<Image>();
        mouthRect = new Rect(0, 0, 200, 270);
        rectTransform = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDragging()
    {
        if (!dragable)
            return;
        EatSystemController.instance.OnPropDragging(this);
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
        EatSystemController.instance.OnPropEndDragging(this);
        if (CheckIsInMouth() && edible)
        {
            Guy.mouthType = GuyMouthType.MOUTH_CLOSE;
            CoroutineUtility.GetInstance().Do().Wait(0.5f).Then(() =>
            {
                Guy.mouthType = GuyMouthType.MOUTH_NORMAL;
            }).Go();
            
            Eaten();
        }

        //reset position
        ResetPosition();
    }
    private void ResetPosition()
    {
        rectTransform.anchoredPosition = dropPosition;
    }

    public void InstantiateAnimation()
    {
        
        if ( weedSoundEffect!=null && weedSoundEffect.clip!=null)//&& isWeed)
        {
            weedSoundEffect.Play();
        }
        else
        {
            if(vomitSoundEffect!=null)
            vomitSoundEffect.Play();
        }

        if (!haveInitiateAnimation)
        {
            return;
        }
        rectTransform.anchoredPosition = Vector2.zero;
        Vector2 outSide = new Vector2(Random.Range(-400,400),800);
        CoroutineUtility.GetInstance().Do().MoveUI(gameObject, outSide, 0.8f)
            .MoveUI(gameObject, new Vector2(dropPosition.x,1000), 0.2f)

            .MoveUI(gameObject, dropPosition, 0.5f).Then(() =>
            {
                //On Initialized
                animationDoneEvent.Invoke();
            }).Go();
        
    }
}
