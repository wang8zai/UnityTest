using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sedan : MonoBehaviour {

	private GameManager gameManager = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetGameManager(GameManager gm) {
		gameManager = gm;
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
    			// rb.velocity = Vector2.Reflect(lastVelocity, surfaceNormal);
				// Vector3 hited = transform.InverseTransformPoint(hitPoint);
				Vector3 hited = hitPoint;
				// Debug.Log("hitPoint " + Hits.point);
				// Debug.Log("Hit Point " + hited);
				// Debug.Log("Hit Normal " + (1000* hitNormal));	
				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(10* hitNormal, hited);
				gameManager.characterManager.Get(0).GetComponent<BaseCharacter>().SetCollisionInt(1);
				// GameObject.Find("Player2").GetComponent<MainHeroSprite>().SetCollisionInt(1);

				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-10* hitNormal, hited);
				coll.gameObject.GetComponent<sceneItem>().minusDuration(1);
            }

			// Destroy(coll.gameObject);
		}
	}
}
