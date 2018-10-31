using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    private float zoom;
    public float zoomSpeed = 2;
    public float zoomMin = -0.8f;
    public float zoomMax = -10f;

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
            zoom = -3;
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
            /*
            zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

            if (zoom > zoomMin)
                zoom = zoomMin;

            if (zoom < zoomMax)
                zoom = zoomMax;

            playerCamera.transform.localPosition = new Vector3(0, 0, zoom);
            */

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
