using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	protected KeyCode[] P1KeyCode = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y};
	protected KeyCode[] P2KeyCode= {KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4};

	// Use this for initialization
	void Start () {
		
	}
	
	public KeyCode[] GetKeyCode(int index) {
		if(index == 0) return P1KeyCode;
		else if(index == 1) return P2KeyCode;
		else return new KeyCode[]{};
	}

	public int GetLength() {
		return P1KeyCode.Length;
	}
}
