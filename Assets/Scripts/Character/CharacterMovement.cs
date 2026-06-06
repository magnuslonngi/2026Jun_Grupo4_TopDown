using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{ 
    [SerializeField] private float _speed = 5f;

    public event EventHandler<Vector2> OnMoveDirectionChange;

    private Vector2 _moveDirection;
    public Vector2 MoveDirection {
        get {
            return _moveDirection;
        }
        set {
            _moveDirection = value;
            OnMoveDirectionChange?.Invoke(this, _moveDirection);
        }
    }
    
    private Rigidbody2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody2d.linearVelocity = _moveDirection * _speed;
    }
}
