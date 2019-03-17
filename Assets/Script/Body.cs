using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : body_part
{
    public int x;
    // Use this for initialization
    void Start()
    {
        mass = GetComponent<Rigidbody2D>().mass;
        // LeftLeg.GetComponent<Rigidbody2D>().mass = 1;
        // RightLeg.GetComponent<Rigidbody2D>().mass = 1;
        // LeftLeg.GetComponent<Rigidbody2D>().gravityScale = 5;
        // RightLeg.GetComponent<Rigidbody2D>().gravityScale = 5;
        // LeftArm.GetComponent<Rigidbody2D>().gravityScale = 5;
        // RightArm.GetComponent<Rigidbody2D>().gravityScale = 5;
        // Head.GetComponent<Rigidbody2D>().gravityScale = 5;
        // GetComponent<Rigidbody2D>().gravityScale = 5;
//        LeftLegVector = transform.position - LeftLeg.transform.position;
//        RightLegVector = transform.position - RightLeg.transform.position;
//        InterLegVector = (LeftLegVector + RightLegVector) / 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
        }
    }

    // public void StandBy(float angle)
    // {
    //     float PID_P = 400.0f;
    //     float PID_I = 1500.0f;
    //     float PID_D = 1;
    //     float CurrentBodyAngle = transform.rotation.eulerAngles.z;
    //     CurrentBodyAngle = CurrentBodyAngle > 180.0f ? CurrentBodyAngle - 360.0f : CurrentBodyAngle;
    //     PIDControl.PID(angle, GetComponent<Rigidbody2D>(), PID_P, PID_I, PID_D, Time.deltaTime);
    // }
}
