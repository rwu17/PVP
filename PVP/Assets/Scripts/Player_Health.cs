using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_Health : NetworkBehaviour {

    [SyncVar (hook = "UpdateValue")]
    private float currentValue = 100;

    private float maxValue = 100;

    private Image content;

    private float currentFill;

    public float publicValue;

    public float publicMaxValue;

    public override void OnStartLocalPlayer()
    {
        content = GameObject.Find("PlayerHP").GetComponent<Image>();
        SetValue();
    }

    private void Update()
    {
        if (!isServer)
        {
            CmdPlayerValue(currentValue);
            CmdPlayerMaxValue(maxValue);
        }
        else if(isServer)
        {
            RpcPlayerValue(currentValue);
            RpcPlayerMaxValue(currentValue);
        }
    }

    public void SetValue()
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

    public void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }

    [Command]
    void CmdPlayerValue(float value)
    {
        RpcPlayerValue(value);
    }

    [ClientRpc]
    void RpcPlayerValue(float value)
    {
        publicValue = value;
    }

    [Command]
    void CmdPlayerMaxValue(float value)
    {
        RpcPlayerMaxValue(value);
    }

    [ClientRpc]
    void RpcPlayerMaxValue(float value)
    {
        publicMaxValue = value;
    }
}