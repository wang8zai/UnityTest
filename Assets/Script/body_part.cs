using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_part : MonoBehaviour
{
    public float gravityScale;
    public float mass;
    public float PID_P_Factor = 1;
    public float PID_I_Factor = 1;
    public float PID_D_Factor = 1;

    private bool walkflag = false;
    public bool Walkflag
    {
        get
        {
            return walkflag;
        }
        set
        {
            walkflag = value;
        }
    }
    private bool pounceFlag = false;
    public bool PounceFlag{
        get {
            return pounceFlag;
        }
        set {
            pounceFlag = value;
        }
    }

    private bool directionFlag = false;
    public bool DirectionFlag{
        get {
            return directionFlag;
        }
        set {
            directionFlag = value;
        }
    }

    private bool directionChangedFlag = true;
    public bool DirectionChangedFlag{
        get {
            return directionChangedFlag;
        }
        set {
            directionChangedFlag = value;
        }
    }

    public void SetMass(int inputmass) {
        GetComponent<Rigidbody2D>().mass = inputmass;
        mass = inputmass;
    }
    public void SetGravityScale(float inputGravityScale) {
        GetComponent<Rigidbody2D>().gravityScale = inputGravityScale;
        gravityScale = inputGravityScale;
    }
    public void AnglePID(float angle)
    {
        float PID_P = gravityScale * mass * PID_P_Factor;
        float PID_I = gravityScale * mass * PID_I_Factor;
        float PID_D = gravityScale * mass * PID_D_Factor;
        PIDControl.PID(angle, gameObject, PID_P, PID_I, PID_D, Time.deltaTime);
    }

    public void SetPID(int[] input) {
        PID_P_Factor = input[0];
        PID_I_Factor = input[1];
        PID_D_Factor = input[2];
    }
}