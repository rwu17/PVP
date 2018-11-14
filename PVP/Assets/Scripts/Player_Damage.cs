using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Damage : NetworkBehaviour {

    private float damage;

    private GameObject player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(player.transform.tag == "Player")
            {
                string uIdentity = player.transform.name;
                CmdTellServerWhoTookdmg(uIdentity, damage);
            }

        } else if (Input.GetKeyDown(KeyCode.O))
        {
            if (player.transform.tag == "Player")
            {

            }
        }
    }

    [Command]
    private void CmdTellServerWhoTookdmg(string uniqueID, float damage)
    {
        GameObject go = GameObject.Find(uniqueID);

    }
}
