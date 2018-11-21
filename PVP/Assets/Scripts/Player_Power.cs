﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Power : NetworkBehaviour
{
    private float currentFill;

    private float maxValue = 100;

    public string playerClass;

    [SyncVar(hook = "UpdateValue")] private float currentValue;

    private Image content;

    private Image Mana;

    private Image Rage;

    private Image Energy;

    private Image Focus;

    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
        {
            PowerImage();

            if (playerClass == "Mage")
            {
                Mana.gameObject.SetActive(true);
                content = GameObject.Find("PlayerMana").GetComponent<Image>();
            }
            else if (playerClass == "Warrior")
            {
                Rage.gameObject.SetActive(true);
                content = GameObject.Find("PlayerRage").GetComponent<Image>();
            }
            else if (playerClass == "Stalker")
            {
                Energy.gameObject.SetActive(true);
                content = GameObject.Find("PlayerEnergy").GetComponent<Image>();
            }
            else if (playerClass == "Ranger")
            {
                Focus.gameObject.SetActive(true);
                content = GameObject.Find("PlayerFocus").GetComponent<Image>();
            }
            
            /*
            currentValue = maxValue;
            SetValue();
            */
        }
    }

    void PowerImage()
    {
        Mana = GameObject.Find("PlayerMana").GetComponent<Image>();
        Mana.gameObject.SetActive(false);

        Rage = GameObject.Find("PlayerRage").GetComponent<Image>();
        Rage.gameObject.SetActive(false);

        Energy = GameObject.Find("PlayerEnergy").GetComponent<Image>();
        Energy.gameObject.SetActive(false);

        Focus = GameObject.Find("PlayerFocus").GetComponent<Image>();
        Focus.gameObject.SetActive(false);
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