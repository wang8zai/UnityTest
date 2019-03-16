using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : ScriptableObject {

	public List<GameObject> list = new List<GameObject>();
	public GameObject CameraPrefab;

	public void Awake() {
		Init();
	}

	public void Init() {
		Debug.Log("camera Init");
		GameObject cameraPrefab = Resources.Load<GameObject>("Prefab/camera");
		GameObject cameraClone = Instantiate(cameraPrefab, new Vector3(0,0,0), Quaternion.identity);
		cameraClone.SetActive(true);
		list.Add(cameraClone);
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
