using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {

    public float maxValue = 100;

    [SyncVar (hook = "UpdateValue")]
    public float currentValue = 100;

    public Image content;

    public float currentFill;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        SetValue();
    }

    private void Start()
    {
        currentValue = 100;
        maxValue = 100;
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
            Debug.Log("The target is dead!");
        }
    }

    void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }
}