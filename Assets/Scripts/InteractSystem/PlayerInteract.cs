using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask _interactLayerMask;
    [SerializeField] private float _interactRange;

    private IInteractable _closestInteractable;
    private IInteractable _lastInteractable;

    private void Update()
    {
        List<IInteractable> interactablesInRange = new();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interactRange, _interactLayerMask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactablesInRange.Add(interactable);
            }
        }

        _closestInteractable = null;

        foreach (IInteractable interactable in interactablesInRange)
        {
            _closestInteractable = interactable;

            if (Vector3.Distance(transform.position, interactable.GetPosition()) < 
                Vector3.Distance(transform.position, _closestInteractable.GetPosition()))
            {
                _closestInteractable = interactable;
            }
        }

        if (_closestInteractable != null)
        {            
            _lastInteractable?.HideText();
            _lastInteractable = _closestInteractable;
        }
        else if (_closestInteractable == null && _lastInteractable != null)
        {
            _lastInteractable.HideText();
            _lastInteractable = null;
        }

        _closestInteractable?.ShowText();
    }

    public void InteractWithObject()
    {
        _closestInteractable?.Interact();
    }
}
