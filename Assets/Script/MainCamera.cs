using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    private GameObject MainHero = null;
    // Use this for initialization
    private Vector3 offset = new Vector3(0, 0, 0);         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        if(MainHero != null) {
            offset = transform.position - MainHero.transform.position;
        }
    }

    // Update is called once per frame
    void Update () {
        if(MainHero != null) {
            transform.position = MainHero.transform.position + offset;
        }
        // this is a example to trigger an event.
        if(Input.GetKeyDown(KeyCode.Z)) {
            EventManager.Instance.TriggerEvent(Enums.Event.PlayerEventTest);
        }
	}

    public void SetCharacter(GameObject GObj) {
        MainHero = GObj;
    }
}
