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

		mouthType = GuyMouthType.MOUTH_NORMAL;
        hairType = GuyHairType.HAIR_NORMAL;
        glassesType = GuyGlassesType.GLASSES_NORMAL;
	}
}
