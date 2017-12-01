using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {

	public float speed;
	public AudioSource note;

	private Transform tPlayer;
	private Animator animator;
	private bool isWalking;
	private bool isSinging;
	private bool isHappy;
	private bool isSad;
	private float destination;
	//private PlayAutomatically managerScript;
	public bool isPlayable2;

	Collider2D objCollider;

	// Use this for initialization
	void Start () {
		tPlayer = GetComponent<Transform>();
		animator = GetComponent<Animator>();
		objCollider = GetComponent<Collider2D>();

		isPlayable2 = false;
		isWalking = false;
		isHappy = false;
		isSad = false;
		isSinging = false;
		StartCoroutine("Wainting");
	}
		
	void FixedUpdate () {
		Move(6f);
		Sing ();

		if (note.isPlaying) {
			animator.CrossFade ("sing", 0f); //change the animation immediately
		} else if (isWalking) {
			animator.CrossFade ("walking", 0f);
			tPlayer.Translate(1f * Time.deltaTime,  0.0f, 0.0f );
		} else if (isHappy) {
			StartCoroutine("HappyDelay");
		} else if (isSad) {
			StartCoroutine("SadDelay");
		} else {
			animator.CrossFade ("idle", 0f);
		}
	}

	void Move(float destination){
		if(transform.position.x<destination){
			tPlayer.Translate(speed * Time.deltaTime , 0.0f, 0.0f);
			isWalking = true;
		}else{
			isPlayable2 = true;
			isWalking = false;
		}
	}

	void Sing(){
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(objCollider.OverlapPoint(mousePosition) && !isWalking) {
				isSinging = true;
			}
		}
		if (isSinging){
			StartCoroutine("SingDelay");
		}	
	}

	IEnumerator SingDelay(){
		yield return new WaitForSeconds (0.5f);
		note.Play();
		isSinging = false;
	}

	IEnumerator HappyDelay(){
		animator.CrossFade ("happy", 0f);
		yield return new WaitForSeconds (1.4f);
		isHappy = false;
	}

	IEnumerator SadDelay(){
		Debug.Log ("to triste");
		animator.CrossFade ("sad", 0f);
		yield return new WaitForSeconds (1.4f);
		isSad = false;
	}

	IEnumerator Wainting(){
		yield return new WaitForSeconds (5.5f);
		isSinging = true;
	}

	public bool getIsHappy(){
		return this.isHappy;
	}

	public void setIsHappy(bool isHappy){
		this.isHappy = isHappy;
	}

	public bool getIsSad(){
		return this.isSad;
	}

	public void setIsSad(bool isSad){
		this.isSad = isSad;
	}

}
