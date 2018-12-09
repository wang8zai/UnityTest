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

	void OnCollisionEnter2D(Collision2D coll)
	{
		string objname = coll.gameObject.name;
		objname = objname.Remove(3);
		if (objname == "Sc_")
		{	
			foreach (ContactPoint2D Hits in coll.contacts)
            {
                Vector2 hitPoint = Hits.point;
				GameObject hobj = coll.gameObject;
				Vector3 direction = transform.InverseTransformPoint(hitPoint);
				gameObject.GetComponent<Rigidbody2D>().AddForce(-3000* direction);
				GameObject.Find("Player1").GetComponent<MainHeroSprite>().SetCollisionFlag(true);
				GameObject.Find("Player2").GetComponent<MainHeroSprite>().SetCollisionFlag(true);
            }
			Destroy(coll.gameObject);
		}
	}
}
