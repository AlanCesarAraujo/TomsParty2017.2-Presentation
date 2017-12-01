using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour {

	public int index;

	public void goToScene(int index){
		SceneManager.LoadScene (index);
		Debug.Log ("Scene"+index);
	}
}
