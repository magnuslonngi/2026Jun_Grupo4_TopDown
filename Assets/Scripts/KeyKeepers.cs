using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyKeepers : MonoBehaviour
{
    [SerializeField] UnityEvent OnAllEnemiesDefeated;
    void Update()
    {
        if(transform.childCount == 0)
        {
            OnAllEnemiesDefeated.Invoke();
        }
    }
}
