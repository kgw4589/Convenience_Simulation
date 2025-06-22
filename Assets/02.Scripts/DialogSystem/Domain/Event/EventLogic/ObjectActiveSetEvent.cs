using UnityEngine;

/// <summary>
/// 오브젝트 활성 상태 변경 이벤트
/// </summary>
public class ObjectActiveSetEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        // 오브젝트 활성 상태를 변경하기 위해 필요한 조건
        if (!IsObjectsActiveOk(eventElement.setActiveCheckingObjects, eventElement.objectsCheckValue))
        {
            return;
        }
        
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
    
    /// <summary>
    /// 조건 처리를 위해, 지정한 모든 오브젝트들의 활성 상태가 조건 값과 같은지 확인 
    /// </summary>
    /// <param name="gameObjects">활성 상태가 같은지 확인할 대상 오브젝트 리스트</param>
    /// <param name="value">활성 상태 조건 값</param>
    /// <returns></returns>
    private bool IsObjectsActiveOk(GameObject[] gameObjects, bool value)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].activeSelf != value)
            {
                return false;
            }
        }

        return true;
    }
}
