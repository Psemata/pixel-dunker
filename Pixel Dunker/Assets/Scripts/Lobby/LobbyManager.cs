using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>LobbyManager</c>, manager of the lobby scene
/// </summary>
public class LobbyManager : MonoBehaviour
{

    //UI P1 and P2
    public GameObject P1;
    public GameObject P2;

    public GameObject backButton;
    public GameObject startGame;
    public GameObject control;

    public StartScript startScript;
    //Animator for the purple player
    public RuntimeAnimatorController animatorPurple;

    /// <summary>
    /// Trigger when a player join the game
    /// </summary>
    /// <param name="playerInput"></param>
    public void OnJoin(PlayerInput playerInput){
        DontDestroyOnLoad(playerInput.gameObject);
        if(P1.activeSelf){
            P1.SetActive(false);
            backButton.SetActive(false);
            playerInput.gameObject.name = "PlayerBlue";
            PlayerPosition(playerInput.gameObject, -15, -4);
        }else if(P2.activeSelf){
            P2.SetActive(false);            
            startGame.SetActive(true);
            control.SetActive(true);
            startScript.launchReady = true;
            playerInput.gameObject.name = "PlayerPurple";
            PlayerPosition(playerInput.gameObject, 15, -4);
            playerInput.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorPurple;
        }
        
    }

    /// <summary>
    /// Position the player at coords
    /// </summary>
    /// <param name="gm"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void PlayerPosition(GameObject gm, float x, float y){
        Vector2 temp = gm.transform.position;
        temp.x = x;
        temp.y = y;
        gm.transform.position = temp;
    }

    /// <summary>
    /// When a player disconnect with a controller
    /// </summary>
    /// <param name="playerInput"></param>
    public void OnQuit(PlayerInput playerInput){
        Destroy(playerInput.gameObject);
        Debug.Log("quit player, keyboard");
    }

    

}
