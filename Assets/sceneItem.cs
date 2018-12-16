using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneItem : MonoBehaviour {
	public int duration;
	public void minusDuration(int i) {
		duration = duration - i;
		if(duration <= 0) {
			Destroy(gameObject);
		}
	}
}
