using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GuyMouthType
{
	MOUTH_NORMAL,
	MOUTH_OPEN,
	MOUTH_CLOSE,
    MOUTH_PUKE,
    MOUTH_DAGER,
    MOUTH_DRINK
}

public enum GuyHairType
{
    HAIR_NORMAL,
    HAIR_RANBOW,
}

public enum GuyGlassesType
{
    GLASSES_NORMAL,
    GLASSES_BROKEN,
}

public enum GuyType
{
    GUY_NORMAL,
    GUY_RANBOW,
}

public class Guy : MonoBehaviour {

	[Header("嘴巴")]
	[SerializeField]
	Sprite[] mouthSprite;

	[SerializeField]
	Image Mouth;

    [Header("頭髮")]
    [SerializeField]
    Sprite[] hairSprite;

    [SerializeField]
    Image Hair;

    [Header("眼鏡")]
    [SerializeField]
    Sprite[] glassesSprite;

    [SerializeField]
    Image Glasses;

    [Header("本體")]
    [SerializeField]
    GameObject RanbowGuy, NormalGuy;
    static GuyType _guyType;
    public static GuyType guyType
    {
        get { return _guyType; }
        set
        {
            _guyType = value;
			switch(value){
				case GuyType.GUY_NORMAL:
					instance.NormalGuy.SetActive(true);
                    instance.RanbowGuy.SetActive(false);
					break;
                case GuyType.GUY_RANBOW:
                    instance.NormalGuy.SetActive(false);
                    instance.RanbowGuy.SetActive(true);
                    break;
			}
        }
    }

    static GuyMouthType _mouthType;
	public static GuyMouthType mouthType
	{
		get { return _mouthType; }
		set {
			_mouthType = value;

            instance.Mouth.sprite = instance.mouthSprite[(int)value];
		}
	}

    static GuyHairType _hairType;
    public static GuyHairType hairType
    {
        get { return _hairType; }
        set
        {
            _hairType = value;

            instance.Hair.sprite = instance.hairSprite[(int)value];
        }
    }

    static GuyGlassesType _glassesType;
    public static GuyGlassesType glassesType
    {
        get { return _glassesType; }
        set
        {
            _glassesType = value;

            instance.Glasses.sprite = instance.glassesSprite[(int)value];
        }
    }

	static Guy instance;

	void Awake()
	{
		if (instance != null && instance != this){
			Destroy(gameObject);
			return;
		}
        instance = this;
		guyType = GuyType.GUY_NORMAL;
		mouthType = GuyMouthType.MOUTH_NORMAL;
        hairType = GuyHairType.HAIR_NORMAL;
        glassesType = GuyGlassesType.GLASSES_NORMAL;
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Z))
        {
            guyType = GuyType.GUY_NORMAL;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            guyType = GuyType.GUY_RANBOW;
        }
		if (Input.GetKeyDown(KeyCode.A)){
			mouthType = GuyMouthType.MOUTH_CLOSE;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mouthType = GuyMouthType.MOUTH_DAGER;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mouthType = GuyMouthType.MOUTH_NORMAL;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            mouthType = GuyMouthType.MOUTH_OPEN;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mouthType = GuyMouthType.MOUTH_PUKE;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
			hairType = GuyHairType.HAIR_NORMAL;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            hairType = GuyHairType.HAIR_RANBOW;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            glassesType = GuyGlassesType.GLASSES_NORMAL;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            glassesType = GuyGlassesType.GLASSES_BROKEN;
        }
	}
}
