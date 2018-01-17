using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {

	private Transform tCloud;
	public float speed;

	// Use this for initialization
	void Start () {
		tCloud = GetComponent<Transform>();
		
	}
		
	void FixedUpdate () {	
		tCloud.Translate(speed * Time.deltaTime,  0.0f, 0.0f );
		if (transform.position.x < -14f){
			tCloud.Translate(33f,  0.0f, 0.0f );
		}
	}

}
