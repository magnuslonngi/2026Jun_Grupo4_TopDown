using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Pause Options")]
    [SerializeField] private InputActionReference _pauseInput;
    [SerializeField] private GameObject _pauseMenu;
    public bool CanPause = false;

    [HideInInspector] public bool IsGamePaused = false;

    [Header("Pause Unity Events")]
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;

    private void OnEnable()
    {
        if (_pauseInput == null) return;

        _pauseInput.action.performed += TogglePause;
    }
    
    private void OnDisable()
    {
        if (_pauseInput == null) return;

        _pauseInput.action.performed -= TogglePause;
    }

    public void GoToScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void EnableItem(GameObject item)
    {
        item.SetActive(true);
    }

    public void DisableItem(GameObject item)
    {
        item.SetActive(false);
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (IsGamePaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        if (!CanPause) return;

        OnGamePause.Invoke();
        _pauseMenu?.SetActive(true);

        Time.timeScale = 0;
        AudioListener.pause = true;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        if (!CanPause) return;

        OnGameResume.Invoke();
        _pauseMenu?.SetActive(false);

        Time.timeScale = 1;
        AudioListener.pause = false;
        IsGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
