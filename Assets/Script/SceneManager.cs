using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : ScriptableObject
{
//    public GameObject MainHero;
    private static SceneManager instance;
    public static SceneManager Instance {
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
        // CManagerScript.Init();
        // GameObject MainCamera = CManagerScript.Get(0);

        // BGManagerScript.InitBG();
        // BGManagerScript.SetCamera(MainCamera);

        // HeroScript.InitHero(1);
        // GameObject HeroObj = HeroScript.Get(0);
        // CManagerScript.SetMainHero(HeroObj);
        // BGManagerScript.SetMainHero(HeroObj);
        // CManagerScript.SetScriptActive();
    }
    public void Init() {

    }
}