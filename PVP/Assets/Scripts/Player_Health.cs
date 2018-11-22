using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {

    public float maxValue;

    [SyncVar (hook = "UpdateValue")]
    public float currentValue = 25; //Don't know what this value does

    public Image content;

    public float currentFill;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        //SetValue();
    }

    private void Start()
    {
        currentValue = 100; //initiate value
        maxValue = 50; //initiate value
        SetValue();
    }

    void SetValue()
    {
        if (isLocalPlayer)
        {
            valueValidate();
            currentFill = currentValue / maxValue;
            content.fillAmount = currentFill;
        }
    }
    
    public void OnChangeValue(float amount)
    {
        currentValue -= amount;
        valueValidate();
    }

    void UpdateValue(float value)
    {
        currentValue = value;
        valueValidate();
        SetValue();
    }

    void valueValidate()
    {
        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        else if (currentValue <= 0)
        {
            currentValue = 0;
            Debug.Log("The target is dead!");
        }
    }
}