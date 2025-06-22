/// <summary>
/// 다이얼로그 실행 이벤트
/// </summary>
public class DialogPlayEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        DialogueManager.Instance.PlayScene(eventElement.storyScene);
    }
}
