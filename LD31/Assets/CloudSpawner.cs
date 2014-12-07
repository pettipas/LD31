using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public GameObject prefab;
	public void Awake(){
		StartCoroutine(Spawn());
	}
	
	public IEnumerator Spawn(){
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Cloud>().dir = transform.forward;
		yield return new WaitForSeconds(Random.Range(10,20));
		StartCoroutine(Spawn());
	}
	
	public void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
}
