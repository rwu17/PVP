using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stat : NetworkBehaviour {

    [SerializeField]
    private float lerpSpeed = 0.5f;

    public float currentFill;

    public float maxValue;

    [SyncVar(hook = "UpdateValue")]
    public float currentValue;

    public Image content;
    
    /*
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            
            if(value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if(value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
        }
    }
    */

    void OnChangeValue(float amount)
    {
        if (!isServer)
        {
            return;
        }

        currentValue -= amount;

        if(currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        else if(currentValue <= 0)
        {
            currentValue = 0;
            Debug.Log("some value at 0!");
        }
    }

    void UpdateValue(float value)
    {

        if (value > maxValue)
        {
            value = maxValue;
        }
        else if (value <= 0)
        {
            value = 0;
            Debug.Log("some value at 0!");
        }

        currentFill = value / maxValue;

        if (currentFill != content.fillAmount)
        {
            content.fillAmount = currentFill;
            //content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}
