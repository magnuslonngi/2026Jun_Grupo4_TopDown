using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent<InventoryInfo> onObjectUsed;
    public UnityEvent<InventoryInfo> onObjectEquipped;

    public void NotifyObjectUsed(InventoryInfo inventoryInfo)
    {
        onObjectUsed?.Invoke(inventoryInfo);
    }

    public void NotifyObjectEquipped(InventoryInfo inventoryInfo)
    {
        onObjectEquipped?.Invoke(inventoryInfo);
    }
}
