using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

	public GameObject portraitDo;
	public GameObject portraitRe;

	private GameObject currentPhoto;

	int id;

	SpriteRenderer spriteDo;
	SpriteRenderer spriteRe;

	private bool isReady;
	Collider2D col;

	void Start(){
		col = GetComponent<Collider2D> ();
		spriteDo = portraitDo.GetComponent<SpriteRenderer> ();
		spriteRe = portraitRe.GetComponent<SpriteRenderer> ();
		isReady = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("aeeee drop");
		if(other.gameObject.tag == "PhotoDo"){
			currentPhoto = portraitDo;
			portraitDo.SetActive (true);
			spriteDo.sortingOrder = 1;
			spriteRe.sortingOrder = 0;
			col.enabled = false;
			isReady = true;
			id = 0;
		} else if(other.gameObject.tag == "PhotoRe"){
			currentPhoto = portraitRe;
			portraitRe.SetActive (true);
			spriteDo.sortingOrder = 0;
			spriteRe.sortingOrder = 1;
			col.enabled = false;
			isReady = true;
			id = 1;
		}
	}

	public void Clean(){
		currentPhoto.SetActive (false);
		col.enabled = true;
	}

	public bool getIsReady(){
		return isReady;
	}

	public void setIsReady(bool isReady){
		this.isReady = isReady;
	}
		
	Collider2D getCollider (){
		return col;
	}

	public int getId(){
		return this.id;
	}

}
