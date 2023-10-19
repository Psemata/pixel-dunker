using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class <c>FreezePlayer</c> freeze the player by disable he PlayerInput
/// </summary>
public class FreezePlayer : MonoBehaviour
{
    public float timeUp;
    public GameObject ui;
    private GameObject player;    

    /// <summary>
    /// Coroutine, timer when the player is freeze
    /// </summary>
    /// <returns></returns>
    private IEnumerator Countdown()
    {
        float duration = timeUp;
        float totalTime = 0;
        while(totalTime <= duration)
        {
          totalTime += Time.deltaTime;
          yield return null;
        }
        EndFreeze();
    }

    /// <summary>
    /// Enable PlayerInput
    /// </summary>
    private void EndFreeze()
    {
        GetPlayerInput.GetPlayerInputGM(player).actions.Enable();
    }

    /// <summary>
    /// Disable PlayerInput
    /// </summary>
    /// <param name="player"></param>
    public void StartFreeze(GameObject player)
    {
        this.player = player;
        if(player.name == "PlayerBlue"){
            GameObject.Find("Holder Player 2").transform.Find("Frozen").gameObject.SetActive(false);
        }else{            
            GameObject.Find("Holder Player 1").transform.Find("Frozen").gameObject.SetActive(false);
        }
        GetPlayerInput.GetPlayerInputGM(player).actions.Disable();
        FindObjectOfType<AudioManager>().Play("freeze");
        StartCoroutine(Countdown());
    }
}
