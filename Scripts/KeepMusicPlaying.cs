using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepMusicPlaying : MonoBehaviour
{
    private static KeepMusicPlaying instance;
    private AudioSource music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            music = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // OnEnable() and OnDisable() are lifecycle methods that relate to 
    // Unity's event system for managing scenes (part of MonoBehavior)
    // Called when script is first enabled
    private void OnEnable()
    {
        // SceneManager.sceneLoaded is triggered every time a new scene is loaded. Whenever a new scene
        // is loaded, OnSceneLoaded method is called
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called when script is disabled or destroyed
    private void OnDisable()
    {
        // -= On SceneLoaded unsubscribes OnSceneLoaded method from sceneLoaded event. This is to prevent
        // memory leaks and unexpected behavior. If a method is not unsubscribed, the method could still be
        // called even after the object is destroyed, leading to errors
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is subscribed to the sceneManager.sceneLoaded event and will be called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            if (music.isPlaying)
            {
                // Pauses and resets the music when player enters the main menu so that it restarts during next game scene
                music.Pause();
                music.time = 0f;
            }
        }
        else
        {
            // Resume music from the beginning if it's not playing
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
    }
}
