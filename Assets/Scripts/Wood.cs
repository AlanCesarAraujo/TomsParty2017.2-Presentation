using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour {

	public GameObject manager;
	private Mec_Three mec = new Mec_Three ();
	private AudioSource note;
	private Animator animator;

	void Start(){
		note = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		mec = manager.GetComponent<Mec_Three>();
	}

	//method called when some wood is clicked. cheks if it's the first oe second click
	public void clickWood(int wood){
		Mec_Three.actualWood = wood;
		animator.SetTrigger ("show");

		if (Mec_Three.firstClick) {
			mec.StartCoroutine ("turnFirst");
		} else {
			mec.StartCoroutine ("turnSecond");
		}
	}

	public void setNote(AudioClip clip){
		this.note.clip = clip;
	}

	public AudioSource getNote(){
		return this.note;
	}

	public void setAnimator(Animator an){
		this.animator = an;
	}

	public Animator getAnimator(){
		return this.animator;
	}


}
