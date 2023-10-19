using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Class <c>BasketDetectBall</c> detect when a ball pass through the basket
/// </summary>
public class BasketDetectBall : MonoBehaviour {
    
    public ChangingScore changingScore;
    public GameObject victoryBlue;
    public GameObject victoryPurple;
    public GameObject holderP1;
    public GameObject holderP2;

    private int blueScore = 0;
    private int violetScore = 0;
    private float cooldown = 5f;
    private bool isFinish = false;

    [SerializeField] 
    private GameObject currentBasket;

    /// <summary>
    /// Trigger when the ball hit the basket
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision){
        //Only if is a ball and the ball is in normal state
        if(collision.CompareTag("Ball") && !collision.gameObject.GetComponent<ExplosionBall>().isExplosion &&collision.transform.parent == null){
            if(currentBasket.CompareTag("BasketBlue")){
                if(violetScore < 5) {
                    //Add point
                    changingScore.ScoreVioletChange(++violetScore);
                    collision.gameObject.GetComponent<Ball>().SpawnBall();
                }
                
                if (violetScore >= 5){
                    //Purple win                    
                    isFinish = true;
                    victoryPurple.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("win_cheer");
                }
                
            }else if(currentBasket.CompareTag("BasketPurple")){                
                if(blueScore < 5) {
                    //Add point
                    changingScore.ScoreBlueChange(++blueScore);
                    collision.gameObject.GetComponent<Ball>().SpawnBall();
                }
                
                if (blueScore >= 5) {
                    //Blue win
                    isFinish = true;
                    victoryBlue.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("win_cheer");
                }
            }
            FindObjectOfType<AudioManager>().Play("whistle");
        }
    }

    /// <summary>
    /// When the game is finish after some times the end scene is display
    /// </summary>
    void Update(){
        if(isFinish){
            cooldown -= Time.deltaTime;
            holderP1.SetActive(false);
            holderP2.SetActive(false);
            if(cooldown <= 0){
                Destroy(GameObject.Find("Ball"));
                DontDestroyOnLoad(GameObject.Find("PlayerPurple"));
                DontDestroyOnLoad(GameObject.Find("PlayerBlue"));
                SceneManager.LoadScene(6);
            }
        }
    }
}
