using UnityEngine;
using UnityEngine.UI;

public class CounterManager : Singleton<CounterManager>
{
    public Transform itemNameParent;
    public Transform itemPriceParent;
    
    [Header("모니터에 출력될 것들 관련")]
    public Text itemInfoTextFactory;
    public Text totalText;

    private int _totalPrice = 0;

    public int TotalPrice
    {
        get
        {
            return _totalPrice;
        }
        set
        {
            _totalPrice = value;

            totalText.text = _totalPrice.ToString();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _totalPrice = 0;
    }

    public void OnScanBarcode(ItemInfoScriptable itemInfo)
    {
        Text itemNameText = Instantiate(itemInfoTextFactory, itemNameParent);
        Text itemPriceText = Instantiate(itemInfoTextFactory, itemPriceParent);

        itemNameText.text = itemInfo.itemName;
        itemPriceText.text = itemInfo.itemPrice.ToString();
        
        TotalPrice += itemInfo.itemPrice;
    }

    public virtual void ClearItem()
    {
        for (int i = 0; i < itemNameParent.childCount; i++)
        {
            Destroy(itemNameParent.GetChild(0));
            Destroy(itemPriceParent.GetChild(0));
        }
        
        _totalPrice = 0;
    }
}
