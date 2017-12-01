using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingNote2 : MonoBehaviour {
	
	public GameObject manager;
	public GameObject stars;

	private AudioSource note;
	private int id;
	private Animator animator;
	private PlayerStates script;
	private ManagerScript managerScript;
	private Drop portrait;
	private bool isCorrect;

	private Animator animStars;

	void Start(){
		note = GetComponent<AudioSource> ();
		managerScript = manager.GetComponent<ManagerScript>();
		portrait = GetComponent<Drop> ();
		animStars = stars.GetComponent<Animator> ();
		isCorrect = false;
	}

	public void checkNote(){
		//play the note
			stars.SetActive (false);
			note.Play ();

			//check if is the correct note
			if (id == portrait.getId()) {
				//script.setIsHappy (true);
				isCorrect = true;
				stars.SetActive (true);
				animStars.SetBool ("IsCorrect",isCorrect);
				Debug.Log ("ACERTÔ MsISERAVI");
			} else {
				//script.setIsSad (true);
				isCorrect = false;
				stars.SetActive (false);
				animStars.SetBool ("IsCorrect",isCorrect);
				Debug.Log ("EROOOOOU");
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
