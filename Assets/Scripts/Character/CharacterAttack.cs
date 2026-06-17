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
    [SerializeField] private float _attackImpulse;
    [SerializeField] private float _attackImpulseDelay;
    [SerializeField] private float _attackDuration;
    public UnityEvent OnAttackPerformed;
    public UnityEvent OnAttackFinished;

    [Header("Charged Attack")]
    [SerializeField] private float _chargedAttackDamage;
    [SerializeField] private float _chargedAttackImpulse;
    [SerializeField] private float _chargedAttackImpulseDelay;
    [SerializeField] private float _chargedAttackHoldTime;
    [SerializeField] private float _chargedAttackDuration;
    public UnityEvent OnAttackChargeBegin;
    public UnityEvent OnAttackChargeCompleted;
    public UnityEvent OnChargedAttackPerformed;
    public UnityEvent OnChargedAttackFinished;

    private CharacterMovement _characterMovement;

    private Coroutine _chargeAttackRoutine;

    private bool _isAttacking;
    private bool _isAttackCharged;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        _hitCollider.gameObject.SetActive(false);
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
