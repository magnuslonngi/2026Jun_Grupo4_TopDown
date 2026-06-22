using System;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] public InventoryInfo inventoryInfo;

    internal void NotifyCollected()
    {
        // Hacemos alguna animación o efecto visual?
        Destroy(gameObject);
    }
}
