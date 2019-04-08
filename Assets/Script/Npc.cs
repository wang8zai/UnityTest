using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : BaseCharacter {
    private IEnumerator LRcoroutineInstance;
    protected void Start() {
        EventManager.Instance.registerEvent(Enums.Event.NpcIntersect, NpcIntersectFunc);
        base.Start();
        LRcoroutineInstance = LRcoroutine();
        StartCoroutine(LRcoroutineInstance);
    }

    protected void Update() {
        base.Update();
    }

    private IEnumerator LRcoroutine()
    {
        while (true)
        {
            PController.SetTrigger(Enums.keycodes.CLeft, false);
            PController.SetTrigger(Enums.keycodes.CRight, false);
            PController.SetKDTrigger(Enums.keycodes.CJump, false);
            bool LRBool  = (Random.value > 0.5f);
            if(LRBool) {
                Enums.keycodes LRDirection = (Random.value > 0.5f) ? Enums.keycodes.CLeft : Enums.keycodes.CRight;
                PController.SetTrigger(LRDirection, true);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
        }
    }

    public void NpcIntersectFunc() {
        if(LRcoroutineInstance != null) {
            StopCoroutine(LRcoroutineInstance);
            LRcoroutineInstance = null;
            PController.SetTrigger(Enums.keycodes.CLeft, false);
            PController.SetTrigger(Enums.keycodes.CRight, false);
            PController.SetKDTrigger(Enums.keycodes.CJump, false);
            PController.SetKDTrigger(Enums.keycodes.CJump, true);
        }
        else {
            LRcoroutineInstance = LRcoroutine();
            StartCoroutine(LRcoroutineInstance);
        }
	}
}
