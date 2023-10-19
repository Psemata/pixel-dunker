using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>Menu</c>, manager of the menu
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Go to lobby scene
    /// </summary>
    public void PlayGame()
    {
        if(GameObject.Find("PlayerBlue")){
            Destroy(GameObject.Find("PlayerBlue"));
        }
        if(GameObject.Find("PlayerPurple")){
            Destroy(GameObject.Find("PlayerPurple"));
        }
        
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Play the selected sound
    /// </summary>
    public void PlayMenuSelectedSound() {
        FindObjectOfType<AudioManager>().Play("select");
    }

    /// <summary>
    /// Go to main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Got to controls menu
    /// </summary>
    public void ControlsMenu()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Go to options menu
    /// </summary>
    public void OptionsMenu()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Go to credits menu
    /// </summary>
    public void CreditsMenu()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Go to main scene
    /// </summary>
    public void ReMatch()
    {
        SceneManager.LoadScene(5);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void QuitGame(){
        Application.Quit();
    }
}
