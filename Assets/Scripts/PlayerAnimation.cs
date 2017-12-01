using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Animator animator;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool fire = Input.GetButtonDown("Fire1");

        animator.SetFloat("Forward",v);
        animator.SetFloat("Strafe",h);
        animator.SetBool("Fire", fire);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Enemy")){
            animator.SetTrigger("Die");
        }
    }
}
