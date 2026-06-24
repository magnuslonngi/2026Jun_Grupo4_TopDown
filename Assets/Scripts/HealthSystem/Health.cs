using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HurtCollider))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoints;
    public float HealthPoints
    {
        get { return _healthPoints; }
        set { _healthPoints = value; }
    }
    public UnityEvent<float> OnHealthInitialize;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent<float> OnHealthDeplete;

    private HurtCollider _hurtCollider;
    private CharacterCollect characterCollect;
    Inventory inventory;

    private float _maxHealthPoints;
    public float MaxHealthPoints
    {
        get { return _maxHealthPoints; }
        set { _maxHealthPoints = value; }
    }

    private float _initialMaxHealth;
    public float InitialMaxHealth
    {
        get { return _initialMaxHealth; }
        set { _initialMaxHealth = value; }
    }

    private void Awake()
    {
        _hurtCollider = GetComponent<HurtCollider>();
        characterCollect = GetComponent<CharacterCollect>();
        inventory = GetComponent<Inventory>();

        _maxHealthPoints = _healthPoints;
        _initialMaxHealth = _healthPoints;
    }

    private void OnEnable()
    {
        _hurtCollider.OnHitRecieved.AddListener(OnHitRecieved);
        characterCollect?.onColletedObjectDirectUsage.AddListener(onCollectedObject);
        inventory?.onObjectUsed.AddListener(onObjectUsed);
    }
    private void OnDisable()
    {
        _hurtCollider.OnHitRecieved.RemoveListener(OnHitRecieved);
        characterCollect?.onColletedObjectDirectUsage.RemoveListener(onCollectedObject);
        inventory?.onObjectUsed.RemoveListener(onObjectUsed);
    }

    private void Start()
    {
        OnHealthInitialize?.Invoke(_maxHealthPoints);
    }

    private void OnHitRecieved(float damage)
    {
        if (!(_healthPoints > 0)) return;

        _healthPoints -= damage;
        OnHealthChange?.Invoke(_healthPoints);

        if (_healthPoints <= 0)
        {
            _healthPoints = 0;
            OnHealthDeplete?.Invoke(_healthPoints);
        }
    }

    private void onCollectedObject(CollectableObject collectable)
    {
        InventoryInfo info = collectable.inventoryInfo;
        UseInventoryInfo(info);
    }


    private void onObjectUsed(InventoryInfo info)
    {
        UseInventoryInfo(info);
    }

    private void UseInventoryInfo(InventoryInfo info)
    {
        if (info.objectType == InventoryInfo.InventoryObjectType.Health)
        {
            _healthPoints += info.recovery;

            _healthPoints = Mathf.Min(_healthPoints, _maxHealthPoints);

            OnHealthChange?.Invoke(_healthPoints);

        }
    }
}
