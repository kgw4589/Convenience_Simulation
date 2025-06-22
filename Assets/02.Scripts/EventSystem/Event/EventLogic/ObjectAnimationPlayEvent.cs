/// <summary>
/// 오브젝트 애니메이션 실행
/// </summary>
public class ObjectAnimationPlayEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        eventElement.objectAnimator.SetTrigger(eventElement.animParamName);
    }
}
