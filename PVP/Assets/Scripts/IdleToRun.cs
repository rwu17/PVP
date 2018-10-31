using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToRun : MonoBehaviour {

    public Animator animator;
    


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetInteger("Condition", 1);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetInteger("Condition", 0);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
