using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public static class GetPlayerInput : object
{
    private static PlayerInput playerBlue;
    private static PlayerInput playerPurple;

    /// <summary>
    /// Function used to get the player input from the blue player
    /// </summary>
    /// <returns>PlayerInput</returns>
    public static PlayerInput GetPlayerInputBlue(){
        if(playerBlue == null){            
            playerBlue = GameObject.Find("PlayerBlue").GetComponent<PlayerInput>();
        }
        return playerBlue;
    }

    /// <summary>
    /// Function used to get the player input from the purple player
    /// </summary>
    /// <returns>PlayerInput</returns>
    public static PlayerInput GetPlayerInputPurple(){
        if(playerPurple == null){
            playerPurple = GameObject.Find("PlayerPurple").GetComponent<PlayerInput>();
        }
        return playerPurple;
    }

    /// <summary>
    /// Get the player input from the gameobject specified
    /// </summary>
    /// <param name="player">The gameobject of the specified player</param>
    /// <returns>Player Input</returns>
    public static PlayerInput GetPlayerInputGM(GameObject player){
        if(player.name == "PlayerBlue"){
            return GetPlayerInputBlue();
        }else if(player.name == "PlayerPurple"){
            return GetPlayerInputPurple();
        }
        return null;
    }
}
