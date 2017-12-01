using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mec_Two : MonoBehaviour {

	//gameObject
	public GameObject objPlayer;
	public GameObject[] objFlower = new GameObject[3];

	//audio
	private AudioSource playerAudioSource;
	public AudioClip[] sounds = new AudioClip[3];

	//flowers
	private PlayingNote[] flowers = new PlayingNote[3];
	public int[] flowersID = new int[3];
	Animator[] flowerAnimator = new Animator[3];

	//player
	private Transform playerTransform;
	private Animator playerAnimator;
	private Collider2D playerCollider;
	private bool isWalking;
	private bool isSinging;
	private float time;
	private bool getTime;
	private float col;
	private bool up, down;

	//end
	private bool win;
	private float posX;

	private bool valido;

	/*//save the time of the click
	if (getTime) {
		time = Time.frameCount;
		getTime = false;
	}*/

	void Start () {
		//Elements of the player
		playerCollider = objPlayer.GetComponent<BoxCollider2D> ();
		playerAnimator = objPlayer.GetComponent<Animator>();
		playerTransform = objPlayer.GetComponent<Transform>();
		playerAudioSource = objPlayer.GetComponent<AudioSource> ();

		//Elements of the flowers
		for (int i = 0; i < 3; i++) {
			flowers [i] = objFlower[i].GetComponent<PlayingNote>();
			flowers [i].setAnimator (objFlower [i].GetComponent<Animator> ());
		}
			
		valido = false;
		isWalking = false;
		isSinging = false;
		getTime = true; // able or disable the possibility to get the current frame(time)
		time = 0; // the variable that gets the current frame(time)
		posX = playerTransform.position.x; //position x of the player 
		down = true; //if the player goes up or down
		up = false;
		win = false; //if it's the end

		//objFlower [1].SetActive (false);
		//objFlower [2].SetActive (false);

		chooseFlowerNotes ();
		StartCoroutine ("Round");
	}

	void FixedUpdate () {
		//Debug.Log ("position player= "+playerTransform.position.x+" | posX= "+posX);

		if (isSinging) {
			playerAnimator.CrossFade ("sing", 0f); //change the animation immediately
		} else if (isWalking) {
			playerAnimator.CrossFade ("walking", 0f);
			//go up
			if (up && posX > playerTransform.position.x) {
				playerTransform.Translate (1f* Time.deltaTime, 1.2f * Time.deltaTime, 0.0f);
			} else if (up) {
				isWalking = false;
				getTime = true;
			}

			//go down
			if (down && posX > playerTransform.position.x) {
				playerTransform.Translate (1f* Time.deltaTime, -1.2f * Time.deltaTime, 0.0f);
			} else if (down) {
				isWalking = false;
				getTime = true;
			}
				
		} else {
			playerAnimator.CrossFade ("idle", 0f);
		}


	}
		
	void Update () {
		
		//Debug.Log ("Walk: "+isWalking);
		//Make player sing
		if(Input.GetMouseButtonDown(0)) {

			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (playerCollider.OverlapPoint (mousePosition) && !isSinging && !isWalking) {
				if (down) {
					up = true;
					down = false;
				} else if (up) {
					down = true;
					up = false;
				}
				//save the time of the click
				if (getTime) {
					time = Time.frameCount;
					posX = playerTransform.position.x+1.7f;
					getTime = false;
				}

				//make sing: change animation, play note
				isSinging = true;
				playerAudioSource.Play ();
			}
		}

		//make the player walk after a time
		if (isSinging && !isWalking && Time.frameCount > time + 70 && col<8) {
				isSinging = false;
				isWalking = true;
				time = Time.frameCount;
		}
			
	}

	void clickFlower(int id){
		if (id == 0) {
			flowers [0].Play ();
			col++; // next col
			isWalking = true;
			//player.moveAnywhere(posX[a], posY[0], 2);
		}  
		if (id == 1) { 
			flowers [1].Play ();
			//sTry.playSound(); tente de novo
		}
		if (id == 2) {
			flowers [2].Play ();
			//sTry.playSound(); tente de novo
		} else if (col >6) {
			win = true;
			congrats();
		}

	}

	void chooseFlowerNotes() {
		for (int i = 0; i <3; i++) {
			do {
				flowersID[i] = Random.Range(0,3);;
				valido = true;
				for (int j = 0; j < i; j++)
					if (flowersID[i] == flowersID[j])
						valido = false;
			} while (valido == false);
		}
		setFlowerNotes ();
	}

	void setFlowerNotes(){

		for(int i = 0; i<flowersID.Length; i++){
			//Debug.Log ("FOR");
			switch(flowersID[i]){
			case 0:					
				flowers [i].setId (0);					
				flowers [i].setNote (sounds [0]);
				//isCorrect = flowers [i].getCorrect();
				//Debug.Log ("Case 0 (DO): "+bugles [i].getId());
				break;
			case 1:				
				flowers [i].setId(1);					
				flowers [i].setNote(sounds [1]);
				//Debug.Log ("Case 1(NÃO): "+bugles [i].getId());
				break;
			case 2:				
				flowers [i].setId(2);					
				flowers [i].setNote(sounds [2]);
				//Debug.Log ("Case 2(NÃO): "+bugles [i].getId());
				break;
			}
		}
	}

	void restart(){
		isWalking = false;
		isSinging = false;
		getTime = true;
		time = 0;
		col = 0;
	}

	void congrats(){
		Debug.Log ("GANHOU");
	}

	//Playing the three sounds
	IEnumerator Round(){
		yield return new WaitForSeconds (4f);
		flowers[0].Play ();

		yield return new WaitForSeconds (2f);
		objFlower [1].SetActive (true);
		flowers[1].Play ();

		yield return new WaitForSeconds (2f);
		objFlower [2].SetActive (true);
		flowers[2].Play ();
	}
}