using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stat : NetworkBehaviour {

    private Image content = GetComponent<Image>();

    private float lerpSpeed = 0.5f;

    private float currentFill;

    public float MyMaxValue { get; set; }

    [SyncVar (hook = "OnChangeValue")] private float currentValue;

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

    void OnChangeValue()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
