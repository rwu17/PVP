using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_PowerPoint : NetworkBehaviour {

    public float maxValue;

    [SyncVar(hook = "UpdateValue")]
    public float currentValue = 100;

    public string playerClass;

    public Image content;

    public Image mana;

    public Image rage;

    public Image energy;

    public Image focus;

    public float currentFill;

    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
        {
            playerClass = "stalker";

            mana = GameObject.Find("PlayerMana").GetComponent<Image>();
            rage = GameObject.Find("PlayerRage").GetComponent<Image>();
            energy = GameObject.Find("PlayerEnergy").GetComponent<Image>();
            focus = GameObject.Find("PlayerFocus").GetComponent<Image>();

            mana.gameObject.SetActive(false);
            rage.gameObject.SetActive(false);
            energy.gameObject.SetActive(false);
            focus.gameObject.SetActive(false);


            switch (playerClass)
            {
                case "mage":
                    mana.gameObject.SetActive(true);
                    content = mana;
                    //content = GameObject.Find("PlayerMana").GetComponent<Image>();
                    break;
                case "warrior":
                    rage.gameObject.SetActive(true);
                    content = rage;
                    //content = GameObject.Find("PlayerRage").GetComponent<Image>();
                    break;
                case "stalker":
                    energy.gameObject.SetActive(true);
                    content = energy;
                    //content = GameObject.Find("PlayerEnergy").GetComponent<Image>();
                    break;
                case "ranger":
                    focus.gameObject.SetActive(true);
                    content = focus;
                    //content = GameObject.Find("PlayerFocus").GetComponent<Image>();
                    break;
                default:
                    break;
            }
        }
    }

    // Use this for initialization
    void Start () {
        //playerClass = "stalker";
        if (isLocalPlayer)
        {
            switch (playerClass)
            {
                case "mage":
                    currentValue = 200;
                    maxValue = 200;
                    SetValue();
                    break;
                case "warrior":
                    currentValue = 0;
                    maxValue = 100;
                    SetValue();
                    break;
                case "stalker":
                    currentValue = 100;
                    maxValue = 100;
                    SetValue();
                    break;
                case "ranger":
                    currentValue = 100;
                    maxValue = 100;
                    SetValue();
                    break;
                default:
                    currentValue = 0;
                    maxValue = 0;
                    SetValue();
                    break;
            }
        }
	}

    void SetValue()
    {
        if (isLocalPlayer)
        {
            currentFill = currentValue / maxValue;
            content.fillAmount = currentFill;
        }
    }

    public void OnChangeValue(float amount)
    {
        currentValue -= amount;

        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        else if (currentValue <= 0)
        {
            currentValue = 0;
            Debug.Log("Out of power!");
        }
    }

    void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }
}
