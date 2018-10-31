using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public CharacterController controller;

    private Vector3 moveDirection;

    public GameObject playerCamera;

    private void Start()
    {
        if (isLocalPlayer == true)
        {
            playerCamera.GetComponent<Camera>().enabled = true;
        } else
        {
            playerCamera.GetComponent<Camera>().enabled = false;
        }

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(isLocalPlayer == true)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
    /*
    [Command]
    public void CmdSpawn()
    {
        GameObject sphere = (GameObject)Instantiate(playerSphere, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(sphere, connectionToClient);
    }
    */
}
