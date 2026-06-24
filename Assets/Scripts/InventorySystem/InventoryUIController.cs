using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject _inventoryPanel;
    [SerializeField] GameObject _equipmentPanel;

    private void Start()
    {
        if (_inventoryPanel != null) _inventoryPanel.SetActive(false);
        if (_equipmentPanel != null) _equipmentPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (_inventoryPanel == null) return;

        bool isCurrentlyActive = _inventoryPanel.activeSelf;

        _inventoryPanel.SetActive(!isCurrentlyActive);

        if (!isCurrentlyActive && _equipmentPanel != null)
        {
            _equipmentPanel.SetActive(false);
        }
    }

    public void ToggleEquipment()
    {
        if (_equipmentPanel == null) return;

        bool isCurrentlyActive = _equipmentPanel.activeSelf;

        _equipmentPanel.SetActive(!isCurrentlyActive);

        if (!isCurrentlyActive && _inventoryPanel != null)
        {
            _inventoryPanel.SetActive(false);
        }
    }
}
