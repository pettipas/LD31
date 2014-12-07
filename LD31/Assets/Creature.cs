using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public float speed = 1.0f;
	public float suckspeed = 2.0f;
	public Vector3 dir;
	public float getSuckedUpTime;
	public AudioSource popSound;
	public Transform target;
	bool running;
	public Transform bubble;

	public ScoreType scoreType;
	public int points;

	public void Awake(){
		float val = Random.Range(2,4.3f);
		if(bubble){
			bubble.localScale += new Vector3(val,val,val);
		}
	}

	public void Update(){
		if(target == null){
			transform.Translate(dir * speed * Time.deltaTime);
			return;
		}
		if(!running){
			running = true;
			StartCoroutine(GetSuckedUp(getSuckedUpTime));
		}
	}

	public IEnumerator GetSuckedUp(float duration){
		float dt = 1/duration;
		float t = 0;
		Destroy(bubble.gameObject);
		if(popSound) popSound.Play();
		while(t < 1){
			t+=dt*Time.smoothDeltaTime * suckspeed;
			transform.position = Vector3.Lerp(transform.position, target.position,t);
			yield return 1;
		}
		Static.Score.ScorePoints(scoreType,points);
		Destroy(gameObject);
	}
	
}

public enum ScoreType{
	win,
	lose
}
