using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
//    public GameObject MainHero;
    public HeroManager HeroScript;
    public Back_GroundManager BGManagerScript;
    public CameraManager CManagerScript;
    public void SetUpScene()
    {
        Debug.Log("test");
        //GameObject hero = Instantiate<GameObject>(MainHero);
        //hero.SetActive(true);
        CManagerScript.Init();
        GameObject MainCamera = CManagerScript.Get(0);

        BGManagerScript.InitBG();
        BGManagerScript.SetCamera(MainCamera);

        HeroScript.InitHero(1);
        GameObject HeroObj = HeroScript.Get(0);
        CManagerScript.SetMainHero(HeroObj);
        BGManagerScript.SetMainHero(HeroObj);
        CManagerScript.SetScriptActive();
    }
}