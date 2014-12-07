using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public int score;
	public Text text;
	public AudioSource scorePoints;
	public int lives = 3;

	public void ScorePoints(ScoreType type, int points){
		if(type == ScoreType.win){
			scorePoints.Play();
			score += points;
		}else if(type == ScoreType.lose){
			score -= points;
		}
	}

	public void Failure() {
		lives--;
	}

	public void LosePoints(int points){
		score -= points;
	}

	public void GainPoints(int points){
		score -= points;
	}

	public void Update(){
		text.text = "SCORE " + score;
	}

	public void LateUpdate(){
		if(lives == 0){
			Application.LoadLevel("end");
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
