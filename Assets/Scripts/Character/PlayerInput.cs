using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement), typeof(CharacterAttack), typeof(PlayerInteract))]

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _interactInput;
    [SerializeField] private InputActionReference _inventoryInput;
    [SerializeField] private InputActionReference _equipmentInput;

    [SerializeField] private InventoryUIController _uiController;

    public UnityEvent OnInventoryToggle;

    private CharacterMovement _characterMovement;
    private CharacterAttack _characterAttack;

    private PlayerInteract _playerInteract;

    private MenuManager _menuManager;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAttack = GetComponent<CharacterAttack>();
        _playerInteract = GetComponent<PlayerInteract>();

        _menuManager = FindAnyObjectByType<MenuManager>();
    }

    private void OnEnable()
    {
        EnableInput();

        _menuManager.OnGamePause.AddListener(DisableInput);
        _menuManager.OnGameResume.AddListener(EnableInput);
    }

    private void OnDisable()
    {
        DisableInput();

        _menuManager.OnGamePause.RemoveListener(DisableInput);
        _menuManager.OnGameResume.RemoveListener(EnableInput);
    }

    public void EnableInput()
    {
        _moveInput.action.performed += OnMovePerformed;
        _moveInput.action.canceled += OnMovePerformed;

        _attackInput.action.performed += OnAttackPerformed;
        _attackInput.action.canceled += OnAttackCanceled;

        _interactInput.action.performed += OnInteractPerformed;

        _inventoryInput.action.performed += OnInvetoryInteractPerformed;
        _equipmentInput.action.performed += OnEquipmentInteractPerformed;
    }

    public void DisableInput()
    {
        _moveInput.action.performed -= OnMovePerformed;
        _moveInput.action.canceled -= OnMovePerformed;

        _attackInput.action.performed -= OnAttackPerformed;
        _attackInput.action.canceled -= OnAttackCanceled;

        _interactInput.action.performed -= OnInteractPerformed;

        _inventoryInput.action.performed -= OnInvetoryInteractPerformed;
        _equipmentInput.action.performed -= OnEquipmentInteractPerformed;

        _characterMovement.MoveDirection = Vector2.zero;
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

    private void OnInvetoryInteractPerformed(InputAction.CallbackContext context)
    {
        if (_uiController != null)
        {
            _uiController.ToggleInventory();
        }
    }

    private void OnEquipmentInteractPerformed(InputAction.CallbackContext context)
    {
        if (_uiController != null)
        {
            _uiController.ToggleEquipment();
        }
    }
}
