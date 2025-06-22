/// <summary>
/// SFX, BGM 플레이 이벤트
/// </summary>
public class SoundPlayEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        if (eventElement.soundType is EventElementData.SoundType.Sfx)
        {
            GameManager.Instance.PlaySfx(eventElement.audioClip, eventElement.audioVolume);
        }
        else if (eventElement.soundType is EventElementData.SoundType.BgmPlay)
        {
            GameManager.Instance.PlayBgm(eventElement.audioClip, eventElement.audioVolume);
        }
        else if (eventElement.soundType is EventElementData.SoundType.BgmStop)
        {
            GameManager.Instance.StopBgm();
        }
    }
}
