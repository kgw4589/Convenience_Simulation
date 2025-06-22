using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Transform rayOrigin;
    
    public AudioClip interactSound;
    
    public LayerMask checkingLayerMask;

    private float _rayRange = 0.7f;
    
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
                
                CounterManager.Instance.OnScanBarcode(item);

                if (interactSound)
                {
                    GameManager.Instance.PlaySfx(interactSound);
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
