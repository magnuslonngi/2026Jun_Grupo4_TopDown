using UnityEngine;

public class InteractableObject : IntaractableBase
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        Debug.Log("Interacting with: " + _interactString);
    }
}
