using UnityEngine;

public class InteractableObject : InteractableBase
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject gameObject)
    {
        Debug.Log(gameObject.name + " is interacting with: " + _interactString);
    }
}
