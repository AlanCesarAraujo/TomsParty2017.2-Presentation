using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

	//External game objects
	public GameObject objPlayer; 
	public GameObject progressBar;
	public GameObject popUpVictory;

	public GameObject[] objBugles = new GameObject[3];
	private PlayingNote[] bugles = new PlayingNote[3];
	public int[] buglesID = new int[3];
	public AudioClip[] sounds = new AudioClip[3];

	private int cont;

	bool isStarting;
	private bool isPlayable;
	private bool valido;
	public bool Rounding;
	private bool isCorrect;
	private Animator animProgress;



	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++) {
			bugles [i] = objBugles[i].GetComponent<PlayingNote>();
		}

		animProgress = progressBar.GetComponent <Animator>();
		cont = 0;

		valido = false;
		isPlayable = false;

		//isCorrect = false;

		//accessing the bugle's scripts
		isStarting = true;
		SoundsOrder ();
		StartCoroutine ("Waiting");

	}

	void Update () {
		for(int i = 0; i<bugles.Length; i++){
			if(bugles[i].getId() == 0 && bugles[i].getCorrect() == true){
				isCorrect = bugles [i].getCorrect ();
				bugles [i].setCorrect (false);
			}
		}
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

		animProgress.SetInteger ("cont", cont);
		//Debug.Log (bugles[0].getCorrect()+" "+bugles[1].getCorrect()+" "+bugles[2].getCorrect());
	}

	//Wainting to start the sounds
	IEnumerator Waiting(){
		yield return new WaitForSeconds (7f);
		StartCoroutine ("Round");
	}

	IEnumerator Ordering(){
		yield return new WaitForSeconds (2f);

		if(cont<5){
			SoundsOrder ();
			StartCoroutine ("Round");
		}
	}

	//Playing the three sounds
	IEnumerator Round(){
		Rounding = true;
		yield return new WaitForSeconds (1f);
		bugles[0].Play ();
		yield return new WaitForSeconds (2f);
		bugles[1].Play ();
		yield return new WaitForSeconds (2f);
		bugles[2].Play ();
		isPlayable = true;
	}

	void RandomOrder(int[] b){
		for (int i = 0; i < b.Length; i++) {
			do {
				buglesID[i] = Random.Range(0,3);
				valido = true;
				for (int j = 0; j < i; j++)
					if (b[i] == buglesID[j])
						valido = false;
			} while (valido == false);
		}
		Debug.Log (buglesID[0]+", "+buglesID[1]+", "+buglesID[2]);
	}

	void SoundsOrder(){
		
		RandomOrder(buglesID);
	
		for(int i = 0; i<buglesID.Length; i++){
			//Debug.Log ("FOR");
			switch(buglesID[i]){
				case 0:					
					bugles [i].setId (0);					
					bugles [i].setNote (sounds [0]);
					isCorrect = bugles [i].getCorrect();
					//Debug.Log ("Case 0 (DO): "+bugles [i].getId());
					break;
				case 1:				
					bugles [i].setId(1);					
					bugles [i].setNote(sounds [1]);
					//Debug.Log ("Case 1(NÃO): "+bugles [i].getId());
					break;
				case 2:				
					bugles [i].setId(2);					
					bugles [i].setNote(sounds [2]);
					//Debug.Log ("Case 2(NÃO): "+bugles [i].getId());
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