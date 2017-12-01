using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mec_Three : MonoBehaviour {

	//gameObject
	public GameObject objPlayer; //player
	public GameObject[] objWood = new GameObject[4]; //woods
	public GameObject progressBar; //progressBar
	public GameObject victory;


	//audios
	//player
	private AudioSource playerAudioSource;
	public AudioClip[] sounds = new AudioClip[2];
	//woods
	private AudioSource[] woodAudioSource = new AudioSource[4];

	//id  woods
	int[] woodsOrder = new int[4]; 

	//collisor
	Collider2D playerCollider;

	//animation
	Animator playerAnimator;
	Animator[] woodAnimator = new Animator[4];
	Animator pBarAnimator;

	//mechanic
	public static int lastWood, actualWood;
	public static bool firstClick;
	public static int stars;
	public static int totalCorrect;


	void Start () {
		//Elements of the player
		playerCollider = objPlayer.GetComponent<BoxCollider2D> ();
		playerAnimator = objPlayer.GetComponent<Animator>();
		playerAudioSource = objPlayer.GetComponent<AudioSource> ();

		//Elements of the woods
		for (int i = 0; i < 4; i++) {
			woodAudioSource[i] = objWood [i].GetComponent<AudioSource>();
			woodAnimator[i] = objWood [i].GetComponent<Animator> ();
		}

		//Elements of the progress bar
		pBarAnimator = progressBar.GetComponent<Animator>();

		//about the mechanic
		chooseWoodsNotes ();
		firstClick = true;
		stars = 0;
	}
				

	void Update () {
		//Make player sing
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (playerCollider.OverlapPoint (mousePosition)) {
				//make sing: change animation, play note
				playerAnimator.SetTrigger("sing");
				StartCoroutine ("stopSing");
				playerAudioSource.Play ();
			}
		}
	}

	//choose random notes to the woods
	void chooseWoodsNotes() {
		bool valido;
		for (int i = 0; i < 4; i++) {
			do {
				woodsOrder [i] = Random.Range (0, 4);
				;
				valido = true;
				for (int j = 0; j < i; j++)
					if (woodsOrder [i] == woodsOrder [j])
						valido = false;
			} while (valido == false);
		}	
		Debug.Log("note1="+woodsOrder[0]+"  note2="+woodsOrder[1]+"  note3="+woodsOrder[2]+"  note4="+woodsOrder[3]);
		setWoodsNotes ();
	}

	// set audio clips to the woods	
	void setWoodsNotes(){
		woodAudioSource[woodsOrder[0]].clip = sounds [0];
		woodAudioSource[woodsOrder[1]].clip = sounds [0];
		woodAudioSource[woodsOrder[2]].clip = sounds [1];
		woodAudioSource[woodsOrder[3]].clip = sounds [1];
	}
		


	//the first wood (piece) is clicked and waits for the second click
	IEnumerator turnFirst(){
		//Debug.Log ("1 last : "+lastWood+" | actual : " + actualWood); 
		yield return new WaitForSeconds (0.4f);
		woodAudioSource [actualWood].Play ();
		lastWood = actualWood;
		firstClick = false;
	}



	//the second click. checks if it's the correct "par"
	IEnumerator turnSecond(){
		if (lastWood == actualWood) {
			yield return new WaitForSeconds (0.4f);
			woodAudioSource [actualWood].Play ();
		} else {
			//play the note of the wood
			yield return new WaitForSeconds (0.4f);
			woodAudioSource [actualWood].Play ();

			if (woodAudioSource [lastWood].clip == woodAudioSource [actualWood].clip) {
				//if it's correct, mi happy, plus star, and stay normal
				playerAnimator.SetTrigger ("happy");
				StartCoroutine ("stopHappy");
				totalCorrect++;
				woodAnimator [actualWood].SetTrigger ("turned");
				woodAnimator [lastWood].SetTrigger ("turned");
				firstClick = true;

				if (totalCorrect == 2 && stars != 5) {
					stars++;
					pBarAnimator.SetInteger ("cont", stars);
					totalCorrect = 0;
					StartCoroutine ("newRound");
				}
				if (stars == 5) {
					StartCoroutine ("theEnd");
				}
				
			} else {
				//if it's wrong turn back the woods
				playerAnimator.SetTrigger ("sad");
				StartCoroutine ("stopSad");
				yield return new WaitForSeconds (2f);
				woodAnimator [actualWood].SetTrigger ("hide");
				woodAnimator [lastWood].SetTrigger ("hide");
				firstClick = true;
			}
		}
	}



	//restart a new round
	IEnumerator newRound(){
		yield return new WaitForSeconds (3f);
		woodAnimator [0].SetTrigger ("hide");
		woodAnimator [1].SetTrigger ("hide");
		woodAnimator [2].SetTrigger ("hide");
		woodAnimator [3].SetTrigger ("hide");

		chooseWoodsNotes ();
	}

	//victory screen
	IEnumerator theEnd () {
		yield return new WaitForSeconds (2f);
		victory.SetActive (true);
	}


	//time to stop animations
	IEnumerator stopHappy(){
		yield return new WaitForSeconds (2.5f);
		playerAnimator.SetTrigger("idle");
	}
	IEnumerator stopSad(){
		yield return new WaitForSeconds (3f);
		playerAnimator.SetTrigger("idle");
	}
	IEnumerator stopSing(){
		yield return new WaitForSeconds (2f);
		playerAnimator.SetTrigger("idle");
	}
}