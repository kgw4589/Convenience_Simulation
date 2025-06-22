using System;
using UnityEngine;

public abstract class BaseItem : BaseGrabbableObject, IItemable
{
    [SerializeField] private ItemInfoScriptable itemInfo;
    
    public bool isScanned = false;

    protected void OnEnable()
    {
        InitItem();
    }

    public virtual void InitItem()
    {
        isScanned = false;
    }

    public virtual ItemInfoScriptable OnScanBarcode()
    {
        isScanned = true;
        
        return itemInfo;
    }
}
