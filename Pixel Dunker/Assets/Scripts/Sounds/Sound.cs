using UnityEngine.Audio;
using UnityEngine;

// System.Serializable helps to use the class Sound as a class usable in unity inspector
[System.Serializable]
public class Sound {

    // Name of the sound
    public string name;

    // The audio clip which will be used
    public AudioClip clip;

    // The volume of the sound (range between 0 and 1)
    [Range(0f, 1f)]
    public float volume;

    // The pitch of the sound (range between 0.1 and 3)
    [Range(.1f, 3f)]
    public float pitch;

    // If the sound can be looped
    public bool loop;
    
    // The mixer group of the sound
    public AudioMixerGroup mixer;

    // The source of the sound
    [HideInInspector]
    public AudioSource source;
}
