using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public float speed = 1.0f;
	public Vector3 dir;
	public void Update(){
		transform.Translate(dir * speed * Time.deltaTime);
	}
}
