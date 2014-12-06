using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public KeyCode code;
	public Catcher catcher;
	public GameObject catcherPrefab;

	public void Awake(){
		GameObject go = (GameObject)Instantiate(catcherPrefab);
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		catcher = go.GetComponentInChildren<Catcher>();
	}

	public void Update(){
		name = code.ToString();
	}

	public void KeyDown(){
		catcher.Activate();
		transform.position+=new Vector3(0,1,0);
	}

	public void KeyUp(){
		transform.position+=new Vector3(0,-1,0);
	}
}
