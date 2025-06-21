using UnityEngine;

public abstract class BaseItem : MonoBehaviour, IItemable
{
    public ItemInfoScriptable itemInfo;
    
    public bool isScanned = false;

    protected Rigidbody Rigidbody;
    
    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        
        GrabbableSetter grabbableSetter = new GrabbableSetter();
        grabbableSetter.SetItem(transform, Rigidbody);
    }


    public virtual ItemInfoScriptable OnScanBarcode()
    {
        isScanned = true;
        
        return itemInfo;
    }
}
