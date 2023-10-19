using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PlayerTeleport</c> teleport player into teleporter
/// </summary>
public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    /// <summary>
    /// Teleport the player if a teleporter has been trigger and not used
    /// </summary>
    void Update()
    {
        if(currentTeleporter != null && !currentTeleporter.GetComponent<Teleporter>().IsUsed()){
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            currentTeleporter.GetComponent<Teleporter>().SetUsed(true);
            currentTeleporter.GetComponent<Teleporter>().GetDestination().GetComponent<Teleporter>().SetUsed(true);
            currentTeleporter = null;
            FindObjectOfType<AudioManager>().Play("teleport");
        }
    }

    /// <summary>
    /// Trigger when the player cross the collider of the a tp  not used.
    /// Take the GameObject of the teleporter trigger only if is not already used
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision){       
        if(collision.CompareTag("TP") && !collision.gameObject.GetComponent<Teleporter>().IsUsed()){
            currentTeleporter = collision.gameObject;
        }        
    }
}
