using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToNextLevel : MonoBehaviour {

	public GameObject nextUp;

	public void goToNextLevel(){
		nextUp.SetActive(false);
	}
}
