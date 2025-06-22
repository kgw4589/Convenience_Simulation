using UnityEngine;

/// <summary>
/// 오브젝트 활성 상태 변경 이벤트
/// </summary>
public class ObjectActiveSetEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        string targetObjectName = eventElement.targetObjectName;
        
        if (!eventElement.setActiveTargetObject)
        {
            eventElement.setActiveTargetObject = FindInactiveObject(targetObjectName);
        }
        
        if (!eventElement.setActiveTargetObject)
        {
            Debug.LogError("찾을 수 없는 오브젝트입니다.");
            return;
        }
        
        eventElement.setActiveTargetObject.SetActive(eventElement.setActiveValue);
    }
    
    private GameObject FindInactiveObject(string targetObjectName)
    {
        // Hierarchy에서 비활성화된 오브젝트를 검색
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.name == targetObjectName && t.gameObject.hideFlags == HideFlags.None)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
