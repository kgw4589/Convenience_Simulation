using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// 오브젝트 위치 이동 이벤트
/// </summary>
public class ObjectMoveEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        GameManager.Instance.StartCoroutine(Move(eventElement));
    }

    private IEnumerator Move(EventElementData eventElement)
    {
       Vector3 dirPosition = eventElement.moveDirTransform.position;

        float duration = eventElement.moveDuration;
        
        Vector3 startPosition = eventElement.moveTargetTransform.position;
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            eventElement.moveTargetTransform.localPosition = Vector3.Lerp(startPosition, dirPosition, currentTime / duration);

            yield return null;
        }
        
        eventElement.moveTargetTransform.localPosition = dirPosition;
    }
}
