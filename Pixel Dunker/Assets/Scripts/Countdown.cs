using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    // Holder who will show the numbers
    public Image holder;
    // All the different numbers
    public Sprite[] numbers;
    // How much does the timer has to wait
    public float startingTime;
    // If the timer is done
    public bool timerDone = false;
    // If the timer has launched
    private bool timerLaunched = false;
    // The current value of the timer
    private float currentTime = 0f;

    /// <summary>
    /// Start the timer
    /// </summary>
    public void StartTimer() {
        if(!timerLaunched) {
            currentTime = startingTime;
            timerLaunched = true;
        }
    }

    /// <summary>
    /// Stop the timer
    /// </summary>
    public void StopTimer() {
        currentTime = 0f;
        timerLaunched = false;
    }

    /// <summary>
    /// Count down the timer
    /// </summary>
    void FixedUpdate() {
        if(currentTime != 0f) {
            if(currentTime > 0f) {
                currentTime -= 1 * Time.deltaTime;
                int index = (int)Mathf.Floor(currentTime);;
                if(index < 0) {
                    index = 0;
                }
                
                holder.sprite = numbers[index];
            } else {
                timerDone = true;
            }
        }        
    }

}
