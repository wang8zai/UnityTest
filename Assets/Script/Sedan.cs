using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedan : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		string objname = coll.gameObject.name;
		objname = objname.Remove(3);
		if (objname == "Sc_")
		{	
			foreach (ContactPoint2D Hits in coll.contacts)
            {
                Vector2 hitPoint = Hits.point;
				GameObject hobj = coll.gameObject;
				Vector2 hitNormal = Hits.normal;

				Vector3 hited = hitPoint;
				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(3000* hitNormal, hited);
				CharacterManager.Instance.Get(0).GetComponent<BaseCharacter>().SetCollisionInt(1);

				coll.gameObject.GetComponent<sceneItem>().minusDuration(1);
            }
		}
	}
}
