using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class <c>SetupGame</c> setup the configuration for a game
/// </summary>
public class SetupGame : MonoBehaviour {
        
    public Ball ball;
    public Countdown countdown;
    public GameObject countDownGO;
    public GameObject scores;
    public GameObject HP1;
    public GameObject HP2;

    private GameObject playerBlue;
    private GameObject playerPurple;
    private bool isFinish = false;

    /// <summary>
    /// Call at start 
    /// </summary>
    void Start()
    {
        scores.SetActive(false);
        //Find players
        playerBlue = GameObject.Find("PlayerBlue");        
        playerPurple = GameObject.Find("PlayerPurple");    

        //Add the script for the healt points and select the good team
        playerBlue.GetComponent<PlayerMovement>().healthPlayers = HP1.GetComponent(typeof(HealthPlayers)) as HealthPlayers;
        playerBlue.GetComponent<PlayerMovement>().team = 0;
        playerPurple.GetComponent<PlayerMovement>().healthPlayers = HP2.GetComponent(typeof(HealthPlayers)) as HealthPlayers;
        playerPurple.GetComponent<PlayerMovement>().team = 1;

        //Diasable inputs for the players
        DisableInput(true);
        //Sapwn the ball
        ball.SpawnBall();
        //Spawns the players
        SpawnPlayers();        
    }

    /// <summary>
    /// Display the timer and at the end the players can move
    /// </summary>
    void Timer(){
        countDownGO.SetActive(true);
        //Start the timer
        countdown.StartTimer();
        //If the timer has finish
        if(countdown.timerDone){
            scores.SetActive(true);
            countDownGO.SetActive(false);
            //Enable PlayerInput
            DisableInput(false);
            isFinish = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFinish){
            Timer();
        }            
    }

    /// <summary>
    /// Position the players, flip the players and reset animation
    /// </summary>
    void SpawnPlayers(){
        Vector2 temp = playerBlue.transform.position;
        temp.x = -27;
        temp.y = 3;        
        playerBlue.transform.position = temp;
        Vector2 tempScale = playerBlue.transform.localScale;
        tempScale.x = 1;
        playerBlue.transform.localScale = tempScale;
        playerBlue.GetComponent<CharacterController2D>().m_FacingRight = true;
        playerBlue.GetComponent<Animator>().Play("Player_Blue_Idle");
        playerBlue.GetComponent<Animator>().SetBool("HasBall", false);
        temp.x = 28;
        temp.y = 3;
        playerPurple.transform.position = temp;
        tempScale = playerPurple.transform.localScale;
        tempScale.x = -1;
        playerPurple.transform.localScale = tempScale;
        playerPurple.GetComponent<CharacterController2D>().m_FacingRight = false;
        playerPurple.GetComponent<Animator>().Play("Player_Purple_Idle");
        playerPurple.GetComponent<Animator>().SetBool("HasBall", false);
    }

    /// <summary>
    /// Disable or enable PlayerInput
    /// </summary>
    /// <param name="disable"></param>
    void DisableInput(bool disable){
        if(disable){
            GetPlayerInput.GetPlayerInputBlue().actions.Disable();
            GetPlayerInput.GetPlayerInputPurple().actions.Disable();
        }else{
            GetPlayerInput.GetPlayerInputBlue().actions.Enable();
            GetPlayerInput.GetPlayerInputPurple().actions.Enable();
        }
    }
}
