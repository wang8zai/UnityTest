using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader{
    // prefabName. name of prefab
    // instantiated. load prefab only or instantiate the prefab after loading.
    public static GameObject LoadPrefab(string prefabName, Vector3 origin, Quaternion originRotation, GameObject parent, bool instantiated) {
        Debug.Log("Prefab Name " + prefabName);
        GameObject rtn = Resources.Load<GameObject>(prefabName);
        if(instantiated) {
            rtn = GameObject.Instantiate(rtn.transform, origin, originRotation, parent.transform).gameObject;
        }
        return rtn;
    }
}
