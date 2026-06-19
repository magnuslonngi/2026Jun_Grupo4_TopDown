using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interactor);
    void ShowText();
    void HideText();
    Vector3 GetPosition();
}
