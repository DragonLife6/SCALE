using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderScript : MonoBehaviour
{
    Slider slider;

    private void Start()
    {
         slider = GetComponent<Slider> (); 
    }

    public void UpdateSlider(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }
}
