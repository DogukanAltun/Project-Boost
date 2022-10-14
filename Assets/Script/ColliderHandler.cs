using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioCrash;
    [SerializeField] private AudioClip audioCongrats;
    [SerializeField] private ParticleSystem SuccessParticle;
    [SerializeField] private ParticleSystem FailParticle;
    private bool IsInteractioned = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!IsInteractioned)
        {
            switch (other.gameObject.tag)
            {
                case "Fuel":
                    Debug.Log("You are fuelling!");
                    other.gameObject.transform.SetParent(transform);
                    break;
                case "Start":
                    Debug.Log("You are into Starting Position");
                    break;
                case "Finish":
                    Debug.Log("Congrats! You Finished this level!");
                    WaitForNext();
                    break;
                default:
                    Debug.Log("SORRY! ,You Blew Up!");
                    WaitForLost();
                    break;
            }
        }
    }

    void WaitForNext()
    {
        IsInteractioned = true;
        audioSource.Stop();
        SuccessParticle.Play();
        audioSource.PlayOneShot(audioCongrats);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 2f);
    }
    void WaitForLost()
    {
        IsInteractioned = true;
        audioSource.Stop();
        FailParticle.Play();
        audioSource.PlayOneShot(audioCrash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);
    }
    private void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int NextLevelIndex = currentIndex + 1;
        if (NextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextLevelIndex = 0;
        }
        SceneManager.LoadScene(NextLevelIndex);
    }
    private void ReloadLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
