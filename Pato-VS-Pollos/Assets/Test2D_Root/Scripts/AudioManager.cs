using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton Declaracion
    public static AudioManager Instance;
    [Header("Audio Source Ref")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip Arrays")]
    [SerializeField] AudioClip[] musicList;
    [SerializeField] AudioClip[] sfxList;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayMusic(int musicIndex) 
    {
        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }

    public void PlaySFX(int soundIndex) 
    {
        sfxSource.PlayOneShot(sfxList[soundIndex]);
    }

    public void StopMusic() 
    {
        musicSource.Stop();
    }
}
