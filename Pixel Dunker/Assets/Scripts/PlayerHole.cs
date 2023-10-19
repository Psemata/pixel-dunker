using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PlayerHole</c> teleport player and ball into hole
/// </summary>
public class PlayerHole : MonoBehaviour
{
    private GameObject currentHole;

    /// <summary>
    /// Teleport the player if a teleporter has been trigger and not used
    /// </summary>
    void Update()
    {
        if(currentHole != null){
            Vector3 temp = currentHole.GetComponent<Hole>().GetDestination().position;

            if(currentHole.tag == "Hole"){
                if(temp.y < 0){
                    temp.y += 3;
                }else{
                    temp.y -= 3;
                }
            }else if(currentHole.tag == "Side"){
                if(temp.x < 0){
                    temp.x += 3;
                }else{
                    temp.x -= 3;
                }
            }
        
            transform.position = temp;
            currentHole = null;
        }
    }

    /// <summary>
    /// Trigger when the player cross the collider of a hole.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision){        
        if(collision.CompareTag("Hole") || collision.CompareTag("Side")){
            if(transform.parent == null){
                currentHole = collision.gameObject;
            }
        }        
    }
}
