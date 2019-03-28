using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : ScriptableObject 
{
    #region Singleton
    private static EventHandler _instance;
    public static EventHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<EventHandler>();
            }
            return _instance;
        }
    }
    #endregion

    public delegate void OnMessageReceived();
    public static event OnMessageReceived onMessageReceived;

}
