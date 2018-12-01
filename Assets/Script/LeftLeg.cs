using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLeg : Leg {

	void Start () {
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Walkflag == true) {
            if (DirectionFlag == false)
            {
                gObj.GetComponent<LeftLeg>().AnglePID(75);
            }
            else
            {
                gObj.GetComponent<LeftLeg>().AnglePID(-75);
            }
            SetDirectionFlag();
        }
        else {
            gObj.GetComponent<LeftLeg>().AnglePID(0);
        }
        
        if(Walkflag) {
            if(PounceFlag == true){
                pounceForce = pounceForceThread;
                rbody.AddForce(new Vector2(0.0f, pounceForce));
                PounceFlag = false;
                currentTime += Time.deltaTime;
            }
            if(currentTime != 0 && currentTime < timeThread){
                pounceForce = pounceForce - pounceP * pounceForceThread >= 0 ? pounceForce - pounceP * pounceForceThread : 0;
                rbody.AddForce(new Vector2(0.0f, pounceForce));
                currentTime += Time.deltaTime;
            }
            else{
                currentTime = 0;
            }
        }
        // float PID_P = 2500.0f;
        // float PID_I = 200.0f;
        // float PID_D = 200.0f;
        // if (stopflag == false)
        // {
        //     if(directionflag == true)
        //         PIDControl.SpeedXPID(25.0f, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
        //     else
        //         PIDControl.SpeedXPID(-25.0f, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
        // }
        // else
        // {
        //     PIDControl.SpeedXPID(0.0f, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
        // }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Walkflag == true)
        {
            if (collision.gameObject.layer == 12)    //12 ground
            {
                PounceFlag = true;
            }
        }
        else PounceFlag = false;
    }
}
