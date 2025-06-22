using UnityEngine;

/// <summary>
/// SFX, BFX 실행, 중지
/// </summary>
public class SoundPlayCommand : ICommandable
{
    public void PlayCommand(DialogCommandData dialogCommandData)
    {
        if (dialogCommandData.soundType is DialogCommandData.SoundControlType.PlaySfx)
        {
            PlaySfx(dialogCommandData);
        }
        else if (dialogCommandData.soundType is DialogCommandData.SoundControlType.PlayBgm)
        {
            PlayBgm(dialogCommandData);
        }
        else if (dialogCommandData.soundType is DialogCommandData.SoundControlType.StopBgm)
        {
            StopBgm();
        }
    }

    private void PlaySfx(DialogCommandData dialogCommandData)
    {
        AudioClip audioClip = dialogCommandData.audioClip;
        float volume = dialogCommandData.soundVolume;
        
        GameManager.Instance.PlaySfx(audioClip, volume);
    }

    private void PlayBgm(DialogCommandData dialogCommandData)
    {
        AudioClip audioClip = dialogCommandData.audioClip;
        float volume = dialogCommandData.soundVolume;
        bool isLoop = dialogCommandData.isLoopSound;
        
        GameManager.Instance.PlayBgm(audioClip, volume, isLoop);
    }

    private void StopBgm()
    {
        GameManager.Instance.StopBgm();
    }
}
