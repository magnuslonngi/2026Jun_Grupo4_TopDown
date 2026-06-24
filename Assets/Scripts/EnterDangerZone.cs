using UnityEngine;
using UnityEngine.Events;

public class EnterDangerZone : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnterArea;

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterArea.Invoke();
    }
}
