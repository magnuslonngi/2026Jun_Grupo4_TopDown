using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI Controls")]
    [SerializeField] Image image;
    [SerializeField] Button useButton;
    [SerializeField] Button equipButton;
    [SerializeField] Button trashButton;

    Inventory inventory;
    InventoryInfo inventoryInfo;

    private void OnEnable()
    {
        useButton.onClick.AddListener(OnUse);
        equipButton.onClick.AddListener(OnEquip);
        trashButton.onClick.AddListener(OnTrash);
    }

    private void OnDisable()
    {
        useButton.onClick.RemoveListener(OnUse);
        equipButton.onClick.RemoveListener(OnEquip);
        trashButton.onClick.RemoveListener(OnTrash);
    }

    public void Initialize(Inventory _inventory, InventoryInfo _inventoryInfo)
    {
        _inventoryInfo = Instantiate(_inventoryInfo);

        inventoryInfo = _inventoryInfo;
        inventory = _inventory;
        image.sprite = _inventoryInfo.sprite;
    }

    private void OnUse()
    {
        Debug.Log("Using object: " + this);
        inventory.NotifyObjectUsed(inventoryInfo);
        inventoryInfo.remainingUseCount--;
        if (inventoryInfo.remainingUseCount <= 0)
            { Destroy(gameObject); }
    }

    public void OnEquip()
    {
        Debug.Log("Equiping object: " + this);
    }

    private void OnTrash()
    {
        Destroy(gameObject);
    }
}
