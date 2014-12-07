using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public GameObject prefab;
	public void Awake(){
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Cloud>().dir = transform.forward;
		StartCoroutine(Spawn());
	}
	
	public IEnumerator Spawn(){
		yield return new WaitForSeconds(Random.Range(100,200));
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Cloud>().dir = transform.forward;
		StartCoroutine(Spawn());
	}
	
	public void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
}
