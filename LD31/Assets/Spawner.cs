using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	public void Awake(){
		StartCoroutine(Spawn());
	}

	public IEnumerator Spawn(){
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Creature>().dir = transform.forward;
		yield return new WaitForSeconds(Random.Range(6,8));
		StartCoroutine(Spawn());
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
}
