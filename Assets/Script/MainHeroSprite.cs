using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroSprite : MonoBehaviour {
	public int PlayerIndex = 0;
	private Animator HeroAllAnimator;
	private PlayerController PController;
	private Rigidbody2D rd2D;
	private int UpperState = 0;
	private int LowerState = 0;
	private float moveVelocity = 5.0f;
	private Vector3 refvelocity = Vector3.zero;
	private bool facingRight = true;
	private bool holdFlag = false;

	private KeyCode CUp;
	private KeyCode CDown;
	private KeyCode CLeft;
	private KeyCode CRight; 
	private KeyCode CSquat;
	private KeyCode CDrop;
	private KeyCode CRun;
	// Use this for initialization
	void Start () {
		HeroAllAnimator = GetComponent<Animator>();
		rd2D = GetComponent<Rigidbody2D>();
		PController = new PlayerController();
		KeyCode[] KC = PController.GetKeyCode(PlayerIndex);
		CUp = KC[0];
		CDown = KC[2];
		CLeft = KC[1];
		CRight = KC[3];
		CSquat = KC[4];
		CDrop = KC[5];
		CRun = KC[6];

		HeroAllAnimator.SetBool("HoldFlag", false);
	}
	
	// Update is called once per frame
	void Update () {


		UpdateStates();
		SetNewStates();


	}

	void FixedUpdate() {
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveVelocity * LowerState * (facingRight==true?1:-1), rd2D.velocity.y);
		// And then smoothing it out and applying it to the character
		rd2D.velocity = Vector3.SmoothDamp(rd2D.velocity, targetVelocity, ref refvelocity, 0.03f);

	}

	private void UpdateStates() {
		UpdateUpperStates();
		UpdateLowerStates();
	}

	private void UpdateUpperStates(){
		if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfLiftMidHold")) {
			UpperState = 1;
		}
		else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfLiftTopHold")){
			UpperState = 2;
		}
		else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(2).IsName("UpperHalfIdle")){
			UpperState = 0;
		}
		else {
		}
	}

	private void UpdateLowerStates() {
		if(holdFlag) {
			if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfWalkHold")) {
				LowerState = 1;
			}
			else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfRunHold")) {
				LowerState = 2;
			}
			else if (HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfIdol")){
				LowerState = 0;
			}
		}
		else {
			if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfWalkNormal")) {
				LowerState = 1;
			}
			else if(HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfRunNormal")) {
				LowerState = 2;
			}
			else if (HeroAllAnimator.GetCurrentAnimatorStateInfo(1).IsName("LowerHalfIdol")){
				LowerState = 0;
			}
		}
	}

	private void SetNewStates() {
		SetUpperState();
		SetLowerState();
		SetSquatState();
	}

	private void SetLowerState() {
		if(holdFlag){
			if(LowerState == 0) {
				if(Input.GetKey(CRight)) {
					HeroAllAnimator.SetInteger("WalkState", 10);
					if(!facingRight) {
						Flip();
					}
				}
				else if(Input.GetKey(CLeft)){
					HeroAllAnimator.SetInteger("WalkState", 10);
					if(facingRight) {
						Flip();
					}
				}
				else {
					HeroAllAnimator.SetInteger("WalkState", 0);
				}
			}
			else if(LowerState == 1) {
				if(facingRight){
					if(Input.GetKey(CRight)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
					else if(Input.GetKey(CLeft)){
						HeroAllAnimator.SetInteger("WalkState", 0);
					}
					else {
						HeroAllAnimator.SetInteger("WalkState", 10);
					}
				}
				else {
					if(Input.GetKey(CLeft)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
					else if(Input.GetKey(CRight)){
						HeroAllAnimator.SetInteger("WalkState", 0);
					}
					else {
						HeroAllAnimator.SetInteger("WalkState", 10);
					}
				}
			}
			else if(LowerState == 2) {
				if(facingRight) {
					if(Input.GetKey(CRight)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
					else if(Input.GetKey(CLeft)){
						HeroAllAnimator.SetInteger("WalkState", 10);
					}
					else {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
				}
				else {
					if(Input.GetKey(CLeft)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
					else if(Input.GetKey(CRight)){
						HeroAllAnimator.SetInteger("WalkState", 10);
					}
					else {
						HeroAllAnimator.SetInteger("WalkState", 15);
					}
				}
			}
			else {
			}
		}
		else {
			if(Input.GetKey(CRight)) {
				if(!facingRight) {
					Flip();
					HeroAllAnimator.SetInteger("WalkState", 0);
					LowerState = 0;
				}
				else {
					HeroAllAnimator.SetInteger("WalkState", 10);
					LowerState = 1;
					if(Input.GetKey(CRun)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
						LowerState = 3;
					}
				}
			}
			else if(Input.GetKey(CLeft)) {
				if(facingRight) {
					Flip();
					HeroAllAnimator.SetInteger("WalkState", 0);
					LowerState = 0;
				}
				else {
					HeroAllAnimator.SetInteger("WalkState", 10);	
					LowerState = 1;		
					if(Input.GetKey(CRun)) {
						HeroAllAnimator.SetInteger("WalkState", 15);
						LowerState = 3;
					}	
				}
			}
			else {
				HeroAllAnimator.SetInteger("WalkState", 0);
				LowerState = 0;			
			}

		}
	}

	private void SetUpperState() {
		if(UpperState == 0){
			if(Input.GetKey(CUp))
				HeroAllAnimator.SetInteger("LiftState", 10);
			else if (!Input.GetKey(CUp) && !Input.GetKey(CDown)){
				HeroAllAnimator.SetInteger("LiftState", 0);
			}
			else{
			}		
		}
		else if(UpperState == 1) {
			if(Input.GetKey(CUp)){
				HeroAllAnimator.SetInteger("LiftState", 11);
			}
			else if(Input.GetKey(CDown)){
				HeroAllAnimator.SetInteger("LiftState", 0);
			}
			else{
				HeroAllAnimator.SetInteger("LiftState", 10);
			}	
		}
		else if(UpperState == 2) {
			if(Input.GetKey(CUp)){
			}
			else if(Input.GetKey(CDown)){
				HeroAllAnimator.SetInteger("LiftState", 10);
			}
			else{
			}	
		}
		else {
			Debug.Log("Unused state");
		}
	}

	private void SetSquatState() {
		if(Input.GetKey(CSquat)) {
			HeroAllAnimator.SetInteger("SquatState", -10);
		}
		else {
			HeroAllAnimator.SetInteger("SquatState", 0);
		}

		if(Input.GetKey(CDrop)) {
			transform.Find("Upper/LeftArmUpper/LeftArmLower/LeftHand").gameObject.GetComponent<CircleCollider2D>().enabled = false;
			transform.Find("Upper/RightArmUpper/RightArmLower/RightHand").gameObject.GetComponent<CircleCollider2D>().enabled = false;
		}
	}

	private void EnableHandCollider() {
		transform.Find("Upper/LeftArmUpper/LeftArmLower/LeftHand").gameObject.GetComponent<CircleCollider2D>().enabled = true;
		transform.Find("Upper/RightArmUpper/RightArmLower/RightHand").gameObject.GetComponent<CircleCollider2D>().enabled = true;	
	}

	private void Flip() {
		facingRight = !facingRight;
 		transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
}
