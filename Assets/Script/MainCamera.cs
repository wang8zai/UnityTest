using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameObject MainHero;
    // Use this for initialization
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - MainHero.transform.position;
    }

    // Update is called once per frame
    void Update () {
        transform.position = MainHero.transform.position + offset;
	}

    public void SetMainHero(GameObject GObj) {
        MainHero = GObj;
    }
}
