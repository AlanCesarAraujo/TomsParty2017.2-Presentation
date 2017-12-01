using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

	public GameObject popUp;

	// Use this for initialization
	public void appear () {
		if (popUp.activeInHierarchy == false) {
			popUp.SetActive (true);
			//Time.timeScale = 0;
		} else {
			popUp.SetActive (true);
		}
	}

}
