using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> allEvents;

    public StoryScene successStory;
    
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource bgmAudioSource;

    public readonly int dirScore = 2;
    public int currentScore = 0;
        
    private void Start()
    {
        InitEvents();
    }
    
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
    
    public void InitEvents()
    {
        for (int i = 0; i < allEvents.Count; i++)
        {
            allEvents[i].SetActive(false);
        }
        
        CustomerManager.Instance.ReadyCustomer();
    }

    public void ClearCustomer()
    {
        ++currentScore;

        if (currentScore >= dirScore)
        {
            DialogueManager.Instance.PlayScene(successStory);
            return;
        }
        
        InitEvents();
    }
}
