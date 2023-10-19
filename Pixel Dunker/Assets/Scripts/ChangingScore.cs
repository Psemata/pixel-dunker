using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingScore : MonoBehaviour {

    // The image which will held the sprites
    public Image scoreImage;
    // The different numbers
    public Sprite[] blueScores = new Sprite[6];
    public Sprite[] violetScores = new Sprite[6];

    // Change the display of the score for the blue team
    public void ScoreBlueChange(int score) {
        scoreImage.sprite = blueScores[score];
    }

    // Change the display of the score for the purple team
    public void ScoreVioletChange(int score) {
        scoreImage.sprite = violetScores[score]; 
    }
}
