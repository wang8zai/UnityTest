using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : BaseCharacter {
    private IEnumerator LRcoroutine; 

    protected void Start() {
        base.Start();
        LRcoroutine = WaitAndPrint(2.0f);
        StartCoroutine(LRcoroutine);
    }
    protected void Update() {
        base.Update();
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }
}
