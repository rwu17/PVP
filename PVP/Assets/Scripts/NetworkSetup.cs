using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour {

    //Animations
    public Animator animator;
    float InputX;
    public float InputY;

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
    }
}
