using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : ScriptableObject 
{
    #region Singleton
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = ScriptableObject.CreateInstance<EventManager>();
            }
            return _instance;
        }
    }
    #endregion

    private Dictionary<Enums.Event, EventDelegate.onEvent> eventDict;

    public void Init()
    {
        if (eventDict == null)
        {
            eventDict = new Dictionary<Enums.Event, EventDelegate.onEvent>();
        }
    }

    // public delegate void OnEvent();
    // public static event OnEventReceived onMessageReceived;

    public void registerEvent(Enums.Event eventEnum, EventDelegate.onEvent func) {
        Debug.Log("event registered");
        EventDelegate.onEvent thisEvent;
        if (_instance.eventDict.TryGetValue(eventEnum, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent += func;

            //Update the Dictionary
            _instance.eventDict[eventEnum] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += func;
            _instance.eventDict.Add(eventEnum, thisEvent);
        }
    }

    public void unregisterEvnet(Enums.Event eventEnum, EventDelegate.onEvent func) {
        if (_instance == null) return;
        EventDelegate.onEvent thisEvent;
        if (_instance.eventDict.TryGetValue(eventEnum, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= func;

            //Update the Dictionary
            _instance.eventDict[eventEnum] = thisEvent;
        }
    }

    public void TriggerEvent(Enums.Event eventEnum)
    {
        Debug.Log("event triggered");
        EventDelegate.onEvent thisEvent = null;
        if (_instance.eventDict.TryGetValue(eventEnum, out thisEvent))
        {
            thisEvent.Invoke();
            // OR USE _instance.eventDict[eventEnum]();
        }
    }

}

namespace EventDelegate {
    public delegate void onEvent();
}
