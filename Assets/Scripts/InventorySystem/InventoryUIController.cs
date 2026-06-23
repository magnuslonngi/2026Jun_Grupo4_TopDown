using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject _inventoryPanel;

    private void Start()
    {
        if (_inventoryPanel != null)
        {
            _inventoryPanel.SetActive(false);
        }
    }

    public void ToggleInventory()
    {
        if (_inventoryPanel != null)
        {
            bool isActive = _inventoryPanel.activeSelf;
            _inventoryPanel.SetActive(!isActive);
        }
    }
}
