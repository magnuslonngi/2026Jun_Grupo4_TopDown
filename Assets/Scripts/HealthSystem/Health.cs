using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HurtCollider))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _healthPoints;
    public UnityEvent<float> OnHealthInitialize;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent<float> OnHealthDeplete;

    private HurtCollider _hurtCollider;
    private CharacterCollect characterCollect;
    Inventory inventory;

    private void Awake()
    {
        _hurtCollider = GetComponent<HurtCollider>();
        characterCollect = GetComponent<CharacterCollect>();
        inventory = GetComponent<Inventory>();
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
        OnHealthInitialize?.Invoke(_healthPoints);
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
            OnHealthChange?.Invoke(_healthPoints);

        }
    }
}
