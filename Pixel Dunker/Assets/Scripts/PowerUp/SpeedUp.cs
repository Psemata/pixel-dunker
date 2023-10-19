using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SpeedUp</c> this a power up apply to the player and give more speed during fews secondes 
/// </summary>
public class SpeedUp : MonoBehaviour
{
    //Stats
    public float speedUp;
    public float baseSpeed;
    public float timeUp;

    //PlayerMovement scripts
    private PlayerMovement pm;  

    /// <summary>
    /// Timer up when the player have the speedUp
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator Countdown()
    {
        float duration = timeUp;
        float totalTime = 0;
        while(totalTime <= duration)
        {
          totalTime += Time.deltaTime;
          yield return null;
        }
        EndSpeedUp();
    }

    /// <summary>
    /// Reset player speed and destroy the game object
    /// </summary>
    private void EndSpeedUp()
    {
        pm.runSpeed = baseSpeed;
        Destroy(gameObject);
    }

    /// <summary>
    /// Trigger when the player pass through the power up. 
    /// Set the new speed and destroy the collider and the sprite.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && gameObject.tag.Equals("SpeedUp"))
        {
            //Take the script PlayerMovement
            pm = collider.gameObject.GetComponent<PlayerMovement>();

            //Destroy the sprite
            Destroy(GetComponent<SpriteRenderer>());
            //Destroy collider
            Destroy(GetComponent<BoxCollider2D>());
            
            //Setup the speed
            pm.runSpeed = speedUp;
            //Start the timer
            FindObjectOfType<AudioManager>().Play("speed");
            StartCoroutine(Countdown());
        }
    }
}
