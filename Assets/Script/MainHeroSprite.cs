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

	private float PlayerBottom = 0;

	private float moveVelocity = 5.0f;
	private Vector3 refvelocity = Vector3.zero;
	private Vector3 connectPoint = Vector3.zero;

	private bool facingRight = true;
	private bool holdFlag = false;
	private bool catchFlag = false; // if the character wants to catch the item or not. If he wants, the item will be detected by raycast 2D.
	private bool ForceRunFlag = false;
	private int jumpState = 0;
	private HingeJoint2D hingeJointToItem = null;

	private bool isGrounded = false;
	private bool isItemGrounded = false;
	private bool FallDownFlag = false;
	public LayerMask groundLayer;
	public LayerMask ItemLayer;

	private int collisionInt = 0;

	private KeyCode CUp;
	private KeyCode CDown;
	private KeyCode CLeft;
	private KeyCode CRight; 
	private KeyCode CSquat;
	private KeyCode CDrop;
	private KeyCode CRun;
	private KeyCode CJump;

	protected List<bool> npcStateList = new List<bool>();
	protected List<KeyCode> keyCodeList = new List<KeyCode>();
	enum getType {getK, getKD};
	enum keycodes {CUp = 0, CLeft, CDown, CRight, CSquat, CDrop, CRun, CJump};

	public MainHeroSprite() {
		// SetUpController();
	}

	// Use this for initialization
	void Start () {
		HeroAllAnimator = GetComponent<Animator>();
		rd2D = GetComponent<Rigidbody2D>();
		PlayerBottom = GetComponent<CircleCollider2D>().offset.y - GetComponent<CircleCollider2D>().radius;
		SetUpController();
		HeroAllAnimator.SetBool("holdFlag", false);
	}

	void SetUpController() {
		PController = new PlayerController();
		if(PlayerIndex < 10){
			KeyCode[] KC = PController.GetKeyCode(PlayerIndex);
			for(int i = (int)keycodes.CUp; i <= (int)keycodes.CJump; i++) {
				keyCodeList.Add(KC[i]);
				Debug.Log("init");
				Debug.Log(KC[i]);
				Debug.Log(i);
			}
		}
		else{
			for(int i = 0; i < PController.GetLength(); i++) {
				npcStateList.Add(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateStates();
		SetNewStates();
	}

	void FixedUpdate() {
		UpdateMove();
		UpdateCatch();
		UpdateJump();
	}

	private void UpdateMove() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -3), Mathf.Infinity, groundLayer);
		// if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) {
		// 	Debug.Log(hit.normal);
		// }
		Vector3 targetVelocity = new Vector2(moveVelocity * LowerState * (facingRight==true?1:-1), rd2D.velocity.y);
		if(collisionInt == 0) {
			rd2D.velocity = Vector3.SmoothDamp(rd2D.velocity, targetVelocity, ref refvelocity, 0.1f);
		}
		else {
			collisionInt = collisionInt - 1;
		}
	}

	private void UpdateCatch() {
		Transform LeftHandTransform = transform.Find("Upper/LeftArmUpper/LeftArmLower/LeftHand");
		if(catchFlag && !holdFlag){
			Vector2 localpos = LeftHandTransform.position;
			Debug.Log(localpos);
			RaycastHit2D hit = Physics2D.Raycast(localpos, new Vector2(0, 1),Mathf.Infinity,ItemLayer);
			if (hit.collider != null)
			{
				hingeJointToItem = hit.collider.gameObject.AddComponent<HingeJoint2D>();
				hingeJointToItem.autoConfigureConnectedAnchor = false;
             	hingeJointToItem.connectedBody = GetComponent<Rigidbody2D>();
				connectPoint = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);
				hingeJointToItem.anchor = connectPoint;
				holdFlag = true;
			}
		}

		if(holdFlag) {
			hingeJointToItem.connectedAnchor = transform.InverseTransformPoint(LeftHandTransform.position);
			// LeftHandTransform.GetComponent<HingeJoint2D>().anchor = transform.InverseTransformPoint(LeftHandTransform.position);
		}
	}

	private void UpdateJump() {
		if(isGrounded || isItemGrounded) {
			jumpState = 0;
		}
		if(rd2D.velocity.y < 0.0f && (!isItemGrounded && !isGrounded)) {
			jumpState = 2;
		}
		if(InputTrigger((int)keycodes.CJump, (int)getType.getKD) && (isGrounded || isItemGrounded)) {
			rd2D.velocity = new Vector2(rd2D.velocity.x, 20.0f);
			jumpState = 1;
		}
		HeroAllAnimator.SetInteger("JumpState", jumpState);
		BoxCollider2D colliderA = GameObject.Find("Item").GetComponent<BoxCollider2D>();
		BoxCollider2D colliderB = GetComponent<BoxCollider2D>();
		CircleCollider2D colliderC = GetComponent<CircleCollider2D>();
		Physics2D.IgnoreCollision(colliderA, colliderB, jumpState == 1 || (isGrounded && jumpState == 0));
		Physics2D.IgnoreCollision(colliderA, colliderC, jumpState == 1 || (isGrounded && jumpState == 0));
	}

	private void UpdateStates() {
		UpdateGroundStates();
		UpdateUpperStates();
		UpdateLowerStates();
	}

	private void UpdateGroundStates() {
		isGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom), groundLayer);
		isItemGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom), ItemLayer);
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
		if(InputTrigger((int)keycodes.CRight, (int)getType.getK)) {
			if(!facingRight) {
				Flip();
				HeroAllAnimator.SetInteger("WalkState", 0);
				LowerState = 0;
			}
			else {
				HeroAllAnimator.SetInteger("WalkState", 10);
				LowerState = 1;
				if(InputTrigger((int)keycodes.CRun, (int)getType.getK)) {
					HeroAllAnimator.SetInteger("WalkState", 15);
					LowerState = 3;
				}
			}
		}
		else if(InputTrigger((int)keycodes.CLeft, (int)getType.getK)) {
			if(facingRight) {
				Flip();
				HeroAllAnimator.SetInteger("WalkState", 0);
				LowerState = 0;
			}
			else {
				HeroAllAnimator.SetInteger("WalkState", 10);	
				LowerState = 1;		
				if(InputTrigger((int)keycodes.CRun, (int)getType.getK)) {
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

	private void SetUpperState() {
		if(UpperState == 0){
			if(InputTrigger((int)keycodes.CUp, (int)getType.getK)){
				HeroAllAnimator.SetInteger("LiftState", 10);
			// }
			// else if (!Input.GetKey(CUp) && !Input.GetKey(CDown)){
			// 	HeroAllAnimator.SetInteger("LiftState", 0);
			}
			else{
			}		
		}
		else if(UpperState == 1) {
			if(InputTrigger((int)keycodes.CUp, (int)getType.getK)){
				HeroAllAnimator.SetInteger("LiftState", 11);
			}
			else if(InputTrigger((int)keycodes.CDown, (int)getType.getK)){
				HeroAllAnimator.SetInteger("LiftState", 0);
			}
			else{
				// HeroAllAnimator.SetInteger("LiftState", 10);
			}	
		}
		else if(UpperState == 2) {
			if(InputTrigger((int)keycodes.CUp, (int)getType.getK)){
			}
			else if(InputTrigger((int)keycodes.CDown, (int)getType.getK)){
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
		if(InputTrigger((int)keycodes.CSquat, (int)getType.getKD)) {
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

		if(Input.GetKeyDown(CDrop)) {
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
		facingRight = !facingRight;
 		transform.localScale = new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	public void SetCollisionInt(int i) {
		collisionInt = collisionInt + i;
	}

	public bool InputTrigger(int kc, int b) {
		if(PlayerIndex < 10) {
			if(b == (int)getType.getKD) return Input.GetKeyDown(keyCodeList[kc]);
			return Input.GetKey(keyCodeList[kc]);
		}
		else {
			if(b == (int)getType.getKD) {
				bool rtn = npcStateList[kc];
				npcStateList[kc] = false;
				return rtn;
			}
			return npcStateList[kc];
		}
	}

	public void SetTrigger(int kc, bool t) {
		npcStateList[kc] = t;
	}
}
