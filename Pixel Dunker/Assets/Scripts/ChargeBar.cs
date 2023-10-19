using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{

    // Slider of the bar (value between 0 and 10)
    public Slider slider;
    // The gradient used to change the color
    public Gradient gradient;
    // Interior/fill of the bar
    public Image fill;

    /// <summary>
    /// Function used to set the max value of the bar if needed
    /// </summary>
    /// <param name="charge">Max value</param>
    public void SetMaxCharge(float charge) {
        slider.maxValue = charge;
        slider.value = 0;

        fill.color = gradient.Evaluate(1f);
    }

    /// <summary>
    /// Change the current value of the bar and change its gradient color
    /// </summary>
    /// <param name="charge">Current value</param>
    public void SetCharge(float charge) {
        slider.value = charge;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
