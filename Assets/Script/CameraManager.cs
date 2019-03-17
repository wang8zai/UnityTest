using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : ScriptableObject {

	private GameManager gameManager = null;
	public List<GameObject> list = new List<GameObject>();
	public GameObject CameraPrefab;

	public void Awake() {
		// Init();
	}

	public void Init(GameManager gm) {
		Debug.Log("camera Init");
		gameManager = gm;
		GameObject cameraPrefab = Resources.Load<GameObject>("Prefab/Camera/MainCamera");
		GameObject cameraClone = Instantiate(cameraPrefab, new Vector3(0, 7,-10), Quaternion.identity);
		//GameObject cameraClone = Instantiate(cameraPrefab);
		cameraClone.SetActive(true);
		list.Add(cameraClone);
	}

	public GameObject Get(int index) {
		return list[index];
	}

	public void SetCharacter(int cameraIndex, int characterIndex) {
		list[cameraIndex].GetComponent<MainCamera>().SetCharacter(gameManager.characterManager.Get(characterIndex));
	}

	public void SetScriptActive() {
		list[0].SetActive(true);
	}
}
