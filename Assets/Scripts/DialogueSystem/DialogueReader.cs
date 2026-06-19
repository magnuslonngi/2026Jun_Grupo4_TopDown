using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueReader : InteractableBase
{
    [Header("Dialogue References")]
    [SerializeField] private InputActionReference _dialogueInput;
    [SerializeField] private GameObject _dialogueCanvas;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Header("Dialogue Data")]
    [SerializeField] private float _charactersRevealSpeed = 0.05f;
    [SerializeField] private DialogueSO _dialogueSO;
    [SerializeField] private bool _destroyOnEnd; 

    [Header("Unity Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueChange;
    public UnityEvent OnCharacterReveal;
    public UnityEvent OnDialogueEnd;

    private int _currentIndex;
    private GameObject _interactor;
    private Coroutine _charactersRevealRoutine;

    public override void Interact(GameObject interactor)
    {
        _dialogueCanvas.SetActive(true);
        _interactor = interactor;

        if (_interactor.TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.DisableInput();
        }

        _dialogueInput.action.performed += ChangeDialogue;
        OnDialogueStart.Invoke();

        SetDialogue(_currentIndex);
    }

    private void ChangeDialogue(InputAction.CallbackContext context)
    {
        if (_currentIndex + 1 >= _dialogueSO.dialogues.Length)
        {
            if (_destroyOnEnd) Destroy(this);

            _dialogueCanvas.SetActive(false);

            if (_interactor.TryGetComponent(out PlayerInput playerInput))
            {
                playerInput.EnableInput();
            }

            _currentIndex = 0;
            _interactor = null;
            _dialogueInput.action.performed -= ChangeDialogue;

            ResetRevealCharacters();
            OnDialogueEnd.Invoke();

            return;
        }

        ResetRevealCharacters();

        _currentIndex++;
        OnDialogueChange.Invoke();

        SetDialogue(_currentIndex);
    }

    private void SetDialogue(int index)
    {
        if (_dialogueSO.dialogues[index].Name == "" || 
            _dialogueSO.dialogues[index].Name == null)
        {
            _nameText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _nameText.transform.parent.gameObject.SetActive(true);
        }

        _nameText.text = _dialogueSO.dialogues[index].Name;

        _dialogueText.maxVisibleCharacters = 0;
        _dialogueText.text = _dialogueSO.dialogues[index].Dialogue;
        _charactersRevealRoutine = StartCoroutine(RevealCharacters());
    }

    private IEnumerator RevealCharacters()
    {
        foreach (char character in _dialogueText.text)
        {
            if (character == ' ')
            {
                _dialogueText.maxVisibleCharacters += 2;
            }
            else
            {
                _dialogueText.maxVisibleCharacters += 1;
            }

            OnCharacterReveal.Invoke();

            yield return new WaitForSeconds(_charactersRevealSpeed);
        }
    }

    private void ResetRevealCharacters()
    {
        if (_charactersRevealRoutine != null)
        {
            StopCoroutine(_charactersRevealRoutine);
            _dialogueText.maxVisibleCharacters = 0;
        }
    }

    public void SetNewDialogueSO(DialogueSO newDialogue)
    {
        _dialogueSO = newDialogue;
    }
}
