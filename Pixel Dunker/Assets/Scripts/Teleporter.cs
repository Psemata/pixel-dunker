using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Teleporter</c> save the destination and params of the teleporter
/// </summary>
public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private bool isUsed = false;
    private float cooldown = 10;
    public Animator animator;

    /// <summary>
    /// Get the destination
    /// </summary>
    /// <returns></returns>
    public Transform GetDestination(){
        return destination;
    }

    public bool IsUsed(){
        return isUsed;
    }

    public void SetUsed(bool used){
        isUsed = used;
        animator.SetBool("IsOff", true);
    }

    /// <summary>
    /// After some times reset the teleporter
    /// </summary>
    public void Update()
    {
        if(isUsed){
            cooldown -= Time.deltaTime;
            if(cooldown <= 0){
                cooldown = 10;
                isUsed = false;
                animator.SetBool("IsOff", false);
            }
        }
    }
}
