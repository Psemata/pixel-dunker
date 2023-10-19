using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ReverseBaskets</c> this is a power up that reverse baskets position
/// </summary>
public class ReverseBaskets : MonoBehaviour
{
    //Stats and baskets
    public float timeUp;
    private GameObject blueBasket;
    private GameObject purpleBasket;

    //PlayerMovement scripts
    private PlayerMovement pm;  

    /// <summary>
    /// Find the two bakets game objects
    /// </summary>
    void Start()
    {
        blueBasket = GameObject.Find("BasketBlue");
        purpleBasket = GameObject.Find("BasketPurple");
    }

    /// <summary>
    /// Timer up when the power up is up
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
        //reset the position
        ChangePosition();
        //Destoy the game object
        Destroy(gameObject);
    }

    /// <summary>
    /// Swap the position between the tow baskets
    /// </summary>
    void ChangePosition(){     
        FindObjectOfType<AudioManager>().Play("swap");   
        Vector3 tempPosBlue = blueBasket.transform.position;
        Vector3 tempPosPurple = purpleBasket.transform.position;
        Vector3 tempScaleBlue = blueBasket.transform.localScale;
        Vector3 tempScalePurple = purpleBasket.transform.localScale;
        blueBasket.transform.position = tempPosPurple;
        purpleBasket.transform.position = tempPosBlue;
        blueBasket.transform.localScale = tempScalePurple;
        purpleBasket.transform.localScale = tempScaleBlue;
    }

    /// <summary>
    /// Trigger when the player pass through the power up.
    /// Swap the positon of the tow baskets and destroy the collider and the sprite.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && gameObject.tag.Equals("Reverse"))
        {
            //Take the script PlayerMovement
            pm = collider.gameObject.GetComponent<PlayerMovement>();

            //Destroy the sprite
            Destroy(GetComponent<SpriteRenderer>());
            //Destroy collider
            Destroy(GetComponent<BoxCollider2D>());
            
            //Swap
            ChangePosition();

            //Start the timer
            StartCoroutine(Countdown());            
        }
    }
}
