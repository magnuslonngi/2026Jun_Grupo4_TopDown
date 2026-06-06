using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private Animator _animator;

    private int _isMovingHash = Animator.StringToHash("IsMoving");
    private int _moveDirectionXHash = Animator.StringToHash("MoveDirectionX");
    private int _moveDirectionYHash = Animator.StringToHash("MoveDirectionY");

    private void Awake()
    {
        _characterMovement = GetComponentInParent<CharacterMovement>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _characterMovement.OnMoveDirectionChange += UpdateMoveAnimation;
    }

    private void OnDisable()
    {
        _characterMovement.OnMoveDirectionChange -= UpdateMoveAnimation;
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
}
