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
    private CharacterCollect _characterCollect;

    private CharacterAttack _characterAttack;
    private Health _health;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _characterCollect = GetComponent<CharacterCollect>();

        _characterAttack = GetComponent<CharacterAttack>();
        _health = GetComponent<Health>();
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
                if (currentWeapon != null) _characterCollect.CollectFromEquipment(currentWeapon);
                currentWeapon = item;
                break;
            case InventoryInfo.InventoryObjectType.Helmet:
                if (currentHelmet != null) _characterCollect.CollectFromEquipment(currentHelmet);
                currentHelmet = item;
                break;
            case InventoryInfo.InventoryObjectType.Chest:
                if (currentChest != null) _characterCollect.CollectFromEquipment(currentChest);
                currentChest = item;
                break;
            case InventoryInfo.InventoryObjectType.Pants:
                if (currentPants != null) _characterCollect.CollectFromEquipment(currentPants);
                currentPants = item;
                break;
            default:
                return;
        }

        OnEquipmentChanged?.Invoke();
        CalculateNewStats();
    }

    private void CalculateNewStats()
    {
        float chestAttack = currentChest == null ? 0 : currentChest.attack;
        float helmetAttack = currentHelmet == null ? 0 : currentHelmet.attack;
        float pantsAttack = currentPants == null ? 0 : currentPants.attack;
        float weaponAttack = currentWeapon == null ? 0 : currentWeapon.attack;

        float newDamage = chestAttack + helmetAttack + pantsAttack + weaponAttack;

        _characterAttack.AttackDamage = _characterAttack.BaseAttackDamage + newDamage;
        _characterAttack.ChargedAttackDamage = _characterAttack.BaseChargedAttackDamage + newDamage;


        float chestHealth = currentChest == null ? 0 : currentChest.defense;
        float helmetHealth = currentHelmet == null ? 0 : currentHelmet.defense;
        float pantsHealth = currentPants == null ? 0 : currentPants.defense;
        float weaponHealth = currentWeapon == null ? 0 : currentWeapon.defense;

        float newHealth = chestHealth + helmetHealth + pantsHealth + weaponHealth;

        _health.MaxHealthPoints = _health.InitialMaxHealth + newHealth;

        _health.OnHealthInitialize.Invoke(_health.MaxHealthPoints);
        _health.OnHealthChange.Invoke(_health.HealthPoints);
    }
}