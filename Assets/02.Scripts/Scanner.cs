using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Transform rayOrigin;
    
    private AudioSource _audioSource;
    public AudioClip interactSound;
    
    public LayerMask checkingLayerMask;
    private int _checkingLayer;

    private float _rayRange = 0.7f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _checkingLayer = checkingLayerMask.value;
    }
    
    private void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayRange, checkingLayerMask))
        {
            if (hit.transform.TryGetComponent(out BaseItem item))
            {
                if (item.isScanned)
                {
                    return;
                }
                
                ItemInfoScriptable itemInfo = item.OnScanBarcode();
                
                CounterManager.Instance.OnScanBarcode(itemInfo);

                if (interactSound)
                {
                    _audioSource.PlayOneShot(interactSound);
                }
            }
        }
    }

    /// <summary>
    /// 레이어 체크 함수. 레이 외 다른 방식을 썼을 때를 위해.
    /// </summary>
    /// <param name="layer">체크할 레이어</param>
    /// <returns></returns>
    private bool IsValidLayer(int layer)
    {
        return (_checkingLayer & (1 << layer)) != 0;
    }
    
    private void OnDrawGizmos()
    {
        if (rayOrigin == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward);
    }
}
