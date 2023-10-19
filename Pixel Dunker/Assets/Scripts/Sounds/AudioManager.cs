using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    // List of sounds used in the game
    public Sound[] sounds;

    // Singleton instance
    public static AudioManager instance;

    /// <summary>
    /// Awake is a Unity function called just before the Start function
    /// Here it is used to intialize the different sounds used in the game
    /// </summary>
    void Awake() {

        // If the instance exist then use it
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        // Line used to not destroy the AudioManager when the user changes scene
        DontDestroyOnLoad(gameObject);

        // Initialize all the sounds put in unity
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;            
        }
    }   

    // Play the theme song
    void Start() {
        Play("theme");
    }

    /// <summary>
    /// Play the sound with the specified name
    /// </summary>
    /// <param name="name">name of the sound</param>
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.source == null || s == null) {
            Debug.LogWarning("Le son n'a pas été trouvé");
            return;
        } else {
            s.source.Play();
        }
    }
}
