using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholController : MonoBehaviour {

    [SerializeField]
    private GameObject straw;
    [SerializeField]
    private GameObject drinkingStraw;
	// Use this for initialization
	void Start () {
		
	}
	
    public void OnClicking()
    {
        //change alcohol image to with straw
        DrinkAlcoholAnimation();
    }

    public void DrinkAlcoholAnimation()
    {
        CoroutineUtility.GetInstance().Do()
        .Then(() =>
        {
            straw.SetActive(false);
            drinkingStraw.SetActive(true);
            Guy.mouthType = GuyMouthType.MOUTH_DRINK;
        }).Wait(0.8f).Then(() =>
        {
            Guy.mouthType = GuyMouthType.MOUTH_NORMAL;
            straw.SetActive(true);
            drinkingStraw.SetActive(false);
            EatSystemController.instance.OnDrinkingAlcohol();
        }).Go();
    }
}
