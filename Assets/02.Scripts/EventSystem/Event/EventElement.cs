using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 이벤트 자체 데이터 설정 컴포넌트 
/// </summary>
public class EventElement : MonoBehaviour
{
    public List<EventElementData> elementData;
}

/// <summary>
/// 이벤트에 필요한 세부 데이터 정의
/// </summary>
[Serializable]
public class EventElementData
{
    public string elementMemo;          // 단순 메모 추가
    public float nextDelay = 0.0f;
    
    public enum EventType
    {
        PlaySound,
        PlayAnime,
        PlayDialog,
        MoveObject,
        SetActiveObject,
        InstantiateObject,
        ShakeObject,
        CustomerControl,
    }
    
    public EventType myEventType;

    #region Data-PlaySound

    public enum SoundType
    {
        Sfx, BgmPlay, BgmStop
    }

    [DrawIf("myEventType", EventType.PlaySound)]
    public SoundType soundType = SoundType.Sfx;
    
    [DrawIf("myEventType", EventType.PlaySound)]
    public AudioClip audioClip;
    
    [DrawIf("myEventType", EventType.PlaySound)]
    [Range(0, 1)]
    public float audioVolume = 1.0f;
    
    #endregion
    
    #region Data-MoveObject

    [DrawIf("myEventType", EventType.MoveObject)]
    public Transform moveTargetTransform;
    
    [DrawIf("myEventType", EventType.MoveObject)]
    public Transform moveDirTransform;

    [DrawIf("myEventType", EventType.MoveObject)]
    public float moveDuration;
    
    #endregion

    #region Data-PlayAnime
    
    [DrawIf("myEventType", EventType.PlayAnime)]
    public bool isCurrentCustomer = true;

    [DrawIf("myEventType", EventType.PlayAnime)]
    [DrawIf("isCurrentCustomer", false)]
    public Animator objectAnimator;
    
    [FormerlySerializedAs("paramName")] [DrawIf("myEventType", EventType.PlayAnime)]
    public string animParamName;
    
    #endregion

    #region Data-PlayDialog
    [DrawIf("myEventType", EventType.PlayDialog)]
    public StoryScene storyScene;
    #endregion
    
    #region Data-SetActiveObject
    
    [DrawIf("myEventType", EventType.SetActiveObject)]
    public GameObject setActiveTargetObject;
    
    [DrawIf("myEventType", EventType.SetActiveObject)]
    public bool setActiveValue;
    
    [DrawIf("myEventType", EventType.SetActiveObject)]
    public string targetObjectName;
    
    #endregion

    #region Data-ObjectInstantiate

    [DrawIf("myEventType", EventType.InstantiateObject)]
    public GameObject objectFactory;

    [DrawIf("myEventType", EventType.InstantiateObject)]
    public Transform instantiateOrigin;
    
    #endregion

    #region Data-PlayerCamera
    
    [DrawIf("myEventType", EventType.ShakeObject)]
    public Transform shakeTarget;                         // 흔들릴 대상 오브젝트

    [DrawIf("myEventType", EventType.ShakeObject)]
    public float shakeDuration = 0.5f;                    // 흔들리는 시간 (초) - 추천: 0.5 ~ 2.0

    [DrawIf("myEventType", EventType.ShakeObject)]
    public float amplitudeGain = 0.05f;                    // 흔들림 세기 (진폭) - 추천: 0.05 ~ 0.5

    [DrawIf("myEventType", EventType.ShakeObject)]
    public float frequencyGain = 10f;                    // 초당 흔들림 횟수 (빈도) - 추천: 10 ~ 60

    [DrawIf("myEventType", EventType.ShakeObject)]
    public float lossRate = 1.0f;                         // 감쇠율 (진폭이 줄어드는 속도) - 추천: 1.0 ~ 5.0

    #endregion
    
    #region Data-CustomerControl

    public enum CustomerControlType
    {
        Ready,
        Start,
        End,
    }

    [DrawIf("myEventType", EventType.CustomerControl)]
    public CustomerControlType customerControlType;

    #endregion
}
