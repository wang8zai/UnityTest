using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_Ground : MonoBehaviour
{
    private Back_Ground BGInstance = null;
    private GameObject BGObj = null;

    public GameObject cameraobject;
    public GameObject Ground;
    public GameObject Body;
    public GameObject Bridge;

    private GameObject Ground_past;

    private float ScreenHeight;
    private float ScreenWidth;
    private float ScrollStart;
    private float CurrentStart;

    private Vector3 minX;
    private Vector3 minY;
    private Vector3 maxX;
    private Vector3 maxY;


    private Vector3 BridgeTransform;
    private Vector3 BridgeSize; 
    // Use this for initialization
    void Start()
    {
//        Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
//        Camera camera = cameraobject.GetComponent<Camera>();
        Camera camera = cameraobject.GetComponent<Camera>();
        ScreenHeight = camera.orthographicSize * 2f;
        ScreenWidth = camera.aspect * ScreenHeight;

        ScreenWidth = ScreenHeight * Screen.width / Screen.height;

        Debug.Log(Ground.GetComponent<BoxCollider2D>().bounds.size);
        Vector3 GroundSize = Ground.GetComponent<BoxCollider2D>().bounds.size;
        Vector3 GroundTransform = Ground.GetComponent<BoxCollider2D>().transform.position;
        Vector3 BridgeTransform = Bridge.GetComponent<PolygonCollider2D>().transform.position;



        SetMinMaxPoly2D(Bridge.GetComponent<PolygonCollider2D>());
//        Debug.Log("Brideg " + maxX);
//        Debug.Log("Brideg " + maxY);
//        Debug.Log("Brideg " + minX);
//        Debug.Log("Brideg " + minY);
        Bridge.SetActive(false);
        BridgeSize = new Vector3(maxX.x - minX.x, maxY.y - minY.y, 0);
        ScrollStart = GroundSize.x / 2;
        //        Debug.Log("Brideg " + BridgeSize);
        //        Debug.Log("transform" + BridgeTransform);
//        Instantiate(Ground, new Vector3(GroundSize.x / 2 + ScrollStart, GroundTransform.y, 0), new Quaternion());
//        Instantiate(Ground, new Vector3(GroundSize.x / 2 + ScrollStart, GroundTransform.y, 0), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////////////////////////////////
        //build ground unit
        ////////////////////////////////////////////        
        //Debug.Log("Scroll Start " + ScrollStart);


        if(ScrollStart <= Body.transform.position.x + Ground.GetComponent<BoxCollider2D>().bounds.size.x/2)
        {
//            Debug.Log("Scroll");
                    Vector3 GroundTransform = Ground.GetComponent<BoxCollider2D>().transform.position;
                    Vector3 GroundSize = Ground.GetComponent<BoxCollider2D>().bounds.size;
                    Ground_past = Instantiate(Ground, new Vector3(GroundSize.x / 2 + ScrollStart, GroundTransform.y, 0), new Quaternion());
//                    Ground_past = Instantiate(Ground, new Vector3(2.0F, -10, 0), Quaternion.identity);
                    ScrollStart = ScrollStart + Ground.GetComponent<BoxCollider2D>().bounds.size.x;
                }

  
        
            
        //        Debug.Log("Bridege size " + BridgeSize);

        ////////////////////////////////////////////
        // build bridge 
        ////////////////////////////////////////////
/*
        if (ScrollStart <= Body.transform.position.x + BridgeSize.x / 2)
        {
//            Bridge.SetActive(true);
            Debug.Log("Scroll");


//            Ground_past = Instantiate(Bridge, new Vector3(BridgeSize.x / 2 + ScrollStart, Ground.GetComponent<BoxCollider2D>().transform.position.y + BridgeSize.y / 4, 0), new Quaternion());
//            Ground_past.SetActive(true);
            Debug.Log(ScrollStart);
            ScrollStart = ScrollStart + BridgeSize.x;

            Debug.Log("Initial " + new Vector3(BridgeSize.x / 2 + ScrollStart, BridgeTransform.y, 0));
            Debug.Log(ScrollStart);
        }


*/



        /*        if (Body.GetComponent<Body>().Walkflag)
                {
                    if (Body.GetComponent<Body>().Walkforward)
                    {
                        GetComponent<Back_Ground>().transform.Translate(new Vector3(-Time.deltaTime * 16.0f, 0, 0));
                    }
                    else if (!Body.GetComponent<Body>().Walkforward)
                    {
                        GetComponent<Back_Ground>().transform.Translate(new Vector3(Time.deltaTime * 16.0f, 0, 0));
                    }
                }

            }
            */
    }


    void SetMinMaxPoly2D(PolygonCollider2D poly)
    {
        minX = new Vector3(Mathf.Infinity, 0, 0);
        minY = new Vector3(0, Mathf.Infinity, 0);
        maxX = new Vector3(-Mathf.Infinity, 0, 0);
        maxY = new Vector3(0, -Mathf.Infinity, 0);

        for (var i = 0; i < poly.pathCount; i++)
        {
            var path = poly.GetPath(i);
            foreach (Vector3 v in path)
            {
                Vector3 u = transform.TransformPoint(v);
                if (u.x < minX.x)
                    minX = u;
                if (u.y < minY.y)
                    minY = u;
                if (u.x > maxX.x)
                    maxX = u;
                if (u.y > maxY.y)
                    maxY = u;
            }
        }

        var z = transform.position.z;
        minX.z = z;
        maxX.z = z;
        minY.z = z;
        maxY.z = z;
    }

    public void SetCamera(GameObject cobj) {
        cameraobject = cobj;
    }

    public void SetMainHero(GameObject obj) {
        Body = obj;
    }

    public void SetBGObj(GameObject obj) {
        BGObj = obj;
    }

    public GameObject Get() {
        return BGObj;
    }
}
