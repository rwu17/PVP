using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {
    public float currentFill;

    public float maxValue = 100;

    [SyncVar(hook = "UpdateValue")]public float currentValue;

    public Image content;

    public Player_ID player;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        currentValue = maxValue;
        SetValue();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OnChangeValue(10);
                SetValue();
            } else if (Input.GetKeyDown(KeyCode.O))
            {
                OnChangeValue(-10);
                SetValue();
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