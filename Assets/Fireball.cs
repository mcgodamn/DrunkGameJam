using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    public GameObject burn;
	// Use this for initialization
	void Start () {
        CoroutineUtility.GetInstance().Do().Wait(0.8f).Then(() =>
        {
            Instantiate(burn);
        }).Go();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
