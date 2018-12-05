using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour {

	private List<List<GameObject>> ObjPools;
	private float LeftBounder = 0;
	private float RightBounder = 0;
	private float cWidth = 0;
	private float cHeight = 0;
	private Camera c;
	private int GroundCount = 10;
	private int Pointer = 0;
	private float ScreenHeight;
	private float ScreenWidth;
	private Vector3 Origin = new Vector3(0, 5, 0);
	public class PoolObj : MonoBehaviour{
		public GameObject GObj;

	}

	// Use this for initialization
	void Start () {
		c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		cHeight = c.orthographicSize;
		cWidth = c.aspect * cHeight;
		LeftBounder = Origin.x;
		RightBounder = Origin.x;
		ObjPools = new List<List<GameObject>>();
		ObjPools.Add(new List<GameObject>());
		for(int i = 0; i < GroundCount; i++) {
			ObjPools[0].Add(Instantiate(transform.Find("Ground/Gd").gameObject, Origin, new Quaternion(0, 0, 0, 1), transform.Find("Ground")));
			ObjPools[0][i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float cXPos = c.transform.localPosition.x;
		int ObjIndex = 0;
		if(LeftBounder == RightBounder) {
			GameObject obj = ObjPools[ObjIndex][Pointer];
			obj.transform.localPosition = Origin;
			obj.SetActive(true);
			RightBounder = RightBounder + obj.GetComponent<BoxCollider2D>().bounds.size.x / 2;
			LeftBounder = LeftBounder - obj.GetComponent<BoxCollider2D>().bounds.size.x / 2;
			Pointer = Pointer + 1;
		}
		while(cXPos + cWidth >= RightBounder) {
			if(Pointer >= ObjPools[ObjIndex].Count) Pointer = 0;
			GameObject obj = ObjPools[ObjIndex][Pointer];
			obj.SetActive(true);
			obj.transform.localPosition = new Vector3(RightBounder + obj.GetComponent<BoxCollider2D>().bounds.size.x / 2, Origin.y, Origin.z);
			RightBounder = RightBounder + obj.GetComponent<BoxCollider2D>().bounds.size.x;
			Pointer = Pointer + 1;
		}
		while(cXPos - cWidth <= LeftBounder) {
			if(Pointer >= ObjPools[ObjIndex].Count) Pointer = 0;
			GameObject obj = ObjPools[ObjIndex][Pointer];
			obj.SetActive(true);
			obj.transform.localPosition = new Vector3(LeftBounder - obj.GetComponent<BoxCollider2D>().bounds.size.x / 2, Origin.y, Origin.z);
			LeftBounder = LeftBounder - obj.GetComponent<BoxCollider2D>().bounds.size.x / 2;
			Pointer = Pointer + 1;
		}
	}
}
