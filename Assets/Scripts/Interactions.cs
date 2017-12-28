using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactions : MonoBehaviour {

	//gameObject
	public GameObject switchLight;
	public GameObject darkLight;

	//animation
	Animator switchLightAnimator;
	BoxCollider2D switchLightCollider;

	bool lightsOn;

	void Start () {
		//Elements of the player
		
		//playerAnimator = objPlayer.GetComponent<Animator>();
		//playerAudioSource = objPlayer.GetComponent<AudioSource> ();

		//Elements of the progress bar
		switchLightAnimator = switchLight.GetComponent<Animator>();
		switchLightCollider = switchLight.GetComponent<BoxCollider2D> ();

		lightsOn = true;

	}


	void Update () {
		//Make player sing
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (switchLightCollider.OverlapPoint (mousePosition)) {
				if (lightsOn)
					turnLightsOff();
				else
					turnLightsOn();
			}
		}

		//end the level
		//if (stars >= 5 ){
			//StartCoroutine("theEnd");
		//}
	}
		
	//Acender luzes
	public void turnLightsOn () {
		switchLightAnimator.SetTrigger("on");
		darkLight.SetActive (false);
		lightsOn = true;
	}

	//Apagar luzes
	public void turnLightsOff () {
		switchLightAnimator.SetTrigger("off");
		darkLight.SetActive (true);
		lightsOn = false;
	}

}