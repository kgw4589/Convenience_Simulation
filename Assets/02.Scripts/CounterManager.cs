using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CounterManager : Singleton<CounterManager>
{
    public Transform itemNameParent;
    public Transform itemPriceParent;

    [Header("카드 삽입 체크")]
    public Transform overlapOrigin;
    public LayerMask checkingLayer;
    private float _overlapRadius = 1f;
    private GameObject _equippedCard = null;
    
    [Header("모니터에 출력될 것들 관련")]
    public Text itemInfoTextFactory;
    public Text totalText;

    public AudioClip chaChingSound;

    private List<BaseItem> _scannedItems = new List<BaseItem>();

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
        _equippedCard = null;
    }

    public void OnScanBarcode(BaseItem item)
    {
        Text itemNameText = Instantiate(itemInfoTextFactory, itemNameParent);
        Text itemPriceText = Instantiate(itemInfoTextFactory, itemPriceParent);

        ItemInfoScriptable itemInfo = item.OnScanBarcode();

        itemNameText.text = itemInfo.itemName;
        itemPriceText.text = itemInfo.itemPrice.ToString();
        
        _scannedItems.Add(item);
        
        TotalPrice += itemInfo.itemPrice;
    }
    
    public void InsertCard()
    {
        Collider[] hits = Physics.OverlapSphere(overlapOrigin.position, _overlapRadius, checkingLayer);

        if (hits.Length > 0)
        {
            _equippedCard = hits[0].gameObject;
            Debug.Log("감지된 오브젝트: " + _equippedCard.name);
        }
        else
        {
            Debug.LogWarning("감지된 오브젝트가 없습니다.");
            _equippedCard = null;
        }
    }
    
    public void RemoveObject()
    {
        _equippedCard = null;
    }

    public bool IsBuyOk()
    {
        return _equippedCard != null && _scannedItems.Count >= 1;
    }

    public void OnOk()
    {
        if (!IsBuyOk())
        {
            return;
        }
        
        GameManager.Instance.PlaySfx(chaChingSound);
        
        DisableScannedItem();
        _equippedCard.SetActive(false);
    }

    public void ClearItem()
    {
        for (int i = 0; i < _scannedItems.Count; i++)
        {
            Destroy(itemNameParent.GetChild(i).gameObject);
            Destroy(itemPriceParent.GetChild(i).gameObject);

            _scannedItems[i].InitItem();
        }

        totalText.text = "0";
        
        _scannedItems.Clear();
        
        _totalPrice = 0;
    }

    public void DisableScannedItem()
    {
        for (int i = 0; i < _scannedItems.Count; i++)
        {
            Destroy(itemNameParent.GetChild(i).gameObject);
            Destroy(itemPriceParent.GetChild(i).gameObject);

            _scannedItems[i].gameObject.SetActive(false);
        }

        totalText.text = "0";
        
        _scannedItems.Clear();
        
        _totalPrice = 0;
    }
}
