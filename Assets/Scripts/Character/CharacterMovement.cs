using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{ 
    [SerializeField] private float _speed = 5f;

    public event EventHandler<Vector2> OnMoveDirectionChange;

    private bool _canMove = true;
    public bool CanMove {
        get { 
            return _canMove; 
        } 
        set {
            _canMove = value;
            if (value) MoveDirection = _moveDirection;
        } 
    }

    private Vector2 _moveDirection;
    public Vector2 MoveDirection {
        get {
            return _moveDirection;
        }
        set {
            _moveDirection = value;

            if (!_canMove) return;

            OnMoveDirectionChange?.Invoke(this, _moveDirection);
        }
    }
    
    private Rigidbody2D _rigidbody2d;
    private bool _disableUpdate;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_disableUpdate) return;

        if (!_canMove)
        {    
            _rigidbody2d.linearVelocity = Vector2.zero;
            return;
        }

        _rigidbody2d.linearVelocity = _moveDirection * _speed;
    }

    public void ApplyImpulseFromObject(GameObject gameObject, float impulse, float delay)
    {
        StopAllCoroutines();

        _disableUpdate = true;
        _rigidbody2d.linearVelocity = Vector2.zero;

        Vector2 direction = (transform.position - gameObject.transform.position).normalized;
        _rigidbody2d.AddForce(direction * impulse, ForceMode2D.Impulse);

        StartCoroutine(ResetImpulse(delay));
    }

    private IEnumerator ResetImpulse(float delay)
    {
        yield return new WaitForSeconds(delay);

        _disableUpdate = false;
        _rigidbody2d.linearVelocity = Vector2.zero;
    }
}
