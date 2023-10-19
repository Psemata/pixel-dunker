using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Hole</c>, save the destination of the hole
/// </summary>
public class Hole : MonoBehaviour
{
    [SerializeField] private Transform destination;

    /// <summary>
    /// return the destination
    /// </summary>
    /// <returns></returns>
    public Transform GetDestination(){
        return destination;
    }
}
