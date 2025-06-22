using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> allEvents;
    
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource bgmAudioSource;
    
    public bool IsValidLayer(GameObject target, LayerMask layerMask)
    {
        int checkingLayer = layerMask.value;
        
        return (checkingLayer & (1 << target.layer)) != 0;
    }

    public void PlaySfx(AudioClip audioClip, float volume = 1f)
    {
        sfxAudioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayBgm(AudioClip audioClip, float volume = 1f, bool isLoop = false)
    {
        bgmAudioSource.clip = audioClip;
        bgmAudioSource.volume = volume;
        bgmAudioSource.loop = isLoop;

        if (bgmAudioSource.clip)
        {
            bgmAudioSource.Play();
        }
    }

    public void StopBgm()
    {
        bgmAudioSource.Stop();
        
        bgmAudioSource.clip = null;
        bgmAudioSource.volume = 1f;
        bgmAudioSource.loop = false;
    }
}

public class CustomerRoot
{
    public GameObject customer;
    public List<Transform> rootPositions;
    public List<GameObject> enableEvents;
}
