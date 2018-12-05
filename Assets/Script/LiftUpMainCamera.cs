using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUpMainCamera : MonoBehaviour {

	private GameObject MainHero;
	// Use this for initialization
	void Start () {
		MainHero = GameObject.Find("MainHeroSprite");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(MainHero.transform.position.x, MainHero.transform.position.y, transform.position.z);
	}
}
