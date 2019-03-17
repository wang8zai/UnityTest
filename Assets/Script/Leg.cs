using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : body_part {

    protected GameObject gObj;
    protected Rigidbody2D rbody;
    protected float timeThread = 0.4f;
    protected float currentTime = 0.0f;
    protected float pounceForceThread = 1500.0f;
    protected float pounceForce;
    protected float pounceP = 0.1f;

    protected float MaxLegAngle = 60.0f;



    public void SetDirectionFlag() {
        float legAngle = gObj.transform.rotation.eulerAngles.z;
        legAngle = legAngle > 180.0f ? legAngle - 360 : legAngle;
        if(legAngle >  MaxLegAngle && DirectionChangedFlag) {
            DirectionFlag = !DirectionFlag;
            DirectionChangedFlag = false;
            rbody.angularVelocity = 0;
        }
        else if(legAngle < -MaxLegAngle &&　DirectionChangedFlag) {
            DirectionFlag = !DirectionFlag;   
            DirectionChangedFlag = false;
            rbody.angularVelocity = 0;
        }
        else{
            DirectionChangedFlag = true;
        }
    }

    public void SetGObj(GameObject gObj) {
        this.gObj = gObj;
        SetRd(gObj.GetComponent<Rigidbody2D>());
    }

    public void SetRd(Rigidbody2D rbody)
    {
        this.rbody = rbody;
    }
}
