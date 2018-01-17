using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript2 : MonoBehaviour {

	public GameObject progressBar;
	public GameObject popUpVictory;

	public GameObject[] objPhotos = new GameObject[3];
	private PlayingNote2[] photos = new PlayingNote2[3];
	public int[] photosID = new int[3];
	public AudioClip[] sounds = new AudioClip[2];
	private Drop[] drops = new Drop[3];


	private bool isPlayable;
	private bool valido;
	public bool Rounding;
	private bool isCorrect;
	private Animator animProgress;
	public GameObject reflexo1;
	public GameObject reflexo2;
	public GameObject reflexo3;
	public GameObject repeatNotes;
	private int cont;
	private Button repeatBtn;

	public AudioClip tryAgain;
	public AudioClip wellDone;
	private AudioSource narrator;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			photos[i] = objPhotos[i].GetComponent<PlayingNote2>();
			drops[i] = objPhotos[i].GetComponent<Drop>();
		}

		repeatBtn = repeatNotes.GetComponent<Button>();
		repeatBtn.onClick.AddListener(playAgain);

		narrator = GetComponent<AudioSource> ();
		animProgress = progressBar.GetComponent <Animator>();
		cont = 0;

		valido = false;
		isPlayable = false;

		isCorrect = false;

		//accessing the bugle's scripts

		SoundsOrder ();
		StartCoroutine ("Round");

	}
	
	// Update is called once per frame
	void Update () {
		if(drops[0].getIsReady() && drops[1].getIsReady() && drops[2].getIsReady()){
			StartCoroutine ("Checking");

			drops [0].setIsReady (false);
			drops [1].setIsReady (false);
			drops [2].setIsReady (false);
		}



		animProgress.SetInteger ("cont", cont);
	}

	void playAgain(){
		StartCoroutine ("Round");
	}

	IEnumerator Round(){
		yield return new WaitForSeconds (1f);
		reflexo1.SetActive (true);
		photos[0].Play ();
		yield return new WaitForSeconds (0.5f);
		reflexo1.SetActive (false);
		yield return new WaitForSeconds (1.5f);		
		reflexo2.SetActive (true);
		photos[1].Play ();
		yield return new WaitForSeconds (0.5f);
		reflexo2.SetActive (false);
		yield return new WaitForSeconds (1.5f);	
		reflexo3.SetActive (true);
		photos[2].Play ();
		yield return new WaitForSeconds (0.5f);
		reflexo3.SetActive (false);
		isPlayable = true;
	}

	IEnumerator Checking(){
		drops [0].setIsReady (false);
		yield return new WaitForSeconds (1f);
		photos[0].checkNote ();
		//if this note is correct it goes to the next one
		if (photos [0].getCorrect ()) {
			yield return new WaitForSeconds (2f);
			drops [1].setIsReady (false);
			photos [1].checkNote ();
			//if this note is correct it goes to the next one
			if (photos [1].getCorrect ()) {
				yield return new WaitForSeconds (2f);
				drops [2].setIsReady (false);
				photos [2].checkNote ();
				//if this note is correct you win the round
				if (photos [2].getCorrect ()) {
					isCorrect = true;
					narrator.clip = wellDone;
					narrator.Play ();
					if (isCorrect && cont<5) {
						cont++;
						isPlayable = false;
						StartCoroutine("Ordering");
						Debug.Log ("ACERTÔ MISERAVI");
						isCorrect = false;
					} 

					if (cont >= 5) {
						popUpVictory.SetActive (true);
					}
					yield return new WaitForSeconds (1f);
					for (int i = 0; i < 3; i++) {
						drops [i].Clean ();
					}

				} else {
					for (int i = 0; i < 3; i++) {
						drops [i].Clean ();
					}
					narrator.clip = tryAgain;
					narrator.Play ();
				}

			} else {
				for (int i = 0; i < 3; i++) {
					drops [i].Clean ();
				}
				narrator.clip = tryAgain;
				narrator.Play ();
			}

		} else {
			for (int i = 0; i < 3; i++) {
				drops [i].Clean ();
				narrator.clip = tryAgain;
				narrator.Play ();
			}
		}

	}

	IEnumerator Ordering(){
		isPlayable = false;
		yield return new WaitForSeconds (2f);

		if(cont<5){
			SoundsOrder ();
			StartCoroutine ("Round");
		}
	}


	void RandomOrder(int[] b){
		for (int i = 0; i < b.Length; i++) {
			photosID[i] = Random.Range(0,2);
		}
		Debug.Log (photosID[0]+", "+photosID[1]+", "+photosID[2]);
	}

	void SoundsOrder(){

		RandomOrder(photosID);
		Debug.Log (photosID[0] +", "+photosID[1]);
		for(int i = 0; i<photosID.Length; i++){
			//Debug.Log ("FOR");
			switch(photosID[i]){
			case 0:					
				photos [i].setId (0);					
				photos [i].setNote (sounds [0]);
				isCorrect = photos [i].getCorrect();
				Debug.Log ("Case 0 (DO): "+photos [i].getId());
				break;
			case 1:				
				photos [i].setId(1);					
				photos [i].setNote(sounds [1]);
				Debug.Log ("Case 1(NÃO): "+photos [i].getId());
				break;
			}
		}
	}

	public bool getIsPlayable(){
		return this.isPlayable;
	}

	public void setIsPlayable(bool isPlayable){
		this.isPlayable = isPlayable;
	}
}
