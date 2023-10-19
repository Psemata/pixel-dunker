using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private PlayerMovement pm;  
    public GameObject ui;  

    /// <summary>
    /// Trigger when the player pass through the power up.
    /// Give to the player the explosion power up
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && gameObject.tag.Equals("Explosion"))
        {
            //Take the script PlayerMovement
            pm = collider.gameObject.GetComponent<PlayerMovement>();
            
            if(pm.powerUp == 0){
                pm.powerUp = 2; 
                if(collider.gameObject.name == "PlayerBlue"){
                    GameObject.Find("Holder Player 1").transform.Find("Explosion").gameObject.SetActive(true);
                }else{
                    GameObject.Find("Holder Player 2").transform.Find("Explosion").gameObject.SetActive(true);
                }               
                //Destroy the game object
                Destroy(gameObject);
            }                                         
        }
    }
}
