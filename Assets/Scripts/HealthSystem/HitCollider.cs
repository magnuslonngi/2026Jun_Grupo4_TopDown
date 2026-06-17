using UnityEngine;

public class HitCollider : MonoBehaviour
{
    private float _damage;
    public float Damage 
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private float _impulse;
    public float Impulse
    {
        get { return _impulse; }
        set { _impulse = value; }
    }

    private float _delay;
    public float Delay
    {
        get { return _delay; }
        set { _delay = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HurtCollider hurtCollider = other.GetComponent<HurtCollider>();
        hurtCollider?.NotifyHit(this);
    }
}
