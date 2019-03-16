using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BGManager : ScriptableObject {
	private List<List<GameObject>> ObjPools;
	private float LeftBounder = 0;
	private float RightBounder = 0;
	private float cWidth = 0;
	private float cHeight = 0;
	private Camera c;
	private Vector3 Origin = new Vector3(0, 5, 0);
	private Quaternion OriginRotation = Quaternion.identity;

	private string GroundBaseName = "Ground";    // name of the grond 
	private GameObject GroundBaseObj = null;     // all ground objects should be placed under this gobj

	private string SceneBaseName = "Scene";
	private GameObject SceneBaseObj = null; 

	private int GroundTypeCnt = 3;
	private string GroundPrefix = "Prefab/Ground/Gd_";
	private int SceneItemCnt = 2;
	private string ScenePrefix = "Prefab/Scene/Sc_";
	// store the basic info about each kind of ground block
	private List<GroundBasicInfo> GroundBasicInfoList;
	private List<GroundInfo> RGInfoList;
	private List<GroundInfo> LGInfoList;

	private List<SceneItemBasicInfo> SceneItemBasicInfoList;
	private List<SceneItemInfo> SIInfoList;

	public class PoolObj : MonoBehaviour{
		public GameObject GObj;
	}

	public void Awake() {
		Init();
	}

	private void Init() {
		Debug.Log("BG Init");
		c = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		cHeight = c.orthographicSize;
		cWidth = c.aspect * cHeight;
		LeftBounder = Origin.x;
		RightBounder = Origin.x;
		GroundBasicInfoList = new List<GroundBasicInfo>();
		RGInfoList = new List<GroundInfo>();
		LGInfoList = new List<GroundInfo>();
		SceneItemBasicInfoList = new List<SceneItemBasicInfo>();
		SIInfoList = new List<SceneItemInfo>();
		InitBaseObj();
		InitBasicGroundInfo();
		InitBasicSceneItemInfo();
	}



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float cXPos = c.transform.localPosition.x;
		int ObjIndex = 0;
		if(LeftBounder == RightBounder) {
			int type = 0;
			GroundBasicInfo tempBInfo = GroundBasicInfoList[type];
			string gname = tempBInfo.getGName();
			GameObject obj = ResourceLoader.LoadPrefab(gname, Origin, OriginRotation, GroundBaseObj, true);
			obj.transform.localPosition = Origin;
			obj.SetActive(true);
			RightBounder = RightBounder + tempBInfo.rightEdge.x;
			LeftBounder = LeftBounder + tempBInfo.leftEdge.x;
			RGInfoList.Add(new GroundInfo(0,type,GroundBasicInfoList[0].leftEdge + (Vector2)Origin, GroundBasicInfoList[0].rightEdge + (Vector2)Origin, Origin, obj));
			LGInfoList.Add(new GroundInfo(0,type,GroundBasicInfoList[0].leftEdge + (Vector2)Origin, GroundBasicInfoList[0].rightEdge + (Vector2)Origin, Origin, obj));
		}
		while(cXPos + cWidth >= RightBounder) {
			int type = Random.Range(0, GroundTypeCnt);
			bool flip = Random.Range(0, 2) == 0 ? true : false;
			GroundInfo LastGInfo = RGInfoList[RGInfoList.Count-1];
			if(LastGInfo.type != 0) {
				type = 0;
				flip = true;
			}
			if(RGInfoList.Count <= 2) {
				type = 0;
				flip = true;
			}
			GroundBasicInfo tempBInfo = GroundBasicInfoList[type];
			string gname = tempBInfo.getGName();
			GameObject obj = ResourceLoader.LoadPrefab(gname, Origin, OriginRotation, GroundBaseObj, true);
			Vector2 newLeftEdge;
			Vector2 newOrigin;
			Vector2 newRightEdge;
			if(flip){
				newLeftEdge = LastGInfo.rightEdge;
				newOrigin = newLeftEdge - tempBInfo.leftEdge;
				newRightEdge = newOrigin + tempBInfo.rightEdge;
			}
			else {
				obj.transform.localScale = new Vector2(-1, 1);
				newLeftEdge = LastGInfo.rightEdge;
				newOrigin = new Vector2(newLeftEdge.x + tempBInfo.rightEdge.x, newLeftEdge.y - tempBInfo.rightEdge.y);
				newRightEdge = new Vector2(newOrigin.x - tempBInfo.leftEdge.x, newOrigin.y + tempBInfo.leftEdge.y);
			}
			obj.transform.localPosition = newOrigin;
			RGInfoList.Add(new GroundInfo(0,type,newLeftEdge, newRightEdge, newOrigin, obj));
			obj.SetActive(true);

			AddSceneItem(newLeftEdge, newRightEdge, Random.Range(0, SceneItemCnt));

			RightBounder = RightBounder + tempBInfo.rightEdge.x - tempBInfo.leftEdge.x;
		}
		while(cXPos - cWidth <= LeftBounder) {
			int type = Random.Range(0, GroundTypeCnt);
			bool flip = Random.Range(0, 2) == 0 ? true : false;
			GroundInfo LastGInfo = LGInfoList[LGInfoList.Count-1];
			if(LastGInfo.type != 0) {
				type = 0;
				flip = true;
			}
			if(LGInfoList.Count <= 2) {
				type = 0;
				flip = true;
			}
	
			GroundBasicInfo tempBInfo = GroundBasicInfoList[type];
			string gname = tempBInfo.getGName();
			GameObject obj = ResourceLoader.LoadPrefab(gname, Origin, OriginRotation, GroundBaseObj, true);
			Vector2 newLeftEdge;
			Vector2 newOrigin;
			Vector2 newRightEdge;
			if(flip){
				newRightEdge = LastGInfo.leftEdge;
				newOrigin = newRightEdge - tempBInfo.rightEdge;
				newLeftEdge = newOrigin + tempBInfo.leftEdge;
			}
			else {
				obj.transform.localScale = new Vector2(-1, 1);
				newRightEdge = LastGInfo.leftEdge;
				newOrigin = new Vector2(newRightEdge.x + tempBInfo.leftEdge.x, newRightEdge.y - tempBInfo.leftEdge.y);
				newLeftEdge = new Vector2(newOrigin.x - tempBInfo.rightEdge.x, newOrigin.y + tempBInfo.rightEdge.y);
			}
			obj.transform.localPosition = newOrigin;
			LGInfoList.Add(new GroundInfo(0, type, newLeftEdge, newRightEdge, newOrigin, obj));
			obj.SetActive(true);

			AddSceneItem(newLeftEdge, newRightEdge, Random.Range(0, SceneItemCnt));

			LeftBounder = LeftBounder - tempBInfo.rightEdge.x + tempBInfo.leftEdge.x;
		}
	}

	// init base gournd obj. all ground objects will be loaded on this object.
	public void InitBaseObj() {
		GroundBaseObj = new GameObject(GroundBaseName);
		SceneBaseObj = new GameObject(SceneBaseName);
	}

	public void InitBasicGroundInfo() {
		for(int i = 0; i < GroundTypeCnt; i++) {
			string GroundName = GroundPrefix + (i+1).ToString();
			GameObject GroundGObj = ResourceLoader.LoadPrefab(GroundName, Origin, OriginRotation, GroundBaseObj, true);
			if(GroundGObj.GetComponent<BoxCollider2D>() != null) {
				BoxCollider2D TempBoxCollider2D = GroundGObj.GetComponent<BoxCollider2D>();
				float top = TempBoxCollider2D.offset.y + (TempBoxCollider2D.size.y / 2f);
				float btm = TempBoxCollider2D.offset.y - (TempBoxCollider2D.size.y / 2f);
				float left = TempBoxCollider2D.offset.x - (TempBoxCollider2D.size.x / 2f);
				float right = TempBoxCollider2D.offset.x + (TempBoxCollider2D.size.x /2f);
				GroundBasicInfoList.Add(new GroundBasicInfo(new Vector2(left, top), new Vector2(right, top), GroundName));
			}
			else if(GroundGObj.GetComponent<PolygonCollider2D>()!=null){
				PolygonCollider2D TempPolygonCollider2D = GroundGObj.GetComponent<PolygonCollider2D>();
				Vector2[] points = TempPolygonCollider2D.points;
				Vector2[] sortedPoints = points.OrderBy(v => v.x).ToArray<Vector2>();
				int index = 0;
				float left = sortedPoints[index].x;
				float right = sortedPoints[sortedPoints.Length-1].x;
				float leftTop = sortedPoints[index].y;
				float rightTop = sortedPoints[sortedPoints.Length-1].y;
				index = index + 1;
				while(index< sortedPoints.Length && sortedPoints[index].x == left) {
					leftTop = Mathf.Max(leftTop, sortedPoints[index].y);
					index = index + 1;
				}
				index = sortedPoints.Length - 2;
				while(index >= 0 && sortedPoints[index].x == right) {
					rightTop = Mathf.Max(rightTop, sortedPoints[index].y);
					index = index - 1;
				}
				GroundBasicInfoList.Add(new GroundBasicInfo(new Vector2(left, leftTop), new Vector2(right, rightTop), GroundName));
			}
			else {
			}
		}
	}

	public void InitBasicSceneItemInfo() {
		for(int i = 0; i < SceneItemCnt; i++) {
			string SIName = ScenePrefix + (i+1).ToString();
			GameObject SIGObj = ResourceLoader.LoadPrefab(SIName, Origin, OriginRotation, GroundBaseObj, false);
			if(SIGObj.GetComponent<CircleCollider2D>() != null) {
				CircleCollider2D Temp = SIGObj.GetComponent<CircleCollider2D>();
				float top = Temp.offset.y + Temp.radius;
				float btm = Temp.offset.y - Temp.radius;
				float left = Temp.offset.x - Temp.radius;
				float right = Temp.offset.x + Temp.radius;
				SceneItemBasicInfoList.Add(new SceneItemBasicInfo(new Vector2(left, top), new Vector2(right, btm), SIName));
			}
			else if(SIGObj.GetComponent<BoxCollider2D>() != null) {
				BoxCollider2D TempBoxCollider2D = SIGObj.GetComponent<BoxCollider2D>();
				float top = TempBoxCollider2D.offset.y + (TempBoxCollider2D.size.y / 2f);
				float btm = TempBoxCollider2D.offset.y - (TempBoxCollider2D.size.y / 2f);
				float left = TempBoxCollider2D.offset.x - (TempBoxCollider2D.size.x / 2f);
				float right = TempBoxCollider2D.offset.x + (TempBoxCollider2D.size.x /2f);
				SceneItemBasicInfoList.Add(new SceneItemBasicInfo(new Vector2(left, top), new Vector2(right, btm), SIName));
			}
			else if(SIGObj.GetComponent<PolygonCollider2D>()!=null){
				PolygonCollider2D TempPolygonCollider2D = SIGObj.GetComponent<PolygonCollider2D>();
				Vector2[] points = TempPolygonCollider2D.points;
				Vector2[] sortedPoints = points.OrderBy(v => v.x).ToArray<Vector2>();
				int index = 0;
				float left = sortedPoints[index].x;
				float right = sortedPoints[sortedPoints.Length-1].x;
				float leftTop = sortedPoints[index].y;
				float rightBtm = sortedPoints[sortedPoints.Length-1].y;
				index = index + 1;
				while(index< sortedPoints.Length && sortedPoints[index].x == left) {
					leftTop = Mathf.Max(leftTop, sortedPoints[index].y);
					index = index + 1;
				}
				index = sortedPoints.Length - 2;
				while(index >= 0 && sortedPoints[index].x == right) {
					rightBtm = Mathf.Min(rightBtm, sortedPoints[index].y);
					index = index - 1;
				}
				SceneItemBasicInfoList.Add(new SceneItemBasicInfo(new Vector2(left, leftTop), new Vector2(right, rightBtm), SIName));
			}
			else {
			}
		}
	}

	public void AddSceneItem(Vector2 vl, Vector2 vr, int index) {
		float ybtm = Mathf.Max(vl.y, vr.y);
		float xpos = Random.Range(vl.x, vr.x);
		string SIName = ScenePrefix + (index+1).ToString();
		GameObject gobj = ResourceLoader.LoadPrefab(SIName, Origin, OriginRotation, SceneBaseObj, true);
		gobj.transform.localPosition = new Vector3(xpos, ybtm - 2 * SceneItemBasicInfoList[index].RBVector.y, 0);
		gobj.SetActive(true);
	}

	public string GetScenePrefix() {
		return ScenePrefix;
	}
}

public class SceneItemBasicInfo : BGManager {
	public string SIName;
	public Vector2 LTVector;
	public Vector2 RBVector;
	public SceneItemBasicInfo(Vector2 lt, Vector2 rb, string name) {
		SIName = name;
		LTVector = lt;
		RBVector = rb;
	}
}

public class SceneItemInfo: BGManager {
}

public class GroundBasicInfo : ScriptableObject{
	public Vector2 leftEdge;
	public Vector2 rightEdge;
	public string GName;
	public GroundBasicInfo(Vector2 left, Vector2 right, string gname) {
		leftEdge = left;
		rightEdge = right;
		GName = gname;
	}
	public string getGName(){
		return GName;
	}
}

// store all ground left edge, right edge, x, y offset. Make it easy to reuse in future.
public class GroundInfo : BGManager {
	public int index;
	public int type;
	public Vector2 leftEdge;
	public Vector2 rightEdge;
	public Vector2 offset;
	public GameObject GObj;
	public GroundInfo(int idx, int t, Vector2 l, Vector2 r, Vector2 o, GameObject G) {
		index = idx;
		type = t;
		leftEdge = l;
		rightEdge = r;
		offset = o;
		GObj = G;
	}
}