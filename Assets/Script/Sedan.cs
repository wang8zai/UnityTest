﻿using System.Collections;
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
				Vector2 hitNormal = Hits.normal;
    			// rb.velocity = Vector2.Reflect(lastVelocity, surfaceNormal);
				// Vector3 hited = transform.InverseTransformPoint(hitPoint);
				Vector3 hited = hitPoint;
				Debug.Log("hitPoint " + Hits.point);
				Debug.Log("Hit Point " + hited);
				Debug.Log("Hit Normal " + (1000* hitNormal));	
				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(10* hitNormal, hited);
				GameObject.Find("Player1").GetComponent<MainHeroSprite>().SetCollisionInt(1);
				GameObject.Find("Player2").GetComponent<MainHeroSprite>().SetCollisionInt(1);

				coll.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-10* hitNormal, hited);
            }

			// Destroy(coll.gameObject);
		}
	}
}
