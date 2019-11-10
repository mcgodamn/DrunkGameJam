using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholController : MonoBehaviour {

    [SerializeField]
    private GameObject straw;
    [SerializeField]
    private GameObject drinkingStraw;

    private AudioSource drinkSound;
	// Use this for initialization
	void Start () {
        drinkSound = GetComponent<AudioSource>();
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
            drinkSound.Play();
            straw.SetActive(false);
            drinkingStraw.SetActive(true);
            Guy.mouthType = GuyMouthType.MOUTH_DRINK;
        }).Wait(0.8f).Then(() =>
        {
            drinkSound.Stop();
            Guy.mouthType = GuyMouthType.MOUTH_NORMAL;
            straw.SetActive(true);
            drinkingStraw.SetActive(false);
            EatSystemController.instance.OnDrinkingAlcohol();
        }).Go();
    }
}
