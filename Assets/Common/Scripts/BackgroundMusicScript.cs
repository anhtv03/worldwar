using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicScript : MonoBehaviour
{
    public AudioClip musicHome;
    public AudioClip musicMain;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = musicHome;
        audioSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex >= 4 && audioSource.clip != musicMain)
        {
            audioSource.clip = musicMain;
            audioSource.Play();
        }
        else if (scene.buildIndex < 4 && audioSource.clip != musicHome)
        {
            audioSource.clip = musicHome;
            audioSource.Play();
        }
    }
}
