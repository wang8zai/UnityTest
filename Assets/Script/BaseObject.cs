using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour {
	private Rigidbody2D rd2D;

	protected bool isGrounded = false;
	protected bool isItemGrounded = false;

	private LayerMask groundLayer;
	private LayerMask ItemLayer;
	private LayerMask UnTouchLayer;

	public BaseObject() {
	}

	// Use this for initialization
	protected void Start () {
		rd2D = GetComponent<Rigidbody2D>();
		groundLayer = LayerMask.GetMask("Ground");
		ItemLayer = LayerMask.GetMask("Item");
		UnTouchLayer = LayerMask.GetMask("UntouchItem");
		EventManager.Instance.registerEvent(Enums.Event.PlayerEventTest, PlayerEventTest);
	}

	public void PlayerEventTest() {
		Debug.Log("Player Event Test");
	}

	// Update is called once per frame
	protected void  Update () {
		// UpdateGroundStates();
	}

	void FixedUpdate() {
	}

	private void UpdateGroundStates() {
		// isGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom - 0.05f), groundLayer);
		// isItemGrounded = Physics2D.OverlapArea(new Vector2(transform.localPosition.x - GetComponent<CircleCollider2D>().radius, transform.localPosition.y + PlayerBottom - 0.1f), new Vector2(transform.localPosition.x + GetComponent<CircleCollider2D>().radius,  transform.localPosition.y + PlayerBottom - 0.05f), UnTouchLayer);
	}

	public void SetCollisionInt(int i) {
		// collisionInt = collisionInt + i;
	}
}
