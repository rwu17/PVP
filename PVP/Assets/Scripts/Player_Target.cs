using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Target : NetworkBehaviour {

    public GameObject player;

    public GameObject target;

    public string playerName;

    public GameObject targetFrame;

    public override void OnStartLocalPlayer()
    {
        playerName = player.GetComponent<Player_ID>().playerUniqueName;
        targetFrame = GameObject.Find("TargetFrame");
        targetFrame.gameObject.SetActive(false);
    }

    void Update () {
        if (isLocalPlayer)
        {
            if(target != null)
            {
                targetFrame.gameObject.SetActive(true);
            } else
            {
                targetFrame.gameObject.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                TargetSelect();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                target = null;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if(target != null)
                {
                    CmdServerDamage(target, 10);
                }
                else
                {
                    print("You need to select a target!");
                }
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                if (target != null)
                {
                    CmdServerDamage(target, -10);
                }
                else
                {
                    print("You need to select a target!");
                }
            }
        }
	}

    void TargetSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.transform.name == playerName)
            {
                print("You can't select yourself");
                target = null;
            }
            else if (hit.transform.tag == "Player")
            {
                target = hit.transform.gameObject;
            }
            else
            {
                target = null;
            }
        }
    }

    public void SelectPlayer()
    {
        target = player;
    }

    [Command]
    private void CmdServerDamage(GameObject damageTarget, float damage)
    {
        //GameObject selectedTarget = GameObject.Find(target);
        //selectedTarget.GetComponent<Player_Health>().OnChangeValue(damage);
        damageTarget.GetComponent<Player_Health>().OnChangeValue(damage);
    }
}