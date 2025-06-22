using System;
using UnityEngine;

/// <summary>
/// 충돌 트리거 기본 클래스
/// </summary>
public abstract class BaseCollisionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask checkingLayer;

    protected bool isEnterPlayed = false;
    protected bool isExitPlayed = false;
    
    /// <summary>
    /// 충돌체 유효성 검사
    /// </summary>
    /// <param name="other">충돌체</param>
    private bool IsValidCollision(Collider other)
    {
        return GameManager.Instance.IsValidLayer(other.gameObject, checkingLayer);
    }

    protected void OnEnable()
    {
        isEnterPlayed = false;
        isExitPlayed = false;
    }

    /// <summary>
    /// 충돌체가 트리거에 진입했을 때 호출
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidCollision(other) || isEnterPlayed)
        {
            return;
        }

        isEnterPlayed = true;
        TriggerEvent();
    }

    /// <summary>
    /// 충돌체가 트리거에서 나갔을 때 호출
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (!IsValidCollision(other) || isExitPlayed)
        {
            return;
        }

        isExitPlayed = true;
        TriggerExitEvent();
    }

    protected abstract void TriggerEvent();
    protected virtual void TriggerExitEvent() { }

}
