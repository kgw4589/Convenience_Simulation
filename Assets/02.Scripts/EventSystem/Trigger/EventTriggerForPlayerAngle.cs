using UnityEngine;

/// <summary>
/// 플레이어의 각도를 체크하여 이벤트를 실행하는 트리거 (시야에 따른 기능 호출 구현을 위함)
/// </summary>
public class EventTriggerForPlayerAngle : MonoBehaviour
{
    [SerializeField] private EventElement myEvent;

    [SerializeField] private LayerMask checkingLayer; 

    [Header("체크를 시작할 각도")]
    [SerializeField] private Vector2 checkAngle;
    
    [Header("준비가 완료되기까지 체크 각도에서 유지해야할 시간")]
    [SerializeField] private float maintainTime;
    private float _currentTime;

    [Header("준비 완료 시, 이 각도 범위 안으로 들어오면 실행")]
    [SerializeField] private Vector2 triggerAngle;

    private bool _isEnd = false;

    private void OnTriggerStay(Collider other)
    {
        if (_isEnd || GameManager.Instance.IsValidLayer(other.gameObject, checkingLayer))
        {
            return;
        }
        
        float playerY = other.transform.position.y;

        if (IsAngleInRange(playerY, checkAngle.x, checkAngle.y))
        {
            _currentTime += Time.deltaTime;
        }
        
        if (_currentTime < maintainTime)
        {
            return;
        }
        
        if (IsAngleInRange(playerY, triggerAngle.x, triggerAngle.y))
        {
            EventManager.Instance.PlayEvent(myEvent);
            _isEnd = true;
        }
    }
    
    private bool IsAngleInRange(float normalizedAngle, float minAngle, float maxAngle)
    {
        if (minAngle < maxAngle)
        {
            // 일반적인 경우
            return normalizedAngle >= minAngle && normalizedAngle <= maxAngle;
        }
        else
        {
            // 360도를 넘어가는 경우
            return normalizedAngle >= minAngle || normalizedAngle <= maxAngle;
        }
    }
}
