using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternShake : MonoBehaviour {
	float randomNumber;
	void Awake() {
		randomNumber = Random.Range(-1.0f,1.0f);
	}

	void Update () {
		transform.rotation = Quaternion.Euler(0,0, 5 * Mathf.Sin(Time.time + randomNumber));
	}
}