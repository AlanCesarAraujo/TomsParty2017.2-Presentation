using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingNote : MonoBehaviour {

	public GameObject player;
	public GameObject manager;
	
	private AudioSource note;
	private int id;
	private Animator animator;
	private PlayerStates script;
	private ManagerScript managerScript;
	private bool isCorrect;

	void Start(){
		note = GetComponent<AudioSource> ();
		script = player.GetComponent<PlayerStates> ();
		managerScript = manager.GetComponent<ManagerScript>();
		isCorrect = false;
	}

	public void checkNote(){
		//play the note
		if (managerScript.getIsPlayable() == true) {
			note.Play ();

			//check if is the correct note
			if (id == 0) {
				script.setIsHappy (true);
				isCorrect = true;
				//Debug.Log ("ACERTÔ MsISERAVI");
			} else {
				script.setIsSad (true);
				isCorrect = false;
				Debug.Log ("EROOOOOU");
			}
		}
	}

	public void Play(){
		//play the note
		note.Play();
	}

	public void setNote(AudioClip clip){
		this.note.clip = clip;
	}

	public void setId(int id){
		this.id = id;
	}

	public int getId(){
		return this.id;
	}

	public void setCorrect(bool isCorrect){
		this.isCorrect = isCorrect;
	}

	public bool getCorrect(){
		return this.isCorrect;
	}
	public void setAnimator(Animator an){
		this.animator = an;
	}

	public Animator getAnimator(){
		return this.animator;
	}


}
