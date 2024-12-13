using System;
using UnityEngine;
using UnityEngine.UI;


public class UIEnemy : MonoBehaviour
{
    public Slider slider;

    public Action<float> OnChangeHealthBar;

    private void OnEnable()
    {
        slider.value = 1f;
        OnChangeHealthBar += ChangeHealthBar;
    }

    private void ChangeHealthBar(float valueDivMaxValue)
    {
        slider.value = valueDivMaxValue;
    }

    private void OnDisable()
    {
        OnChangeHealthBar -= ChangeHealthBar;
    }
}