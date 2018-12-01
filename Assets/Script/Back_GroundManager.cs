using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_GroundManager : MonoBehaviour {
	public static List<GameObject> BGInstance;
	public GameObject CameraPrefab;		// need to update to assign into this value
	public GameObject BGPrefab;
	
	public void Get() {
	}

	public void InitBG() {
		if(BGInstance == null) {
			BGInstance = new List<GameObject>();
		}
		BGInstance.Add(Instantiate<GameObject>(BGPrefab));

//		BGInstance[0].Init();
//		BGInstance[0].Get().GetComponent<Camera>
		BGInstance[0].SetActive(true);
		print(BGInstance.Count);
	}

	public void SetCamera(GameObject CameraObj) {
		CameraPrefab = CameraObj;
		print(BGInstance.Count);
		for(int i=0; i < BGInstance.Count; i++) {
			BGInstance[i].GetComponent<Back_Ground>().SetCamera(CameraPrefab);
		}
	}

	public void SetMainHero(GameObject obj) {
		for(int i=0; i < BGInstance.Count; i++) {
			BGInstance[i].GetComponent<Back_Ground>().Body = obj;
		}
	}
}
