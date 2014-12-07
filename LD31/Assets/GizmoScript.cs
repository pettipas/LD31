using UnityEngine;
using System.Collections;

public class GizmoScript : MonoBehaviour {



	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(transform.position,new Vector3(0.1f,0.1f,0.1f));
	}
}
