using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {


	public List<GameObject> goodOnes = new List<GameObject>();
	public List<GameObject> badOnes = new List<GameObject>();

	public IEnumerator Start(){
		yield return new WaitForSeconds(Random.Range(1,15));
		GameObject creature = (GameObject)Instantiate(Choose(),transform.position,Quaternion.identity);
		creature.GetComponent<Creature>().dir = transform.forward;
		StartCoroutine(Spawn());
	}

	public IEnumerator Spawn(){
		yield return new WaitForSeconds(Random.Range(6,15));
		GameObject creature = (GameObject)Instantiate(Choose(),transform.position,Quaternion.identity);
		creature.GetComponent<Creature>().dir = transform.forward;
		StartCoroutine(Spawn());
	}

	public GameObject Choose(){
		float val = Random.value;

		if(val < 0.95f){
			return goodOnes.GetRandomElement();
		}else {
			return badOnes.GetRandomElement();
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,1.0f);
	}
}

public static class Extensions {
	
	private static System.Random random = new System.Random();
	
	public static T GetRandomElement<T>(this IEnumerable<T> list) {
		if (list.Count() == 0)
			return default(T);
		return list.ElementAt(random.Next(list.Count()));
	}
}
