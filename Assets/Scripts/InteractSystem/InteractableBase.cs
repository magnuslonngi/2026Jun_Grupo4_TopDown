using TMPro;
using UnityEngine;

public class IntaractableBase : MonoBehaviour, IInteractable
{
    [Header("Input")]
    [SerializeField] private string _keyString;

    [Header("Display")]
    [SerializeField] protected string _interactString;
    [SerializeField] private GameObject _canvasGameObject;
    [SerializeField] private TextMeshProUGUI _textMeshProKeyText;
    [SerializeField] private TextMeshProUGUI _textMeshProInteractText;

    protected virtual void Start()
    {
        _textMeshProKeyText.text = _keyString;
        _textMeshProInteractText.text = _interactString;
    }

    public virtual void ShowText()
    {
        _canvasGameObject.gameObject.SetActive(true);
    }

    public virtual void HideText()
    {
        _canvasGameObject.gameObject.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Interact() { }
}