using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Power : NetworkBehaviour
{
    private float currentFill;

    private float maxValue = 100;

    public string playerClass;

    [SyncVar(hook = "UpdateValue")] public float currentValue;

    private Image content;

    public override void OnStartLocalPlayer()
    {
        GameObject.Find("PlayerMana").SetActive(false);
        GameObject.Find("PlayerRage").SetActive(false);
        GameObject.Find("PlayerEnergy").SetActive(false);
        GameObject.Find("PlayerFocus").SetActive(false);

        if (playerClass == "Mage")
        {
            GameObject.Find("PlayerMana").SetActive(true);
            content = GameObject.Find("PlayerMana").GetComponent<Image>();
        }
        else if(playerClass == "Warrior")
        {
            GameObject.Find("PlayerRage").SetActive(true);
            content = GameObject.Find("PlayerRage").GetComponent<Image>();
        }
        else if (playerClass == "Stalker")
        {
            GameObject.Find("PlayerEnergy").SetActive(true);
            content = GameObject.Find("PlayerEnergy").GetComponent<Image>();
        }
        else if (playerClass == "Ranger")
        {
            GameObject.Find("PlayerFocus").SetActive(true);
            content = GameObject.Find("PlayerFocus").GetComponent<Image>();
        }
        /*
        currentValue = maxValue;
        SetValue();
        */
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
            Debug.Log("Dead!");
        }
    }

    void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }

    void classPower(string playerClass) //Function for how power works for different class
    {

    }

}