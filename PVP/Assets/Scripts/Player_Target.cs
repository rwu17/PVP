using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Target : NetworkBehaviour {

    //Cursor on enemies
    public Texture2D cursorMain;
    public Texture2D cursorAttack;
    public Texture2D cursorEnemy;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject player;

    private GameObject target;

    private GameObject targetFrame;

    private Text targetName;

    public string playerSelf;

    private float targetCurrentHP;

    private float targetMaxHP;

    private float targetHPFill;

    private Image targetHP;

    public override void OnStartLocalPlayer()
    {
        playerSelf = GetComponent<Player_ID>().playerPublicName;
        targetFrame = GameObject.Find("TargetFrame");
        targetHP = GameObject.Find("TargetHP").GetComponent<Image>();
        targetName = GameObject.Find("TargetName").GetComponent<Text>();
        targetFrame.gameObject.SetActive(false);
    }

    void Update() {
        if (isLocalPlayer)
        {
            changeCursor();

            if (Input.GetMouseButtonDown(0))
            {
                TargetSelect();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                target = null;
            }

            if (target != null)
            {
                TargetHP(target);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (target != null)
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
                //CmdTellServerTargetName(target);
                TargetHP(target);
            }
            else
            {
                target = null;
                targetFrame.gameObject.SetActive(false);
            }
        }
    }

    void TargetHP(GameObject selectedTarget)
    {
        targetCurrentHP = selectedTarget.GetComponent<Player_Health>().currentValue;
        targetMaxHP = selectedTarget.GetComponent<Player_Health>().maxValue;
        targetHPFill = targetCurrentHP / targetMaxHP;
        targetHP.fillAmount = targetHPFill;
    }

    /*
    [Command]
    void CmdTellServerTargetName(GameObject selectedTarget)
    {
        targetName.text = selectedTarget.GetComponent<Player_ID>().GetPlayerUniqueName();
    }
    */

    void changeCursor(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast (ray, out hit, 10000))
        {
            if (hit.transform.name == playerSelf)
            {
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
            }
            else if (hit.transform.tag == "Player")
            {
                Cursor.SetCursor(cursorEnemy, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
            }
        }
    }

    [Command]
    void CmdServerDamage(GameObject damageTarget, float damage)
    {
        //GameObject selectedTarget = GameObject.Find(target);
        //selectedTarget.GetComponent<Player_Health>().OnChangeValue(damage);
        damageTarget.GetComponent<Player_Health>().OnChangeValue(damage);
    }
}