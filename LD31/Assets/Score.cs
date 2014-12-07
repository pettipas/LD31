using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	public int score;
	public Text text;
	public Text livesText;
	public Text gameOver;
	public AudioSource scorePoints;
	public AudioSource loseLife;
	private int lives = 1;

	public bool gameisover;
	public bool popBubbles;

	public bool GameOver{
		get{
			return lives <=0;
		}
	}

	public void ScorePoints(ScoreType type, int points){
		if(type == ScoreType.win){
			scorePoints.Play();
			score += points;
		}else if(type == ScoreType.lose){
			score -= points;
			Failure();
		}else if(type == ScoreType.life){
			ExtraLife();
		}
	}

	public List<Creature> creatures = new List<Creature>();

	public void RegisterForPopping (Creature creature)
	{
		creatures.Add(creature);
	}

	public void ExtraLife() {
		lives++;
	}

	public void Failure() {
		lives--;
		loseLife.Play();
	}

	public void LosePoints(int points){
		score -= points;
	}

	public void GainPoints(int points){
		score -= points;
	}

	public void Update(){
		text.text = "SCORE " + score;
		livesText.text = "CHANCES " + lives;

		if(gameisover && Input.anyKey){
			Time.timeScale = 1;
			Application.LoadLevel("main");
		}
	}
	bool started;
	public IEnumerator TimeSlow(){
		popBubbles = true;
		yield return null;
		yield return StartCoroutine(PopAll());
		gameisover = true;
		gameOver.text = "G A M E O V E R";
		Time.timeScale = 0;
	}

	public IEnumerator PopAll(){
		int count = creatures.Count;
		for(int i = 0; i < count; i++){
			creatures[i].Pop();
			yield return new WaitForSeconds(0.08f);
		}
		Debug.Log ("done");
		yield return null;
	}

	public void LateUpdate(){
		if(GameOver && !started){
			started = true;
			StartCoroutine(TimeSlow());
		}
	}
}

public partial class Static {
	public Score score;
	public static Score Score {
		get { return instance.score; }
		set { instance.score = value; }
	}
}
