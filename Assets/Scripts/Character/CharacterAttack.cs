using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterAttack : MonoBehaviour
{
    [Header("Collider Reference")]
    [SerializeField] private HitCollider _hitCollider;

    [Header("Simple Attack")]
    [SerializeField] private float _attackDamage;
    public float AttackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }
    [SerializeField] private float _attackImpulse;
    [SerializeField] private float _attackImpulseDelay;
    [SerializeField] private float _attackDuration;

    [Header("Attack Unity Events")]
    public UnityEvent OnAttackPerformed;
    public UnityEvent OnAttackFinished;

    [Header("Charged Attack")]
    [SerializeField] private float _chargedAttackDamage;
    public float ChargedAttackDamage
    {
        get { return _chargedAttackDamage; }
        set { _chargedAttackDamage = value; }
    }
    [SerializeField] private float _chargedAttackImpulse;
    [SerializeField] private float _chargedAttackImpulseDelay;
    [SerializeField] private float _chargedAttackHoldTime;
    [SerializeField] private float _chargedAttackDuration;

    [Header("Charged Attack Unity Events")]
    public UnityEvent OnAttackChargeBegin;
    public UnityEvent OnAttackChargeCompleted;
    public UnityEvent OnChargedAttackPerformed;
    public UnityEvent OnChargedAttackFinished;

    private CharacterMovement _characterMovement;

    private Coroutine _chargeAttackRoutine;

    private bool _isAttacking;
    private bool _isAttackCharged;

    // Save initial reference for equipment change
    private float _baseAttackDamage;
    public float BaseAttackDamage
    {
        get { return _baseAttackDamage; }
        set { _baseAttackDamage = value; }
    }
    private float _baseChargedAttackDamage;
    public float BaseChargedAttackDamage
    {
        get { return _baseChargedAttackDamage; }
        set { _baseChargedAttackDamage = value; }
    }

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        _hitCollider.gameObject.SetActive(false);

        _baseAttackDamage = _attackDamage;
        _baseChargedAttackDamage = _chargedAttackDamage;
    }

    public void AttackPerformed()
    {
        if (_isAttacking || _isAttackCharged) return;

        OnAttackPerformed.Invoke();
        EnableHitCollider(_attackDamage, _attackImpulse, _attackImpulseDelay);

        _characterMovement.CanMove = false;
        _isAttacking = true;

        StartCoroutine(WaitForAttackDuration());

        _chargeAttackRoutine = StartCoroutine(ChargeAttack());
    }

    private IEnumerator WaitForAttackDuration()
    {
        yield return new WaitForSeconds(_attackDuration);

        OnAttackFinished.Invoke();
        DisableHitCollider();

        _hitCollider.gameObject.SetActive(false);
        _characterMovement.CanMove = true;
        _isAttacking = false;
    }

    private IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(_attackDuration);

        OnAttackChargeBegin.Invoke();

        yield return new WaitForSeconds(_chargedAttackHoldTime);

        OnAttackChargeCompleted.Invoke();
        _isAttackCharged = true;
    }

    public void AttackCanceled()
    {
        if (!_isAttackCharged || _isAttacking)
        {
            StopCoroutine(_chargeAttackRoutine);
            return;
        }

        OnChargedAttackPerformed.Invoke();
        EnableHitCollider(_chargedAttackDamage, _chargedAttackImpulse, _chargedAttackImpulseDelay);

        StartCoroutine(WaitForChargedAttackDuration());
    }

    private IEnumerator WaitForChargedAttackDuration()
    {
        yield return new WaitForSeconds(_chargedAttackDuration);

        OnChargedAttackFinished.Invoke();
        DisableHitCollider();

        _isAttackCharged = false;
    }

    private void DisableHitCollider()
    {
        _hitCollider.gameObject.SetActive(false); 
    }

    private void EnableHitCollider(float damage, float impulse, float delay)
    {
        _hitCollider.Damage = damage; 
        _hitCollider.Impulse = impulse;
        _hitCollider.Delay = delay;

        _hitCollider.gameObject.SetActive(true); 
    }
}
