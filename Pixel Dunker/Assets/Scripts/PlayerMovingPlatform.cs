using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingPlatform : MonoBehaviour
{
    /// <summary>
    /// Add the plateform parent to the player
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    /// <summary>
    /// Remove the parent
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
