using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public List<GameObject> list = new List<GameObject>();
	public GameObject CameraPrefab;

	public void Init() {
		GameObject camera = Instantiate<GameObject>(CameraPrefab);
		list.Add(camera);
	}

	public GameObject Get(int index) {
		return list[index];
	}

	public void SetMainHero(GameObject MainHero) {
		list[0].GetComponent<MainCamera>().SetMainHero(MainHero);
	}

	public void SetScriptActive() {
		list[0].SetActive(true);
	}
}
