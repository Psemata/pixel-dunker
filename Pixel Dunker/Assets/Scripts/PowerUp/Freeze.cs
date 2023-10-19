using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Freeze</c>, this a power up apply to the opposite player and he can't move fews secondes
/// </summary>
public class Freeze : MonoBehaviour
{
    private PlayerMovement pm;
    public GameObject ui;

    /// <summary>
    /// Trigger when the player pass through the power up.
    /// Give to the player the freeze power up
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && gameObject.tag.Equals("Frozen"))
        {
            //Take the script PlayerMovement
            pm = collider.gameObject.GetComponent<PlayerMovement>();

            if(pm.powerUp == 0){
                pm.powerUp = 1;   
                if(collider.gameObject.name == "PlayerBlue"){
                    GameObject.Find("Holder Player 1").transform.Find("Frozen").gameObject.SetActive(true);
                }else{
                    GameObject.Find("Holder Player 2").transform.Find("Frozen").gameObject.SetActive(true);
                }
                //Destroy the game object
                Destroy(gameObject);
            }                  
        }
    }
}
