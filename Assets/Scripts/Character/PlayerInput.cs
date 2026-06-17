using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement), typeof(CharacterAttack))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _interactInput;

    private CharacterMovement _characterMovement;
    private CharacterAttack _characterAttack;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAttack = GetComponent<CharacterAttack>();
    }

    private void OnEnable()
    {
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMovePerformed;

        _attackInput.action.performed += OnAttackPerformed;
        _attackInput.action.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMovePerformed;

        _attackInput.action.performed -= OnAttackPerformed;
        _attackInput.action.canceled -= OnAttackCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _characterMovement.MoveDirection = context.ReadValue<Vector2>();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
       _characterAttack.AttackPerformed();
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _characterAttack.AttackCanceled();
    }
}
