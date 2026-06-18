using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement), typeof(CharacterAttack), typeof(PlayerInteract))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _interactInput;

    private CharacterMovement _characterMovement;
    private CharacterAttack _characterAttack;

    private PlayerInteract _playerInteract;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAttack = GetComponent<CharacterAttack>();
        _playerInteract = GetComponent<PlayerInteract>();
    }

    private void OnEnable()
    {
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMovePerformed;

        _attackInput.action.performed += OnAttackPerformed;
        _attackInput.action.canceled += OnAttackCanceled;

        _interactInput.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMovePerformed;

        _attackInput.action.performed -= OnAttackPerformed;
        _attackInput.action.canceled -= OnAttackCanceled;

        _interactInput.action.performed -= OnInteractPerformed;
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

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        _playerInteract.InteractWithObject();
    }
}
