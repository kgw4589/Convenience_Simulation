using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "New Info/Item")]
public class ItemInfoScriptable : ScriptableObject
{
    public string itemName;
    public int itemPrice = 1000;
}
