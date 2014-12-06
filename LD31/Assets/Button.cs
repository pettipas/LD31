using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Button : MonoBehaviour {

	public KeyCode code;

	public void Update(){
		name = code.ToString();
	}

	public void KeyDown(){
		transform.position+=new Vector3(0,1,0);
	}

	public void KeyUp(){
		transform.position+=new Vector3(0,-1,0);
	}
}
