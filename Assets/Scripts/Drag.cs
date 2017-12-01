using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	Vector3 temp;      //temporary variable to keep the transform
	Vector3 dist;	   
	float posX;
	float posY;
	float initPosX;    //initial position of X;
	float initPosY;    //initial position of X;
	bool isDragging;
	AudioSource note;
	private bool inTouching;

	//Photos objects
	public GameObject otherPhoto;
	public GameObject staticPhoto1;
	public GameObject staticPhoto2;

	//Photos Sprites
	SpriteRenderer sprite;
	SpriteRenderer otherSprite;
	SpriteRenderer staticSprite1;
	SpriteRenderer staticSprite2;

	void Start(){
		temp = transform.position;
		initPosX = temp.x;
		initPosY = temp.y;
		sprite = GetComponent<SpriteRenderer> ();
		otherSprite = otherPhoto.GetComponent<SpriteRenderer> ();
		staticSprite1 = staticPhoto1.GetComponent<SpriteRenderer> ();
		staticSprite2 = staticPhoto2.GetComponent<SpriteRenderer> ();
		note = GetComponent<AudioSource> ();
		isDragging = false;
		inTouching = false;
	}

	void Update(){

	}
		
	void OnMouseDown(){
		//if (!inTouching) {
			note.Play ();

			dist = Camera.main.WorldToScreenPoint (transform.position);
			posX = Input.mousePosition.x - dist.x;
			posY = Input.mousePosition.y - dist.y;
		
			sprite.sortingOrder = 3;
			otherSprite.sortingOrder = 1;
			staticSprite1.sortingOrder = 2;
			staticSprite2.sortingOrder = 0;
		//}
	}

	//While is dragging
	void OnMouseDrag(){
		if(!inTouching){
			Vector3 mousePos = new Vector3 (Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);

			Vector3 worldPos = Camera.main.ScreenToWorldPoint (mousePos);
			transform.position = worldPos;

			isDragging = true;
		}

	}

	public void OnMouseUp(){
		temp.x = initPosX;
		temp.y = initPosY;
		transform.position = temp;
		isDragging = false;
		inTouching = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.tag == "Portrait"){
			Debug.Log ("aeeeedrag");

			temp.x = initPosX;
			temp.y = initPosY;
			transform.position = temp;
			isDragging = false;
			inTouching = true;
		}
	}

	//
		
	bool getIsDragging(){
		return isDragging;
	}
}
