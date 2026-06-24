using UnityEngine;
using UnityEngine.Events;

public class CharacterCollect : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemUIPrefab;
    [SerializeField] Transform itemsParent;

    public UnityEvent <CollectableObject> onColletedObjectDirectUsage;

    Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectableObject collectable = collision.GetComponent<CollectableObject>();
        if(collectable != null)
        {
            switch (collectable.inventoryInfo.usage)
            {
                case InventoryInfo.UsageType.Direct:
                    onColletedObjectDirectUsage.Invoke(collectable);
                    break;
                case InventoryInfo.UsageType.InInventory:
                    {
                        GameObject newItem = Instantiate(inventoryItemUIPrefab, itemsParent);
                        newItem.GetComponent<InventoryItem>().Initialize(inventory, collectable.inventoryInfo);
                    }
                    break;
            }
            collectable.NotifyCollected();
        }

    }

    public void CollectFromEquipment(InventoryInfo inventoryInfo)
    {
        GameObject newItem = Instantiate(inventoryItemUIPrefab, itemsParent);
        newItem.GetComponent<InventoryItem>().Initialize(inventory, inventoryInfo);
    }
}
