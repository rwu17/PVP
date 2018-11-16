using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {
    private float currentFill;

    private float maxValue = 100;

    [SyncVar(hook = "UpdateValue")]private float currentValue = 100;

    private Image content;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        //currentValue = maxValue;
        SetValue();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GetComponent<Player_Health>().OnChangeValue(10);
            } else if (Input.GetKeyDown(KeyCode.O))
            {
                GetComponent<Player_Health>().OnChangeValue(-10);
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
            Debug.Log("some value at 0!");
        }
    }

    void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }
}