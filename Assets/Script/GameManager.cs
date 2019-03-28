using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if(_instance == null) {
            _instance = this;
            Init();
        }
        else if(_instance != this) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void Init()
    {
        Debug.Log("start Init");

        SceneManager.Instance.Init();
        CameraManager.Instance.Init();
        BGManager.Instance.Init();
        CharacterManager.Instance.Init();
        ItemManager.Instance.Init();

        SetCharacterCamera(0, 0);
    }
    public void Update()
    {
        BGManager.Instance.Update();
    }

    private void SetCharacterCamera(int cameraIndex, int characterIndex)
    {
        CameraManager.Instance.SetCharacter(cameraIndex, characterIndex);
    }
}