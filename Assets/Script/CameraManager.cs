using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : ScriptableObject {
    #region Singleton
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<CameraManager>();
            }
            return _instance;
        }
    }
    #endregion

    public List<GameObject> list = new List<GameObject>();
	public GameObject CameraPrefab;

    private GameObject CameraBaseObj;
    private string CameraBaseName = "Camera";

    private Vector3 Origin = new Vector3(0, 7, -10);
    private Quaternion OriginRotation = Quaternion.identity;

	public void Init() {
		Debug.Log("camera Init");
        CameraBaseObj = new GameObject(CameraBaseName);
        GameObject cameraPrefab = ResourceLoader.LoadPrefab("Prefab/Camera/MainCamera", Origin, OriginRotation, CameraBaseObj, true);
        cameraPrefab.SetActive(true);
		list.Add(cameraPrefab);
	}

	public GameObject Get(int index) {
		return list[index];
	}

	public void SetCharacter(int cameraIndex, int characterIndex) {
		list[cameraIndex].GetComponent<MainCamera>().SetCharacter(CharacterManager.Instance.Get(characterIndex));
	}

	public void SetScriptActive() {
		list[0].SetActive(true);
	}
}
