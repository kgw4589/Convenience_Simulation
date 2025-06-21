using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Transform rayOrigin;
    
    private AudioSource _audioSource;
    public AudioClip interactSound;
    
    public LayerMask checkingLayerMask;

    private float _rayRange = 0.7f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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

    private void OnDrawGizmos()
    {
        if (rayOrigin == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward);
    }
}
