using UnityEngine;

/// <summary>
/// 충돌 시 이벤트를 실행하는 트리거
/// </summary>
public class EventTriggerForCollision : BaseCollisionTrigger
{
    [SerializeField] private EventElement myEvent;
    [SerializeField] private EventElement exitEvent;

    protected override void TriggerEvent()
    {
        if (myEvent)
        {
            EventManager.Instance.PlayEvent(myEvent);
        }
    }
    
    protected override void TriggerExitEvent()
    {
        if (exitEvent)
        {
            EventManager.Instance.PlayEvent(exitEvent);
        }
    }
}
