using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Ball</c> spawn the ball
/// </summary>
public class Ball : MonoBehaviour
{

    [SerializeField] private GameObject currentBall;
    public Vector3 spawnBall1;
    public Vector3 spawnBall2;


    /// <summary>
    /// Spawn the ball at two random postion
    /// </summary>
    public void SpawnBall(){
        int random = Random.Range(0, 2);
        Debug.Log(random);
        if(random == 0){
            transform.position = spawnBall1;
        }else{
            transform.position = spawnBall2;
        }
        currentBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        FindObjectOfType<AudioManager>().Play("ball");
    }
}
