using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mec_Zero : MonoBehaviour {

	//gameObject
	public GameObject objPlayer; //player
	public GameObject progressBar; //progressBar
	public GameObject victory;
	public GameObject nextUp;


	//audios
	//player
	private AudioSource playerAudioSource;

	//collisor
	Collider2D playerCollider;

	//animation
	Animator playerAnimator;
	Animator pBarAnimator;
	public float secToSing;

	//mechanic
	public static int stars;
	bool isSinging = false;

	void Start () {
		//Elements of the player
		playerCollider = objPlayer.GetComponent<BoxCollider2D> ();
		playerAnimator = objPlayer.GetComponent<Animator>();
		playerAudioSource = objPlayer.GetComponent<AudioSource> ();

		//Elements of the progress bar
		pBarAnimator = progressBar.GetComponent<Animator>();

		//about the mechanic
		stars = 0;
	}


	void Update () {
		//Make player sing
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (playerCollider.OverlapPoint (mousePosition) && isSinging == false) {
				//make sing: change animation, play note
				playerAnimator.SetTrigger("sing");
				stars++;
				StartCoroutine ("sing");
			}
		}

		//end the level
		if (stars == 5 ){
			StartCoroutine("theEnd");
		}
	}
		
	//victory screen
	IEnumerator theEnd () {
		yield return new WaitForSeconds (2f);
		victory.SetActive(true);
		nextUp.SetActive(true);
		stars = 0;
		
	}

	IEnumerator sing(){

		isSinging = true;
		if(isSinging)
		yield return new WaitForSeconds(1f);
		pBarAnimator.SetInteger("cont",stars);
		playerAudioSource.Play ();
		StartCoroutine("stopSing");
		isSinging = false;
	}

	IEnumerator stopSing(){
		yield return new WaitForSeconds (secToSing);
		playerAnimator.SetTrigger("idle");
	}
}