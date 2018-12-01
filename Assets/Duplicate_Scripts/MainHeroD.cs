// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MainHeroD : body_part
// {
//     private GameObject MainHeroObj = null;

// //    public GameObject Body;
//     public GameObject LeftLeg;
//     public GameObject RightLeg;
//     public GameObject RightArm;
//     public GameObject LeftArm;
//     public GameObject Head;

//     private Vector2 LeftLegVector;
//     private Vector2 RightLegVector;
//     private Vector2 InterLegVector;
//     private float yheight = 20.0f;

//     private float BodyAngle = 10.0f;

//     private bool walkflag = false;
//     private bool LegFlag = true;
//     private bool StopFlag = false;
//     private bool walkforward = true;
//     private float MaxLegAngle = 60;
//     private float defaultLegspeed = 60000.0f;

//     private float StandAngleThreshold = 0.0f;


//     public bool Walkflag
//     {
//         get
//         {
//             return walkflag;
//         }
//         set
//         {
//             walkflag = value;
//         }
//     }
//     public bool Walkforward
//     {
//         get
//         {
//             return walkforward;
//         }
//         set
//         {
//             walkforward = value;
//         }
//     }


//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         Debug.Log("COLLISION");
//         if (collision.gameObject.layer == 12)
//         {

//         }
//     }

//     public void SetBodyPartsMass(int[] weights) {
//         SetMass(weights[1]);
//         LeftLeg.gameObject.GetComponent<LeftLeg>().SetMass(weights[3]);       
//         RightLeg.gameObject.GetComponent<RightLeg>().SetMass(weights[3]); 
//         LeftArm.gameObject.GetComponent<LeftArm>().SetMass(weights[2]);
//         RightArm.GetComponent<RightArm>().SetMass(weights[2]);

// //        ((Body)transform.Find("Body").gameObject).SetMass(h["body"]);
// //        ((LeftArm)transform.Find("LeftArm").gameObject).SetMass(h["arm"]);
//     }

//     public void SetBodyGravityScale(int scale) {
//         SetGravityScale(scale);
//         LeftLeg.GetComponent<LeftLeg>().SetGravityScale(scale);
//         RightLeg.GetComponent<RightLeg>().SetGravityScale(scale);
//         LeftArm.GetComponent<LeftArm>().SetGravityScale(scale);
//         RightArm.GetComponent<RightArm>().SetGravityScale(scale);
// //        Head.GetComponent<Head>().SetGravityScale(scale);
//     }

//     public void InitHero(GameObject obj) {
//         if(MainHeroObj == null) {
//             MainHeroObj = Instantiate<GameObject>(obj);
//         }
//     }

//     public GameObject Get() {
//         return MainHeroObj;
//     }



//     private void FixedUpdate()
//     {
// //      Stand Pose
//         AnglePID(0.0f);

//         if (Input.GetKey(KeyCode.Z))
//         {
//             float PID_P = 10.0f;
//             float PID_I = 150.0f;
//             float PID_D = 1;
//             PIDControl.PID(+90.0f, LeftLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//             PIDControl.PID(-90.0f, RightLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//         }

// //      Move back/forward
//         if (Input.GetAxis("Horizontal") != 0.0f)
//         {
//             StopFlag = false;
//             LeftLeg.GetComponent<LeftLeg>().Stopflag = false;
//             RightLeg.GetComponent<RightLeg>().Stopflag = false;
//             if (Input.GetAxis("Horizontal") > 0)
//             {
//                 AnglePID(-BodyAngle);
//                 walkflag = true;
//                 walkforward = true;
//                 LeftLeg.GetComponent<LeftLeg>().Directionflag = true;
//                 RightLeg.GetComponent<RightLeg>().Directionflag = true;
//             }
//             else
//             {
//                 AnglePID(BodyAngle);
//                 walkflag = true;
//                 walkforward = false;
//                 LeftLeg.GetComponent<LeftLeg>().Directionflag = false;
//                 RightLeg.GetComponent<RightLeg>().Directionflag = false;
//             }


// //            PostionY(1.3f);
//             float bodyangle = GetComponent<Rigidbody2D>().rotation;
// /*
//             if (walkflag == false)
//             {
//                 walkflag = true;
//                 LeftLeg.GetComponent<LeftLeg>().Walkflag = true;
//                 RightLeg.GetComponent<RightLeg>().Walkflag = true;
//             }
// */

//             LeftLeg.GetComponent<LeftLeg>().Walkflag = true;
//             RightLeg.GetComponent<RightLeg>().Walkflag = true;
//             //            Debug.Log(LegFlag);
//             //            Debug.Log(MaxLegAngle);
//             //            float LeftLegAngles = LeftLeg.transform.rotation.eulerAngles.z + BodyAngle;
//             //            Debug.Log(GetComponent<Rigidbody2D>().rotation);
//             float LeftLegAngles = LeftLeg.transform.rotation.eulerAngles.z - GetComponent<Rigidbody2D>().rotation;
//             LeftLegAngles = LeftLegAngles >= 180.0f ? LeftLegAngles - 360.0f : LeftLegAngles;
// //            Debug.Log(LeftLegAngles);
//             if (LegFlag == false)
//             {
// //                Debug.Log("LEGFLAGA");
//                 LeftLeg.GetComponent<Rigidbody2D>().AddTorque(defaultLegspeed * Time.deltaTime);
//                 RightLeg.GetComponent<Rigidbody2D>().AddTorque(-defaultLegspeed * Time.deltaTime);
// //                Debug.Log("Force " + defaultLegspeed * Time.deltaTime);
//             }
//             else
//             {
// //                Debug.Log("LEGFLAGB");
//                 LeftLeg.GetComponent<Rigidbody2D>().AddTorque(-defaultLegspeed * Time.deltaTime);
//                 RightLeg.GetComponent<Rigidbody2D>().AddTorque(defaultLegspeed * Time.deltaTime);
// //                Debug.Log("Force " + -defaultLegspeed * Time.deltaTime);
//             }

//             if (LeftLegAngles < -MaxLegAngle)
//             {
//                 LeftLeg.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//                 RightLeg.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//                 LegFlag = false;
//             }
//             else if (LeftLegAngles > MaxLegAngle)
//             {
//                 LeftLeg.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//                 RightLeg.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
//                 LegFlag = true;
//             }

//         }
//         //      Stand by
//         else
//         {
//             float PID_P = 2000.0f;
//             float PID_I = 200.0f;
//             float PID_D = 1;
//             float InitialAngle = 0.0f;
//             walkflag = false;
//             StopFlag = true;
//             LeftLeg.GetComponent<LeftLeg>().Stopflag = true;
//             RightLeg.GetComponent<RightLeg>().Stopflag = true;
//             float LeftLegAngle = LeftLeg.transform.rotation.eulerAngles.z;
//             LeftLegAngle = LeftLegAngle >= 180.0f ? LeftLegAngle - 360.0f : LeftLegAngle;
// //            Debug.Log("LEFTLEGANGLE " + LeftLegAngle + " LEFTLEG FLAG " + LeftLeg.GetComponent<LeftLeg>().Walkflag);

//             if (LeftLegAngle > InitialAngle + StandAngleThreshold || LeftLegAngle < InitialAngle - StandAngleThreshold)
//             {
//                 PIDControl.PID(InitialAngle, LeftLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//             }
//             else
//             {
//                 LeftLeg.GetComponent<LeftLeg>().Walkflag = false;
//             }

//             float RightLegAngle = RightLeg.transform.rotation.eulerAngles.z;
//             RightLegAngle = RightLegAngle >= 180.0f ? RightLegAngle - 360.0f : RightLegAngle;
// //            Debug.Log("RIGHTLEGANGLE " + RightLegAngle + " RIGHTLEG FLAG " + RightLeg.GetComponent<RightLeg>().Walkflag);

//             if (RightLegAngle > InitialAngle + StandAngleThreshold || RightLegAngle < InitialAngle - StandAngleThreshold)
//             {
//                 PIDControl.PID(InitialAngle, RightLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//             }
//             else
//             {
//                 RightLeg.GetComponent<RightLeg>().Walkflag = false;
//             }
//         }

// //      Sit down




// //      Hand up
//         if (Input.GetAxis("Vertical") > 0.0f)
//         {
//             float PID_P = 20.0f;
//             float PID_I = 60.0f;
//             float PID_D = 1;
//             PIDControl.PID(180.0f, LeftArm.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//             PIDControl.PID(180.0f, RightArm.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//         }

// //      Hand down
//         else if (Input.GetAxis("Vertical") < 0.0f)
//         {
//             float PID_P = 20.0f;
//             float PID_I = 60.0f;
//             float PID_D = 1;
//             PIDControl.PID(0.0f, LeftArm.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//             PIDControl.PID(0.0f, RightArm.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//         }

//         if (Input.GetMouseButtonDown(0))
//         {

//         }
//     }

//     private void PostionY(float targetY)
//     {
//         float PID_P = 2250.0f;
//         float PID_I = 9250.0f;
//         float PID_D = 1;
//         float CurrentBodyY = transform.position.y;
// //        Debug.Log("BodyY " + CurrentBodyY);
//         PIDControl.PositionYPID(targetY, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
//     }
// }
