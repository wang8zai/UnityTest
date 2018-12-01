using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    public SceneManager SceneScript;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        initGame();
    }

    public void initGame()
    {
        SceneScript.SetUpScene();
    }
}