using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public GameObject playerSphere;

    void Update()
    {
        if(isLocalPlayer == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * 3f);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                CmdSpawn();
            }
        }
    }

    [Command]
    public void CmdSpawn()
    {
        GameObject sphere = (GameObject)Instantiate(playerSphere, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(sphere, connectionToClient);
    }

}
