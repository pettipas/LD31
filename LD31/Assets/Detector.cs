using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {



	public void OnTriggerEnter(Collider other){

		Creature creature = other.GetComponent<Creature>();
		if(creature){
			if(creature.scoreType == ScoreType.win){
				Static.Score.Failure();
				Static.Score.LosePoints(creature.points);
			}else if(creature.scoreType == ScoreType.lose){
				Static.Score.GainPoints(creature.points);
			}
			Destroy(creature.gameObject);
		}


	}
}
