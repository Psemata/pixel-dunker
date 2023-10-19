using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Armor</c> this a power up apply to the player and give an armor for one heart
/// </summary>
public class Armor : MonoBehaviour
{
    //UI where heart are display
    public GameObject UI;

    //PlayerMovement scripts
    private PlayerMovement pm;

    /// <summary>
    /// Trigger when the player pass through the power up. 
    /// Apply armor only if the player have not already 3 armors and destroy the collider and the sprite
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && gameObject.tag.Equals("Armor"))
        {
            //Take the script PlayerMovement
            pm = collider.gameObject.GetComponent<PlayerMovement>();
            
            if(pm.GetNbArmor() < 3){
                pm.SetNbArmor(pm.GetNbArmor()+1);
                FindObjectOfType<AudioManager>().Play("armor");
                //Destroy the sprite
                Destroy(GetComponent<SpriteRenderer>());
                //Destroy collider
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(gameObject);
            }        
        }
    }
}
