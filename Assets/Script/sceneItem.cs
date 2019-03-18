using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneItem : MonoBehaviour {
	private GameManager gameManager;
	public int duration;
	public void SetGameManager(GameManager gm) {
		gameManager = gm;
	} 
	public void minusDuration(int i) {
		duration = duration - i;
		if(duration <= 0) {
			Destroy(gameObject);
		}
	}
}
