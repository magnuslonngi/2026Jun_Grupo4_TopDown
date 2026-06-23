using UnityEngine;

[CreateAssetMenu(fileName = "InventoryInfo", menuName = "Scriptable Objects/InventoryInfo")]
public class InventoryInfo : ScriptableObject
{
    public enum InventoryObjectType
    {
        Health,
        Magic,
        Armor
    }

    public enum UsageType
    {
        Direct,
        OnCollect,
        InInventory
    }

    public InventoryObjectType objectType;
    public UsageType usage;
    public float recovery = 1f;
    public Sprite sprite;
    public int remainingUseCount = 3;
}
