using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayers : MonoBehaviour {

    // Images containing the heart sprites of the two players
    public Image[] heartsBlue = new Image[3];
    public Image[] heartsViolet = new Image[3];
    // Armored heart sprite
    public Sprite armorSprite;

    // Normal heart sprite
    public Sprite healthSprite;

    // Change the hearts display depending on how much health left
    public void healthLossBlue(int health) {
        heartsBlue[health].enabled = false;
    }
    public void healthLossViolet(int health) {
        heartsViolet[health].enabled = false;
    }

    // Transform an heart into an armored heart
    public void armorGainBlue(int health) {
        heartsBlue[health].sprite = armorSprite;
    }
    public void armorGainViolet(int health) {
        heartsViolet[health].sprite = armorSprite;
    }

    // Transform an armored heart into an heart
    public void armorLossBlue(int armor) {
        for(int i = 2; i >= armor; i--){
            heartsBlue[i].sprite = healthSprite;
        }
    }
    public void armorLossViolet(int armor) {
        for(int i = 2; i >= armor; i--){
            heartsViolet[i].sprite = healthSprite;
        }
    }

    // Heal all the hearts
    public void recoverBlue() {
        for(int i = 0; i < 3; i ++) {
            heartsBlue[i].enabled = true;
        }
    }
    public void recoverViolet() {
        for(int i = 0; i < 3; i ++) {
            heartsViolet[i].enabled = true;
        }
    }


}
