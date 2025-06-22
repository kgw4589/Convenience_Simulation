using UnityEngine;

/// <summary>
/// 오브젝트 동적 생성 이벤트
/// </summary>
public class ObjectInstantiateEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        GameObject factory = eventElement.objectFactory;

        GameObject gameObject = Object.Instantiate(factory, eventElement.instantiateOrigin.position, eventElement.instantiateOrigin.rotation);
        gameObject.SetActive(true);
    }
}
