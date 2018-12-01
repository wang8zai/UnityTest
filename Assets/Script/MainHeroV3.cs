// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MainHeroV3 : MonoBehaviour {

//     private Rigidbody2D rgbd;

// 	// Use this for initialization
// 	void Start () {
//         rgbd = GetComponent<Rigidbody2D>();
// //        rgbd = (Rigidbody2D)GetComponent("MainHero");
//     }
	
// 	// Update is called once per frame
// 	void Update () {
//         if(Input.GetAxis("Horizontal") > 0 ){
//  //           rgbd.velocity = Vector2.zero;
//             rgbd.AddForce(new Vector2(100.0f, 0.0f));
//         }
//         else if (Input.GetAxis("Horizontal") < 0)
//         {
// //            rgbd.velocity = Vector2.zero;
//             rgbd.AddForce(new Vector2(-100.0f, 0.0f));
//         }
//         else
//             rgbd.velocity = Vector2.zero;
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         rgbd.AddForce(new Vector2(0.0f, 0.0f));
//     }
// }
