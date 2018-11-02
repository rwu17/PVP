using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour {

    //Animations
    public Animator animator;
    float InputX;
    public float InputY;
    private CharacterController controller;

    void Update()
    {
        CheckForPlayerInput();
    }

    void CheckForPlayerInput()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");
        animator.SetFloat("X", InputX);
        animator.SetFloat("Y", InputY);

        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("In Air", true);
            }
            else
            {
                animator.SetBool("In Air", false);
            }
        }
    }
}
