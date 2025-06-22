using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이벤트 관리 매니저
/// </summary>
public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<EventElementData.EventType, Func<IEventable>> _eventLogicDic = new()
    {
        { EventElementData.EventType.PlaySound , () => new SoundPlayEvent() },
        { EventElementData.EventType.PlayAnime , () => new ObjectAnimationPlayEvent() },
        { EventElementData.EventType.PlayDialog , () => new DialogPlayEvent() },
        { EventElementData.EventType.MoveObject , () => new ObjectMoveEvent() },
        { EventElementData.EventType.SetActiveObject , () => new ObjectActiveSetEvent() },
        { EventElementData.EventType.ShakeObject , () => new ObjectShakeEvent() },
        
    };

    public void PlayEvent(EventElement eventElement)
    {
        if (!eventElement)
        {
            return;
        }
        
        Debug.Log("이벤트 실행");
        GameManager.Instance.StartCoroutine(TreatEvents(eventElement));
    }

    private IEnumerator TreatEvents(EventElement eventElement)
    {
        float time = 0;
        int count = 0;

        while (count < eventElement.elementData.Count)
        {
            time += Time.deltaTime;

            yield return null;
            
            if (time >= eventElement.elementData[count].nextDelay)
            {
                IEventable eventLogic = _eventLogicDic[eventElement.elementData[count].myEventType].Invoke();
                eventLogic.EventAction(eventElement.elementData[count++]);
                
                time = 0;
            }
        }
    }
    
    public void PlayEventByData(List<EventElementData> eventElementDataList, Action onComplete = null)
    {
        Debug.Log("데이터 기반 이벤트 실행");
        GameManager.Instance.StartCoroutine(TreatEventsByData(eventElementDataList, onComplete));
    }

    private IEnumerator TreatEventsByData(List<EventElementData> eventElementDataList, Action onComplete)
    {
        if (eventElementDataList == null)
        {
            yield break;
        }
        
        float time = 0;
        int count = 0;

        while (count < eventElementDataList.Count)
        {
            time += Time.deltaTime;

            yield return null;
            
            if (time >= eventElementDataList[count].nextDelay)
            {
                IEventable eventLogic = _eventLogicDic[eventElementDataList[count].myEventType].Invoke();
                eventLogic.EventAction(eventElementDataList[count++]);
                
                time = 0;
            }
        }
        
        onComplete?.Invoke();
    }
}
