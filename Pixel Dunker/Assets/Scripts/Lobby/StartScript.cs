using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private int nbPlayers = 0;

    public Countdown Countdown;
    public GameObject CountDownGO;
    public GameObject ControlGO;
    public bool launchReady = false;
    public GameObject ball;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider != null && collider.tag.Equals("Player"))
        {
            nbPlayers++;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if (collider != null && collider.tag.Equals("Player"))
        {
            nbPlayers--;
        }
    }

    void Timer(){
        if(nbPlayers == 2){
            CountDownGO.SetActive(true);
            ControlGO.SetActive(false);
            Countdown.StartTimer();
            if(Countdown.timerDone){
                SceneManager.LoadScene(5);
                Destroy(ball);
            }
        } else {
            CountDownGO.SetActive(false);
            ControlGO.SetActive(true);
            Countdown.StopTimer();
        }
    }


    void Update(){
        if(launchReady) {
            Timer();
        }        
    }
}
