using UnityEngine;

public class HitCollider : MonoBehaviour
{
    private float _damage;
    public float Damage 
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HurtCollider hurtCollider = other.GetComponent<HurtCollider>();
        hurtCollider?.NotifyHit(this);
    }
}
