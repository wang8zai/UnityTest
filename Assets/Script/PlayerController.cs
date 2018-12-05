using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	protected KeyCode[] P1KeyCode = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.E, KeyCode.Q, KeyCode.Z, KeyCode.X};
	protected KeyCode[] P2KeyCode= {KeyCode.Keypad8, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad9, KeyCode.Keypad7, KeyCode.Keypad1, KeyCode.Keypad2};

	// Use this for initialization
	void Start () {
		
	}
	
	public KeyCode[] GetKeyCode(int index) {
		if(index == 0) return P1KeyCode;
		else if(index == 1) return P2KeyCode;
		else return new KeyCode[]{};
	}
}
