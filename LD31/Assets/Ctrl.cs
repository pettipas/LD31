using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

//big money control. winitron 1997
public class Ctrl : MonoBehaviour {

	public Dictionary<KeyCode,Button> buttons = new Dictionary<KeyCode, Button>();

	public void Awake(){
		foreach(Transform t in transform){
			Button b = t.GetComponent<Button>();
			if(b != null){
				buttons.Add(b.code,b);
			}
		}
	}

	void Update () {
		foreach (var code in Enum.GetValues(typeof(KeyCode))) {
			if(Input.GetKeyDown((KeyCode)code)) {
				if(buttons.ContainsKey((KeyCode)code)){
					buttons[(KeyCode)code].KeyDown();
				}
			}
			if(Input.GetKeyUp((KeyCode)code)) {
				if(buttons.ContainsKey((KeyCode)code)){
					buttons[(KeyCode)code].KeyUp();
				}
			}
		}
	}
}
