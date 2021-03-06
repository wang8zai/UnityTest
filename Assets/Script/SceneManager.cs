using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : ScriptableObject
{
    #region Singleton
    private static SceneManager _instance;
     public static SceneManager Instance {
         get {
             if(_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<SceneManager>();
            }
            return _instance;
         }
     }
    #endregion

    public void Init() {
    }

}