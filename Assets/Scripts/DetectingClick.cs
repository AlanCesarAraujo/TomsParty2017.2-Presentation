using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectingClick : MonoBehaviour {

	public int index;
	Collider2D objCollider;

	// Use this for initialization
	void Start () {
		objCollider = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if(objCollider.OverlapPoint(mousePosition)) {
				SceneManager.LoadScene (index);
				Debug.Log ("Scene"+index);
			}
		}
	}
}
