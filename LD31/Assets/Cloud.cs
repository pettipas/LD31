using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public float speed = 1.0f;
	public Vector3 dir;

	public void Awake(){
		speed = Random.Range(0.2f,1.0f);
	}
	
	public void Update(){
		transform.Translate(dir * speed * Time.deltaTime);
	}
}
