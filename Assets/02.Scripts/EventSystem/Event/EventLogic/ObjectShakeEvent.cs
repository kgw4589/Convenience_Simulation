using System.Collections;
using UnityEngine;

/// <summary>
/// 오브젝트 흔들림 이벤트
/// </summary>
public class ObjectShakeEvent : IEventable
{
    public void EventAction(EventElementData eventElement)
    {
        Transform target = eventElement.shakeTarget;

        float duration = eventElement.shakeDuration;      // 흔들리는 시간 (초) - 추천: 0.5 ~ 2.0
        float amplitudeGain = eventElement.amplitudeGain; // 흔들림 세기 (진폭) - 추천: 0.05 ~ 0.5
        float frequencyGain = eventElement.frequencyGain; // 초당 흔들림 횟수 (빈도) - 추천: 10 ~ 60
        float lossRate = eventElement.lossRate;           // 감쇠율 (시간에 따른 흔들림 감소 속도) - 추천: 1.0 ~ 5.0

        GameManager.Instance.StartCoroutine(ShakeObject(target, duration, amplitudeGain, frequencyGain, lossRate));
    }

    private IEnumerator ShakeObject(Transform target, float duration, float amplitude, float frequency, float lossRate)
    {
        if (!target) yield break;

        Vector3 originalPos = target.localPosition;
        float elapsed = 0f;

        float shakeInterval = 1f / frequency;
        float shakeTimer = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            shakeTimer += Time.deltaTime;

            if (shakeTimer >= shakeInterval)
            {
                shakeTimer = 0f;

                float currentAmplitude = amplitude * Mathf.Exp(-lossRate * elapsed);
                float offsetX = (Random.value * 2 - 1) * currentAmplitude;
                float offsetY = (Random.value * 2 - 1) * currentAmplitude;
                float offsetZ = (Random.value * 2 - 1) * currentAmplitude;

                target.localPosition = originalPos + new Vector3(offsetX, offsetY, offsetZ);
            }

            yield return null; // 매 프레임마다 실행
        }

        target.localPosition = originalPos;
    }
}