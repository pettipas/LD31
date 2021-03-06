﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Catcher : MonoBehaviour {

	public float radius;
	public AudioSource wrongButton;
	public ParticleSystem particles;
	public Transform target;
	public AudioSource suckingNoise;
	bool activated;

	public List<AudioClip> notes = new List<AudioClip>();

	public void Start(){
		Static.PlayerPiano.RegisterCatcher(this);
		suckingNoise.clip = notes.GetRandomElement();
		particles.enableEmission = false;
	}

	public IEnumerator DoEffect(){
		particles.enableEmission = true;
		suckingNoise.Play();
		yield return new WaitForSeconds(2.0f);
		particles.enableEmission = false;
		activated = false;
	}

	public void Update(){
		if(!activated){
			return;
		}

		Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
		colliders.ToList().ForEach(x=>{
			Creature creature = x.GetComponent<Creature>();
			if(creature != null){
				creature.target = target;
			}
		});
	}

	public void Activate(){
		if(!activated){
			activated = true;
			StartCoroutine(DoEffect());
		}else {
			if(wrongButton){
				wrongButton.Play();
			}
		}
	}



	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,radius);
	}
}
