﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
	public int PlayerIndex = 10;
	private Animator HeroAllAnimator;
	protected PlayerController PController;
	private Rigidbody2D rd2D;

	private int UpperState = 0;
	private int LowerState = 0;

	private float PlayerBottom = 0;

	private float moveVelocity = 1.0f;
	private Vector3 refvelocity = Vector3.zero;
	private Vector3 connectPoint = Vector3.zero;

	private int facingRight = 1;
	private bool holdFlag = false;
	private bool catchFlag = false; // if the character wants to catch the item or not. If he wants, the item will be detected by raycast 2D.
	private bool ForceRunFlag = false;
	private int jumpState = 0;
	private bool disablePID = false;

	private SpringJoint2D hingeJointToItem = null;

	protected bool isGrounded = false;
	protected bool isItemGrounded = false;

	private LayerMask groundLayer;
	private LayerMask ItemLayer;
	private LayerMask UnTouchLayer;

	private int collisionInt = 0;

	private int speedValue = 0;
	private int walkValve = 8;
	private int speedValve = 10;

	public BaseCharacter() {
		// SetUpController();
	}

	// Use this for initialization
	protected void Start () {
		HeroAllAnimator = GetComponent<Animator>();
		rd2D = GetComponent<Rigidbody2D>();
		PlayerBottom = GetComponent<CircleCollider2D>().offset.y - GetComponent<CircleCollider2D>().radius;
		SetUpController();
		groundLayer = LayerMask.GetMask("Ground");
		ItemLayer = LayerMask.GetMask("Item");
		UnTouchLayer = LayerMask.GetMask("UntouchItem");
		HeroAllAnimator.SetBool("holdFlag", false);
		EventManager.Instance.registerEvent(Enums.Event.PlayerEventTest, PlayerEventTest);
	}

	public void PlayerEventTest() {
		Debug.Log("Player Event Test");
	}
 
	public void SetCharacterAsPlayer(int index) {
		PlayerIndex = index;
		SetUpController();
	}

	public void SetCharacterAsNpc() {
		PlayerIndex = 10;
		SetUpController();
	}

	void SetUpController() {
		PController = new PlayerController(PlayerIndex);
	}
	
	// Update is called once per frame
	protected void  Update () {
		if(GameManager.Instance != null){
			PController.Update();
			UpdateStates();
			SetNewStates();
		}
	}

	void FixedUpdate() {
		if(GameManager.Instance != null) {

			UpdateCatch();
			UpdateJump();
			UpdateMove();
		}
	}

	private void UpdateMove() {
		// RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -3), Mathf.Infinity, groundLayer);
		// if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) {
		// 	Debug.Log(hit.normal);
		// }
		// Vector3 targetVelocity = new Vector2(moveVelocity * speedValue * LowerState, rd2D.velocity.y);
		if(collisionInt == 0) {
			if(!disablePID) {
				PIDControl.SpeedXPID(speedValue, rd2D, 3000.0f, 30.0f, 10.0f);		
			}
			// rd2D.velocity = Vector3.SmoothDamp(rd2D.velocity, targetVelocity, ref refvelocity, 0.1f);
		}
		else {
			collisionInt = collisionInt - 1;
			PIDControl.SpeedXPID(speedValue - speedValve, rd2D, 3000.0f, 30.0f, 10.0f);	
		}
	}

	private void UpdateCatch() {
		// update catch using animation end //
		Transform LeftHandTransform = transform.Find("Upper/LeftArmUpper/LeftArmLower/LeftHand");
		if(PlayerIndex < 10 && !ItemManager.Instance.isEmpty() && !holdFlag) {
			holdFlag = true;
			hingeJointToItem = ItemManager.Instance.Get(0).AddComponent<SpringJoint2D>();
			hingeJointToItem.autoConfigureConnectedAnchor = false;
			hingeJointToItem.autoConfigureDistance = false;
			hingeJointToItem.connectedBody = GetComponent<Rigidbody2D>();
			hingeJointToItem.distance = 0;
			hingeJointToItem.frequency = 5;
			hingeJointToItem.dampingRatio = 0.5f;
			connectPoint = new Vector3(-2, 0, 0);
			if(PlayerIndex == 1){
				connectPoint = new Vector3(2, 0, 0);
			}
			hingeJointToItem.anchor = connectPoint;
		}
		if(holdFlag) {
			hingeJointToItem.connectedAnchor = new Vector2(0, transform.InverseTransformPoint(LeftHandTransform.position).y);
		}
	}

	private void UpdateJump() {
		if(isGrounded || (isItemGrounded && !holdFlag)) {
			jumpState = 0;
			disablePID = false;
		}
		else {
			disablePID = true;
		}
		if(rd2D.velocity.y < 0.0f && (!isItemGrounded && !isGrounded)) {
			jumpState = 0;
		}
		if(PController.InputTrigger((int)Enums.keycodes.CJump, (int)Enums.getType.getKD) && (isGrounded || isItemGrounded)) {
			rd2D.velocity = new Vector2(rd2D.velocity.x, 20.0f);
			jumpState = 1;
		}
		HeroAllAnimator.SetInteger("JumpState", jumpState);

		if(!ItemManager.Instance.isEmpty()){
			PolygonCollider2D colliderA = ItemManager.Instance.GetStandCollider(0).GetComponent<PolygonCollider2D>();
			BoxCollider2D colliderD = ItemManager.Instance.Get(0).GetComponent<BoxCollider2D>();
			
			BoxCollider2D colliderB = GetComponent<BoxCollider2D>();
			CircleCollider2D colliderC = GetComponent<CircleCollider2D>();
			
			bool IgnoreBool = jumpState == 1 || (isGrounded && jumpState == 0);

			Physics2D.IgnoreCollision(colliderA, colliderB, IgnoreBool);
			Physics2D.IgnoreCollision(colliderA, colliderC, IgnoreBool);

			Physics2D.IgnoreCollision(colliderD, colliderB, true);
			Physics2D.IgnoreCollision(colliderD, colliderC, true);
		}
	}

	private void UpdateStates() {
		UpdateGroundStates();
		// UpdateUpperStates();
		// UpdateLowerStates();
		UpdateIntersect();
		UpdateController();
	}

	private void UpdateGroundStates() {
		isGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom - 0.05f), groundLayer);
		isItemGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom - 0.05f), UnTouchLayer);
	}

	private void UpdateIntersect() {
		if(PlayerIndex < 10) {
			foreach(GameObject obj in CharacterManager.Instance.GetNpcList()) {
				if(gameObject.GetComponent<BoxCollider2D>().bounds.Intersects(obj.GetComponent<BoxCollider2D>().bounds)) {
					if(PController.InputTrigger((int)Enums.keycodes.CDrop, (int)Enums.getType.getKD)){
						EventManager.Instance.TriggerEvent(Enums.Event.NpcIntersect);
					}
				}
			}
		}
	}

	private void UpdateController() {
		if(PController.InputTrigger((int)Enums.keycodes.CRight, (int)Enums.getType.getK)) {
			speedValue = speedValue + 1;
		}
		else if (PController.InputTrigger((int)Enums.keycodes.CLeft, (int)Enums.getType.getK)) {
			speedValue = speedValue - 1;
		}
		if(Mathf.Abs(speedValue) > speedValve) {
			if(speedValue < 0) speedValue = -speedValve;
			if(speedValue > 0) speedValue = speedValve;
		}
	}

	// private void UpdateUpperStates(){
	// 	if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfLiftMidHold")) {
	// 		UpperState = 1;
	// 	}
	// 	else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfLiftTopHold")){
	// 		UpperState = 2;
	// 	}
	// 	else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfIdle")){
	// 		UpperState = 0;
	// 	}
	// 	else {
	// 	}
	// }

	// private void UpdateLowerStates() {
	// 	if(holdFlag) {
	// 		if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfWalkHold")) {
	// 			LowerState = 1;
	// 		}
	// 		else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfRunHold")) {
	// 			LowerState = 2;
	// 		}
	// 		else if (HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfIdol")){
	// 			LowerState = 0;
	// 		}
	// 	}
	// 	else {
	// 		if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfWalkNormal")) {
	// 			LowerState = 1;
	// 		}
	// 		else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfRunNormal")) {
	// 			LowerState = 2;
	// 		}
	// 		else if (HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfIdol")){
	// 			LowerState = 0;
	// 		}
	// 	}
	// }



	private void SetNewStates() {
		SetUpperState();
		SetLowerState();
		// SetSquatState();
	}

	private void SetLowerState() {
		if(speedValue == 0) {
			HeroAllAnimator.SetInteger("WalkState", 0);
			LowerState = 0;
		}
		else if(speedValue != 0) {
			if(Mathf.Abs(speedValue) < walkValve) {
				HeroAllAnimator.SetInteger("WalkState", 10);
				LowerState = 1;
			}
			else {
				HeroAllAnimator.SetInteger("WalkState", 15);
				LowerState = 1;
			}
		}
		if(speedValue * facingRight < 0) {
			Flip();
		}
		// if((!facingRight && PController.InputTrigger((int)Enums.keycodes.CRight, (int)Enums.getType.getKD)) || (facingRight && PController.InputTrigger((int)Enums.keycodes.CLeft, (int)Enums.getType.getKD))) {
		// 	Flip();
		// 	HeroAllAnimator.SetInteger("WalkState", 0);
		// 	LowerState = 0;		
		// }
		// else if((facingRight && PController.InputTrigger((int)Enums.keycodes.CRight, (int)Enums.getType.getKD)) || (!facingRight && PController.InputTrigger((int)Enums.keycodes.CLeft, (int)Enums.getType.getKD))) {
		// 	HeroAllAnimator.SetInteger("WalkState", 10);
		// 	LowerState = 1;			
		// }
		// if(LowerState == 1 && PController.InputTrigger((int)Enums.keycodes.CRun, (int)Enums.getType.getKD)) {
		// 	HeroAllAnimator.SetInteger("WalkState", 15);
		// 	LowerState = 3;
		// }
	}

	private void SetUpperState() {
		if(UpperState == 0){
			if(PController.InputTrigger((int)Enums.keycodes.CUp, (int)Enums.getType.getKD)){
				HeroAllAnimator.SetInteger("LiftState", 10);
				UpperState = 1;
			}
			else{
			}		
		}
		else if(UpperState == 1) {
			if(PController.InputTrigger((int)Enums.keycodes.CUp, (int)Enums.getType.getKD)){
				HeroAllAnimator.SetInteger("LiftState", 11);
				UpperState = 2;
			}
			else if(PController.InputTrigger((int)Enums.keycodes.CDown, (int)Enums.getType.getKD)){
				HeroAllAnimator.SetInteger("LiftState", 0);
				UpperState = 0;
			}
			else{
			}	
		}
		else if(UpperState == 2) {
			if(PController.InputTrigger((int)Enums.keycodes.CUp, (int)Enums.getType.getKD)){
			}
			else if(PController.InputTrigger((int)Enums.keycodes.CDown, (int)Enums.getType.getKD)){
				HeroAllAnimator.SetInteger("LiftState", 10);
				UpperState = 1;
			}
			else{
			}	
		}
		else {
			Debug.Log("Unused state");
		}
	}

	private void SetSquatState() {
		if(PController.InputTrigger((int)Enums.keycodes.CSquat, (int)Enums.getType.getKD)) {
			if(holdFlag) {
				if(hingeJointToItem != null) {
					Destroy(hingeJointToItem);
					holdFlag = false;
				}
			}
			else{
				HeroAllAnimator.SetInteger("SquatState", -10);
			}
		}
		else {
			HeroAllAnimator.SetInteger("SquatState", 0);
		}

		if(PController.InputTrigger((int)Enums.keycodes.CDrop, (int)Enums.getType.getKD)) {
			ForceRunFlag = !ForceRunFlag;
		}
	}

	private void SetCatchFlag() {
		catchFlag = true;
	}

	private void ResetCatchFlag() {
		catchFlag = false;
	}

	private void Flip() {
		facingRight = -facingRight;
 		transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	public void SetCollisionInt(int i) {
		collisionInt = collisionInt + i;
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		string objname = coll.gameObject.name;
		objname = objname.Remove(3);
		if (objname == "Sc_")
		{	
			foreach (ContactPoint2D Hits in coll.contacts)
            {
                Vector2 hitPoint = Hits.point;
				GameObject hobj = coll.gameObject;
				Vector2 hitNormal = Hits.normal;
				Vector3 hited = hitPoint;	
				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(1000* hitNormal, hited);
				CharacterManager.Instance.Get(0).GetComponent<BaseCharacter>().SetCollisionInt(1);

				// gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-100* hitNormal, hited);
				coll.gameObject.GetComponent<sceneItem>().minusDuration(1);
            }
		}
	}
}
