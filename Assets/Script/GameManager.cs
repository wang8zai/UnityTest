using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    public SceneManager sceneManager;
    public CharacterManager characterManager;
    public BGManager backGroundManager;
    public CameraManager cameraManager;
    public ItemManager itemManager;
    public GameManager Get()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }
        else if(instance != this)
        {
            instance = this;
        }
        return instance;
    }
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
        sceneManager = ScriptableObject.CreateInstance<SceneManager>();
        sceneManager.Init(this);
        cameraManager = ScriptableObject.CreateInstance<CameraManager>();
        cameraManager.Init(this);
        backGroundManager = ScriptableObject.CreateInstance<BGManager>();
        backGroundManager.Init(this);
        characterManager = ScriptableObject.CreateInstance<CharacterManager>();
        characterManager.Init(this);
        itemManager = ScriptableObject.CreateInstance<ItemManager>();
        itemManager.Init(this);
        SetCharacterCamera(0, 0);
    }
    public void Update()
    {
        backGroundManager.Update();
    }

    private void SetCharacterCamera(int cameraIndex, int characterIndex)
    {
        cameraManager.SetCharacter(cameraIndex, characterIndex);
    }
}