using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PIDControl{

    public static void PID(float target, GameObject gObj, float PID_P, float PID_I, float PID_D, float deltaTime)
    {
        Rigidbody2D rd = gObj.GetComponent<Rigidbody2D>();
        float rotatespeed = rd.angularVelocity;
        float rotateposition = gObj.transform.rotation.eulerAngles.z;
        // Debug.Log( "target angle is " + target);
        rotateposition = rotateposition > 180.0f ? rotateposition - 360.0f : rotateposition;
        // Debug.Log("Current Angle is " + rotateposition);
        float diffpostion = rotateposition - target;
        // Debug.Log("Diff position " + diffpostion );
        float rotateforce = diffpostion * PID_P + rotatespeed * PID_I;
        // Debug.Log("rotate force is " + rotateforce + " rotate speed " + rotatespeed);
        rd.AddTorque(-rotateforce * deltaTime);
    }

    public static void SpeedXPID(float target, Rigidbody2D rd, float PID_P, float PID_I, float PID_D)
    {
        float Xspeed = rd.velocity.x;
        Xspeed = Xspeed - target;
        Xspeed = -Xspeed;
        float Xspeedforce = Xspeed * PID_P;
//        Debug.Log("XSPEEDFORCE :" + Xspeedforce);
        rd.AddForce(new Vector3(Xspeedforce * Time.deltaTime, 0.0f, 0.0f));
    }

    public static void PositionYPID(float target_Y, Rigidbody2D rd, float PID_P, float PID_I, float PID_D)
    {
        float movespeed = rd.velocity.y;
        float movepostion = rd.transform.position.y;
        movepostion = movepostion - target_Y;
        float PostionYforce = movespeed * PID_P + movepostion * PID_I;
        rd.AddForce(new Vector3(0.0f, -PostionYforce * Time.deltaTime, 0.0f));
    }


    public static void PositionPID(float target, Rigidbody rd, float PID_P, float PID_I, float PID_D, float deltaTime)
    {
        Vector3 movespeed = rd.velocity;
        Vector3 movepostion = rd.transform.position;
    }
}
