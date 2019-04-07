using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : BaseCharacter {
    protected void Start() {
        base.Start();
        StartCoroutine(LRcoroutine());
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
            bool LRBool  = (Random.value > 0.5f);
            if(LRBool) {
                Enums.keycodes LRDirection = (Random.value > 0.5f) ? Enums.keycodes.CLeft : Enums.keycodes.CRight;
                PController.SetTrigger(LRDirection, true);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
        }
    }
}
