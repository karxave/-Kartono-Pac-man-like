using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource; // AudioSource component to play sound
    [SerializeField] private AudioClip _loadClip;      // Clip to play before loading
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");

        _audioSource.PlayOneShot(_loadClip);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
