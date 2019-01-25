using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public List<GameObject> list = new List<GameObject>();
	public GameObject CameraPrefab;
	private static CameraManager instance = null;

    public static CameraManager Instance {
        get {
            return instance;
        }
    }

	public void Awake() {
		if(instance == null) {
            instance = this;
			instance.Init();
        }
		else if(instance != this) {
            Destroy(this);
        }

	}

	public void Init() {
		// GameObject camera = Instantiate<GameObject>(CameraPrefab);
		// list.Add(camera);
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
