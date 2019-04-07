using System;
using System.Collections;
using System.Collections.Generic;
// using System.Collections.Enum;
using UnityEngine;

public class PlayerController : ScriptableObject {

	protected KeyCode[] P1KeyCode = {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y};
	protected KeyCode[] P2KeyCode= {KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4};

	protected bool[,] stateList = new bool[Enum.GetNames(typeof(Enums.keycodes)).Length, Enum.GetNames(typeof(Enums.getType)).Length];               // input states map
	protected List<KeyCode> keyCodeList;       // state machine. read state from here.


	private int playerIndex = -1;
	private bool isPlayer = false;

	public PlayerController(int playerIndex) {
		this.playerIndex = playerIndex;
		if(playerIndex < 10) {
			isPlayer = true;
			keyCodeList = new List<KeyCode>();
		}
		Debug.Log(this.playerIndex);
		KeyCode[] KC = GetKeyCode();
		foreach(KeyCode kc in Enum.GetValues(typeof(Enums.keycodes))) {
			if(isPlayer)
				keyCodeList.Add(KC[(int)kc]);
		}
	}

	public void Update() {
		if(playerIndex < 10) {
			foreach(KeyCode kc in Enum.GetValues(typeof(Enums.keycodes))) {
				stateList[(int)kc, (int)Enums.getType.getKD] = Input.GetKeyDown(keyCodeList[(int)kc]);
				stateList[(int)kc, (int)Enums.getType.getK] = Input.GetKey(keyCodeList[(int)kc]);
			}
		}
	}

	public KeyCode[] GetKeyCode() {
		if(playerIndex == 0) return P1KeyCode;
		else if(playerIndex == 1) return P2KeyCode;
		else return new KeyCode[]{};
	}

	public int GetLength() {
		return P1KeyCode.Length;
	}

	public bool InputTrigger(int kc, int b) {
		return stateList[kc, b];
	}

	public void SetTrigger(int kc, bool t) {
		stateList[kc, (int)Enums.getType.getK] = t;
	}

}
