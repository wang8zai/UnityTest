using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    private SceneManager sceneManager;
    private HeroManager characterManager;
    private BGManager backGroundManager;
    private CameraManager cameraManager;
    public void Awake()
    {
        if(instance == null) {
            instance = this;
            Init();
        }
        else if(instance != this) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void Init()
    {
        Debug.Log("start Init");
        sceneManager = new SceneManager();
        cameraManager = new CameraManager();
        backGroundManager = new BGManager();
        characterManager = new HeroManager();
    }
}