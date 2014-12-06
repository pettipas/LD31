using UnityEngine;
using System.Collections;

public class Tumble : MonoBehaviour {

	void Start () {
		float xSpin = Random.Range(0,360);
		float ySpin = Random.Range(0,360);
		float zSpin = Random.Range(0,360);
		transform.rotation=Quaternion.Euler(xSpin, ySpin,zSpin );
	}
	
	void Update(){
		transform.Rotate(Vector3.up,Time.deltaTime * Random.Range(10,20));
	}
}
