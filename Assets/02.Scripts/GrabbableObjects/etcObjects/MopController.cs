using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopController : BaseGrabbableObject
{
    public LayerMask checkingLayer;

    private bool _isPlayed = false;

    [SerializeField] private AudioClip cleaningSound;
    [SerializeField] private List<EventElementData> myEvent;

    protected override void Awake()
    {
        base.Awake();

        _isPlayed = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsValidLayer(other.gameObject, checkingLayer) || _isPlayed)
        {
            return;
        }

        _isPlayed = true;
        GameManager.Instance.PlaySfx(cleaningSound);
        StartCoroutine(OnCleaning(other.gameObject));
    }

    private IEnumerator OnCleaning(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (!renderer)
        {
            yield break;
        }

        Material mat = renderer.material;
        if (!mat.HasProperty("_Color"))
        {
            yield break;
        }

        // 렌더링 모드를 투명하게 설정
        SetMaterialToFadeMode(mat);

        Color color = mat.color;
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        EventManager.Instance.PlayEventByData(myEvent);
        Destroy(target);
    }

    private void SetMaterialToFadeMode(Material mat)
    {
        mat.SetFloat("_Mode", 2); // Fade
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
