using System;

public abstract class BaseItem : BaseGrabbableObject, IItemable
{
    public ItemInfoScriptable itemInfo;
    
    public bool isScanned = false;

    protected void OnDisable()
    {
        isScanned = false;
    }

    public virtual ItemInfoScriptable OnScanBarcode()
    {
        isScanned = true;
        
        return itemInfo;
    }
}
