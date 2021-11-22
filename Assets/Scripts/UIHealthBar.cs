using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image mask;
   // public Slider Health;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
      //  Health.value = 5;
      //  Health.maxValue = 5;
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
       // Health.value = value;
    }
}

