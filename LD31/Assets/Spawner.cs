using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	public IEnumerator Start(){
		yield return new WaitForSeconds(Random.Range(1,15));
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Creature>().dir = transform.forward;
		StartCoroutine(Spawn());
	}

	public IEnumerator Spawn(){
		yield return new WaitForSeconds(Random.Range(6,15));
		GameObject creature = (GameObject)Instantiate(prefab,transform.position,Quaternion.identity);
		creature.GetComponent<Creature>().dir = transform.forward;
		StartCoroutine(Spawn());
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
}
