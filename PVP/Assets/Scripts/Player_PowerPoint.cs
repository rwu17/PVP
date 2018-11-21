using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_PowerPoint : NetworkBehaviour {

    public float maxValue;

    [SyncVar(hook = "UpdateValue")]
    public float currentValue = 50;

    public string playerClass;

    public Image power;

    public Image mana;

    public Image rage;

    public Image energy;

    public Image focus;

    public float currentFill;

    public override void OnStartLocalPlayer()
    {
        assignPower();

        playerClass = "stalker";

        switch (playerClass)
        {
            case "mage":
                mana.gameObject.SetActive(true);
                power = mana;
                SetValue();
                break;
            case "warrior":
                rage.gameObject.SetActive(true);
                power = rage;
                SetValue();
                break;
            case "stalker":
                energy.gameObject.SetActive(true);
                power = energy;
                SetValue();
                break;
            case "ranger":
                focus.gameObject.SetActive(true);
                power = focus;
                SetValue();
                break;
            default:
                mana.gameObject.SetActive(true);
                power = mana;
                SetValue();
                break;
        }
    }

    // Use this for initialization
    void Start () {
        playerClass = "stalker";

        switch (playerClass)
        {
            case "mage":
                currentValue = 200;
                maxValue = 200;
                break;
            case "warrior":
                currentValue = 0;
                maxValue = 100;
                break;
            case "stalker":
                currentValue = 100;
                maxValue = 100;
                break;
            case "ranger":
                currentValue = 100;
                maxValue = 100;
                break;
            default:
                currentValue = 0;
                maxValue = 0;
                break;
        }
	}

    private void Update()
    {
        
    }

    void assignPower()
    {
        mana = GameObject.Find("PlayerMana").GetComponent<Image>();
        mana.gameObject.SetActive(false);
        rage = GameObject.Find("PlayerRage").GetComponent<Image>();
        rage.gameObject.SetActive(false);
        energy = GameObject.Find("PlayerEnergy").GetComponent<Image>();
        energy.gameObject.SetActive(false);
        focus = GameObject.Find("PlayerFocus").GetComponent<Image>();
        focus.gameObject.SetActive(false);
    }

    void SetValue()
    {
        if (isLocalPlayer)
        {
            currentFill = currentValue / maxValue;
            power.fillAmount = currentFill;
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
