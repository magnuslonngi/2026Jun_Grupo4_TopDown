using UnityEngine;
using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    public UnityEvent<float> OnHitRecieved;

    public void NotifyHit(HitCollider hitCollider)
    {
        OnHitRecieved?.Invoke(hitCollider.Damage);

        if (TryGetComponent(out CharacterMovement characterMovement))
        {
            GameObject parentGameObject = hitCollider.transform.parent.gameObject;
            characterMovement.ApplyImpulseFromObject(parentGameObject, hitCollider.Impulse, hitCollider.Delay);
        }
    }
}
