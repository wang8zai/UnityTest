using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    // private SceneManager sceneManager;
    // private HeroManager characterManager;
    // private Back_GroundManager backGroundManager;
    // private CameraManager cameraManager;
    public void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(this.gameObject);
        }
        Init();
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        SceneManager sceneManager = SceneManager.Instance;
        CameraManager cameraManager = CameraManager.Instance;
        Back_GroundManager backGroundManager = Back_GroundManager.Instance;
        HeroManager heroManager = HeroManager.Instance;
    }
}