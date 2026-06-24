using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    [Header("Equipped Items")]
    public InventoryInfo currentWeapon;
    public InventoryInfo currentHelmet;
    public InventoryInfo currentChest;
    public InventoryInfo currentPants;

    public UnityEvent OnEquipmentChanged;

    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        _inventory.onObjectEquipped.AddListener(EquipItem);
    }

    private void OnDisable()
    {
        _inventory.onObjectEquipped.RemoveListener(EquipItem);
    }

    public void EquipItem(InventoryInfo item)
    {
        switch (item.objectType)
        {
            case InventoryInfo.InventoryObjectType.Weapon:
                currentWeapon = item;
                break;
            case InventoryInfo.InventoryObjectType.Helmet:
                currentHelmet = item;
                break;
            case InventoryInfo.InventoryObjectType.Chest:
                currentChest = item;
                break;
            case InventoryInfo.InventoryObjectType.Pants:
                currentPants = item;
                break;
            default:
                return;
        }

        OnEquipmentChanged?.Invoke();
    }
}