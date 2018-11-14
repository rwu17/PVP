using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {
    //[SerializeField]
    //private float lerpSpeed = 0.5f;

    public float currentFill;

    public float maxValue = 100;

    [SyncVar(hook = "UpdateValue")]public float currentValue;

    public Image content;

    void Start()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        SetValue();
    }

    void SetValue()
    {
        if (isLocalPlayer)
        {
            currentValue = maxValue;
            currentFill = currentValue / maxValue;
            content.fillAmount = currentFill;
        }
    }

    public void OnChangeValue(float amount)
    {
        if (!isServer)
        {
            return;
        }

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
        currentFill = value / maxValue;

        if (currentFill != content.fillAmount)
        {
            content.fillAmount = currentFill;
        }
    }
}
