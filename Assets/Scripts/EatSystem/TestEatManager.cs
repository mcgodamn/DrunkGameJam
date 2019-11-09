using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEatManager : MonoBehaviour {
    public string a;
    public string b;
    public string c;
    private EatSystemController eat;
	// Use this for initialization
	void Start () {
        eat = GetComponent<EatSystemController>();
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.KeypadEnter)) 
        {
            //eat.Eat(a);
            //eat.Eat(b);
            //eat.Eat(c);
            print(eat.Evolution());
        }
	}
}
