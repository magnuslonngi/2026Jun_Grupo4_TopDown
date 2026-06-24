using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EquipmentManager _equipmentManager;

    [Header("Weapon Slot")]
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _weaponText;

    [Header("Helmet Slot")]
    [SerializeField] private Image _helmetImage;
    [SerializeField] private TextMeshProUGUI _helmetText;

    [Header("Chest Slot")]
    [SerializeField] private Image _chestImage;
    [SerializeField] private TextMeshProUGUI _chestText;

    [Header("Pants Slot")]
    [SerializeField] private Image _pantsImage;
    [SerializeField] private TextMeshProUGUI _pantsText;

    private void OnEnable()
    {
        _equipmentManager.OnEquipmentChanged.AddListener(UpdateEquipmentUI);
        UpdateEquipmentUI();
    }

    private void OnDisable()
    {
        _equipmentManager.OnEquipmentChanged.RemoveListener(UpdateEquipmentUI);
    }

    public void UpdateEquipmentUI()
    {
        UpdateSlot(_weaponImage, _weaponText, "Weapon", _equipmentManager.currentWeapon);
        UpdateSlot(_helmetImage, _helmetText, "Helmet", _equipmentManager.currentHelmet);
        UpdateSlot(_chestImage, _chestText, "Armor", _equipmentManager.currentChest);
        UpdateSlot(_pantsImage, _pantsText, "Pants", _equipmentManager.currentPants);
    }

    // Esta función hace el "concat" del texto y cambia la imagen
    private void UpdateSlot(Image slotImage, TextMeshProUGUI slotText, string slotName, InventoryInfo item)
    {
        if (item != null)
        {
            slotImage.sprite = item.sprite;
            slotImage.color = Color.white;

            slotText.text = slotName + "\nAttack: " + item.attack + "\nDefense: " + item.defense;
        }
        else
        {
            slotImage.sprite = null;
            slotImage.color = new Color(1, 1, 1, 0.2f);
            slotText.text = slotName + "\nAttack: 0\nDefense: 0";
        }
    }
}