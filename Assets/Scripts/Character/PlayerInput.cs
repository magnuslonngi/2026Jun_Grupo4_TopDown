using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _interactInput;

    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void OnEnable()
    {
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnMove;
    }

    private void OnDisable()
    {
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _characterMovement.MoveDirection = context.ReadValue<Vector2>();
    }
}
