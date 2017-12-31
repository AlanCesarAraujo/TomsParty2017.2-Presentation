using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour {

	public int index;
	public GameObject fase;
	public GameObject buttons1;
	public GameObject buttons;
	public GameObject victory;

	public void goToScene(){
		victory.SetActive(false);
		fase.SetActive(true);
		buttons1.SetActive(true);
		buttons.SetActive(false);

		Debug.Log("Index: "+ index +" deu certo");
	}

	public void goToScene2(int index){
		SceneManager.LoadScene (index);
		Debug.Log ("Scene"+index);
	}
}
