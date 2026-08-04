using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] float levelLoadDelay = 2f;

    AudioSource audioSource;
    bool isControllable;
    private void Start()
    {
        isControllable = true;
        audioSource = GetComponent<AudioSource>();
    }

    
    void OnCollisionEnter(Collision other)
    {
        if(!isControllable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Finish");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCrashSequence();
                Debug.Log("Dead");
                break;
        }
    }

    void StartCrashSequence()
    {
        //todo add particle effect
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void StartSuccessSequence()
    {
        //todo add sfx and particle effect
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel(){
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}

