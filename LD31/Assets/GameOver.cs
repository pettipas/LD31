using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	void Update () {
		if(Input.anyKey){
			Application.LoadLevel("main");
		}
	}
}
