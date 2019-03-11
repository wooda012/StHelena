using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNoise : MonoBehaviour
{

	public AudioSource sound;
	public int wait;
	private int played;
	public bool disableOnPlay;
	private int enable;

	void Start(){
		sound = GetComponent<AudioSource>();
		enable = 0;
	}

	void Update(){
		if(played > 0){
			played--;
		}
	}
	void OnTriggerEnter (Collider other){
		if(played == enable){
		sound.Play();
		played = wait;
		}
		if(disableOnPlay){
			enable = -100;
		}
	}
}
