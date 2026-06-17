using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterAttack _characterAtack;
    private Animator _animator;

    private int _isMovingHash = Animator.StringToHash("IsMoving");
    private int _moveDirectionXHash = Animator.StringToHash("MoveDirectionX");
    private int _moveDirectionYHash = Animator.StringToHash("MoveDirectionY");

    private int _isAttackingHash = Animator.StringToHash("IsAttacking");
    private int _isChargedAttackingHash = Animator.StringToHash("IsChargedAttacking");

    private void Awake()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _characterAtack = GetComponentInParent<CharacterAttack>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _characterMovement.OnMoveDirectionChange += UpdateMoveAnimation;

        _characterAtack.OnAttackPerformed.AddListener(UpdateAttackPerformedState);
        _characterAtack.OnAttackFinished.AddListener(UpdateAttackFinishedState);

        _characterAtack.OnChargedAttackPerformed.AddListener(UpdateChargedAttackPerformedState);
        _characterAtack.OnChargedAttackFinished.AddListener(UpdateChargedAttackFinishedState);
    }

    private void OnDisable()
    {
        _characterMovement.OnMoveDirectionChange -= UpdateMoveAnimation;

        _characterAtack.OnAttackPerformed.RemoveListener(UpdateAttackPerformedState);
        _characterAtack.OnAttackFinished.RemoveListener(UpdateAttackFinishedState);

        _characterAtack.OnChargedAttackPerformed.RemoveListener(UpdateChargedAttackPerformedState);
        _characterAtack.OnChargedAttackFinished.RemoveListener(UpdateChargedAttackFinishedState);
    }

    private void UpdateMoveAnimation(object sender, Vector2 moveDirection)
    {
        if (moveDirection == Vector2.zero)
        {
            _animator.SetBool(_isMovingHash, false);
            return;
        }

        _animator.SetBool(_isMovingHash, true);
        _animator.SetFloat(_moveDirectionXHash, moveDirection.x);
        _animator.SetFloat(_moveDirectionYHash, moveDirection.y);
    }

    private void UpdateAttackPerformedState()
    {
        _animator.SetBool(_isAttackingHash, true);
    }

    private void UpdateAttackFinishedState()
    {
        _animator.SetBool(_isAttackingHash, false);
    }

    private void UpdateChargedAttackPerformedState()
    {
        _animator.SetBool(_isChargedAttackingHash, true);
    }

    private void UpdateChargedAttackFinishedState()
    {
        _animator.SetBool(_isChargedAttackingHash, false);
    }
}
