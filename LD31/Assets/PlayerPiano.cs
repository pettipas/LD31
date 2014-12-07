using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPiano : MonoBehaviour {

	public List<Catcher> catchers = new List<Catcher>();

	public void RegisterCatcher(Catcher catcher){
		if(!catchers.Contains(catcher)){
			catchers.Add(catcher);
		}
	}

	public void DoStartTune(){
		if(!started){
			started = true;
			StartCoroutine(StartTune());
		}
	}

	public void DoEndTune(){
		if(!ended){
			ended = true;
			StartCoroutine(EndTune());
		}
	}

	bool started;
	public IEnumerator StartTune(){
		for(int i = 0; i < Random.Range(6,11); i++){
			catchers.GetRandomElement().Activate();
			yield return new WaitForSeconds(Random.Range(0.05f,0.08f));
		}
	}
	bool ended;
	public IEnumerator EndTune(){
		for(int i = 0; i < catchers.Count; i++){
			catchers.GetRandomElement().Activate();
			yield return new WaitForSeconds(Random.Range(0.01f,0.02f));
		}
	}

	public void Update(){
		DoStartTune();
	}
}

public partial class Static {
	public PlayerPiano playerpiano;
	public static PlayerPiano PlayerPiano {
		get { return instance.playerpiano; }
		set { instance.playerpiano = value; }
	}
}
