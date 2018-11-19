using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Target : NetworkBehaviour {

    public GameObject target;

    private GameObject targetFrame;

    public Text targetName;

    private string playerSelf;

    private float targetHPFill = 0.5f;

    public Image targetHP;

    public override void OnStartLocalPlayer()
    {
        playerSelf = GetComponent<Player_ID>().playerUniqueName;
        targetFrame = GameObject.Find("TargetFrame");
        targetHP = GameObject.Find("TargetHP").GetComponent<Image>();
        targetHP.fillAmount = targetHPFill;
        targetFrame.gameObject.SetActive(false);
    }

    void Update () {
        if (isLocalPlayer)
        {
            if(target != null)
            {

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
            if (hit.transform.name == playerSelf)
            {
                print("You can't select yourself");
                target = null;
            }
            else if (hit.transform.tag == "Player")
            {
                target = hit.transform.gameObject;
                targetFrame.gameObject.SetActive(true);
                targetHP.gameObject.SetActive(true);
                //targetHP.fillAmount = target.GetComponent<Player_Health>().currentFill;
            }
            else
            {
                target = null;
                targetFrame.gameObject.SetActive(false);
            }
        }
    }

    [Command]
    private void CmdServerDamage(GameObject damageTarget, float damage)
    {
        //GameObject selectedTarget = GameObject.Find(target);
        //selectedTarget.GetComponent<Player_Health>().OnChangeValue(damage);
        damageTarget.GetComponent<Player_Health>().OnChangeValue(damage);
    }
}