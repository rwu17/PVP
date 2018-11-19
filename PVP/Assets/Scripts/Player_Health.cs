using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {
    public float currentFill;

    [SyncVar(hook = "UpdateValue")]private float currentValue = 100;

    private float maxValue = 100;

    private Image content;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        UpdateValue(maxValue);
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
}