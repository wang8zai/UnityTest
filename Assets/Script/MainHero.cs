using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : body_part
{
    private GameObject MainHeroObj = null;

//    public GameObject Body;
    public GameObject LeftLeg;
    public GameObject RightLeg;
    public GameObject RightArm;
    public GameObject LeftArm;
    public GameObject Head;

    private Vector2 LeftLegVector;
    private Vector2 RightLegVector;
    private Vector2 InterLegVector;
    private float yheight = 20.0f;

    private float BodyAngle = 10.0f;

    private bool LegFlag = true;

    private float StandAngleThreshold = 0.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
        }
    }

    public void SetBodyPartsMass(int[] weights) {
        SetMass(weights[1]);
        LeftLeg.gameObject.GetComponent<LeftLeg>().SetMass(weights[3]);       
        RightLeg.gameObject.GetComponent<RightLeg>().SetMass(weights[3]); 
        LeftArm.gameObject.GetComponent<LeftArm>().SetMass(weights[2]);
        RightArm.GetComponent<RightArm>().SetMass(weights[2]);
    }

    public void SetBodyGravityScale(int scale) {
        SetGravityScale(scale);
        LeftLeg.GetComponent<LeftLeg>().SetGravityScale(scale);
        RightLeg.GetComponent<RightLeg>().SetGravityScale(scale);
        LeftArm.GetComponent<LeftArm>().SetGravityScale(scale);
        RightArm.GetComponent<RightArm>().SetGravityScale(scale);
//        Head.GetComponent<Head>().SetGravityScale(scale);
    }

    public void InitHero(GameObject obj) {
        if(MainHeroObj == null) {
            MainHeroObj = Instantiate<GameObject>(obj);
        }
    }

    public GameObject Get() {
        return MainHeroObj;
    }

    private void Update(){
    }

    private void FixedUpdate(){
        if (Input.GetKey(KeyCode.Z))
        {
            // float PID_P = 10.0f;
            // float PID_I = 150.0f;
            // float PID_D = 1;
            // LeftLeg.GetComponent<LeftLeg>().AnglePID(+90);
            // RightLeg.GetComponent<RightLeg>().AnglePID(-90);
            // PIDControl.PID(+90.0f, LeftLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
            // PIDControl.PID(-90.0f, RightLeg.GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
        }

//      Move back/forward
//        Debug.Log(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") != 0.0f){
            // LeftLeg.GetComponent<LeftLeg>().SetPounce(true);
            // RightLeg.GetComponent<RightLeg>().SetPounce(true);
            if (Input.GetAxis("Horizontal") > 0){
                AnglePID(-BodyAngle);
            }
            else{
                AnglePID(BodyAngle);
            }

//            PostionY(1.3f);
            // float LeftLegAngles = LeftLeg.transform.rotation.eulerAngles.z;
            // LeftLegAngles = LeftLegAngles > 180.0f ? LeftLegAngles - 360 : LeftLegAngles;
            // float RightLegAngles = RightLeg.transform.rotation.eulerAngles.z;
            // Debug.Log("Left Leg Angel is " + LeftLegAngles);
            // Debug.Log("Right Leg Angel is " + RightLegAngles);
            LeftLeg.GetComponent<LeftLeg>().Walkflag = true;
            RightLeg.GetComponent<RightLeg>().Walkflag = true;


            // if (LeftLegAngles < -MaxLegAngle)
            // {
            //     LegFlag = false;
            // }
            // else if (LeftLegAngles > MaxLegAngle)
            // {
            //     LegFlag = true;
            // }
        }
        //      Stand by
        else
        {
            LeftLeg.GetComponent<LeftLeg>().Walkflag = false;
            RightLeg.GetComponent<RightLeg>().Walkflag = false;
            // LeftLeg.GetComponent<LeftLeg>().SetPounce(false);
            // RightLeg.GetComponent<RightLeg>().SetPounce(false);
            AnglePID(0.0f);
            // LeftLeg.GetComponent<LeftLeg>().AnglePID(0);
            // RightLeg.GetComponent<RightLeg>().AnglePID(0);
        }

//      Sit down




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
    }

    private void PostionY(float targetY)
    {
        float PID_P = 2250.0f;
        float PID_I = 9250.0f;
        float PID_D = 1;
        float CurrentBodyY = transform.position.y;
//        Debug.Log("BodyY " + CurrentBodyY);
        PIDControl.PositionYPID(targetY, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
    }
}
