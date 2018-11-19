using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Target_Health : NetworkBehaviour
{
    private float currentFill;

    private float maxValue;

    [SyncVar(hook = "UpdateValue")] public float currentValue;

    private Image content;

    public GameObject selectedTarget;
    /*
    public override void OnStartLocalPlayer()
    {
        
    }
    */
    void Update()
    {
        if (isLocalPlayer)
        {
            selectedTarget = GetComponent<Player_Target>().target;

            if(selectedTarget != null)
            {
                content = GameObject.Find("TargetHP").GetComponent<Image>();
                maxValue = selectedTarget.GetComponent<Player_Health>().maxValue;
                currentValue = selectedTarget.GetComponent<Player_Health>().currentValue;
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
    /*
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
    */

    void UpdateValue(float value)
    {
        currentValue = value;
        SetValue();
    }
}