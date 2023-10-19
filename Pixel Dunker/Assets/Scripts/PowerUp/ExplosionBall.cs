using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ExplosionBall</c> charging and explosion of the ball
/// </summary>
public class ExplosionBall : MonoBehaviour
{
    public float timerCharge;
    public float timeExplosion;
    public bool isExplosion = false;
    public Animator animatorBall;
    private CircleCollider2D colliderExplosion;  

    /// <summary>
    /// Coroutine timer when the ball is charging
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator CountdownCharge()
    {
        float duration = timerCharge;
        float totalTime = 0;
        while(totalTime <= duration)
        {
          totalTime += Time.deltaTime;
          yield return null;
        }
        Explosion();
    }

    /// <summary>
    /// Coroutine when the ball explode
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator CountdownExplosion()
    {
        float duration = timeExplosion;
        float totalTime = 0;
        while(totalTime <= duration)
        {
          totalTime += Time.deltaTime;
          yield return null;
        }
        EndExplosion();
    }

    /// <summary>
    /// When the ball explode
    /// </summary>
    private void Explosion()
    {   
        //Add a circle collider
        //This is the impact collider
        colliderExplosion = gameObject.AddComponent<CircleCollider2D>();
        colliderExplosion.isTrigger = true;
        colliderExplosion.radius = 5;
        isExplosion = true;
        
        //Change the animation
        animatorBall.SetBool("IsExplosion", true);
        FindObjectOfType<AudioManager>().Play("explosion");
        StartCoroutine(CountdownExplosion());
    }

    /// <summary>
    /// After the explosion
    /// </summary>
    private void EndExplosion()
    {
        isExplosion = false;
        //Reset animation
        animatorBall.SetBool("IsExplosion", false);
        animatorBall.SetBool("IsCharging", false);
        //Destroy the impact collider
        Destroy(colliderExplosion);
    }

    /// <summary>
    /// When the player press the button to activate the power up
    /// </summary>
    /// <param name="player"></param>
    public void StartExplosion(GameObject player)
    {
        if(player.name == "PlayerBlue"){
            GameObject.Find("Holder Player 2").transform.Find("Explosion").gameObject.SetActive(false);
        }else{
            GameObject.Find("Holder Player 1").transform.Find("Explosion").gameObject.SetActive(false);
        }

        //Display the ball
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //Change the animation
        animatorBall.SetBool("IsCharging", true);
        StartCoroutine(CountdownCharge());
    }

    /// <summary>
    /// When a player touch the impact collider
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isExplosion && collider.tag.Equals("Player"))
        {            
            //The player take 3 damages
            collider.gameObject.GetComponent<PlayerMovement>().TakeDamage(3);
        }
    }
}
