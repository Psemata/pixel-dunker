using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Class <c>SettingsMenu</c> manager of the settings
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    //The two mixers of the game
    public AudioMixer soundsEffectsMixer;
    public AudioMixer musicMixer;

    public Slider soundsEffectsSlider;
    public Slider musicSlider;

    /// <summary>
    /// At the start set to the slider the volume of the mixers
    /// </summary>
    private void Start() {
        float volume = 0f;
        bool result = soundsEffectsMixer.GetFloat("SoundsEffects", out volume);
        soundsEffectsSlider.value = volume;
        result = musicMixer.GetFloat("Music", out volume);
        musicSlider.value = volume;
    }

    /// <summary>
    /// Set the value of the slider to the mixer
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumeSoundsEffects(float volume){
        if(volume > -10){
            soundsEffectsMixer.SetFloat("SoundsEffects", volume);
        }else{
            soundsEffectsMixer.SetFloat("SoundsEffects", -80);
        }
    }

    /// <summary>
    /// Set the value of the slider to the mixer
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumeMusic(float volume){
        if(volume > -10){
            musicMixer.SetFloat("Music", volume);
        }else{
            musicMixer.SetFloat("Music", volume);
        }
    }
}
