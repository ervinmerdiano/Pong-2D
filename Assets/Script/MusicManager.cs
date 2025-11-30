using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    private bool isMusicOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }
    
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        musicOnButton.SetActive(isMusicOn);
        musicOffButton.SetActive(!isMusicOn);
    }
}
