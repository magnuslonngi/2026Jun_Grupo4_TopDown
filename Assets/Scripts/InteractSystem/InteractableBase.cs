using TMPro;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractable
{
    [Header("Input")]
    [SerializeField] private string _keyString;

    [Header("World Display")]
    [SerializeField] protected string _interactString;
    [SerializeField] private GameObject _worldCanvasGameObject;
    [SerializeField] private TextMeshProUGUI _textMeshProKeyText;
    [SerializeField] private TextMeshProUGUI _textMeshProInteractText;

    protected virtual void Start()
    {
        _textMeshProKeyText.text = _keyString;
        _textMeshProInteractText.text = _interactString;
    }

    public virtual void ShowText()
    {
        _worldCanvasGameObject.gameObject.SetActive(true);
    }

    public virtual void HideText()
    {
        _worldCanvasGameObject.gameObject.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Interact(GameObject interactor) { }
}