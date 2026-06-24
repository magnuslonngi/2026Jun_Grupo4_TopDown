using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _player;

    [Header("Pause Options")]
    [SerializeField] private InputActionReference _pauseInput;
    [SerializeField] private GameObject _pauseMenu;

    public bool CanPause = false;

    [Header("Win and Lose References")]
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _loseMenu;

    [HideInInspector] public bool IsGamePaused = false;

    [Header("Pause Unity Events")]
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;

    private Health _playerHealth;

    private void Awake()
    {
        if (_player != null)
        {
            _playerHealth = _player.GetComponent<Health>();
        }
    } 

    private void OnEnable()
    {
        if (_pauseInput != null)
        {
            _pauseInput.action.performed += TogglePause;
        }

        if (_player != null)
        {
            _playerHealth.OnHealthDeplete.AddListener(PlayerDead);
        }
    }
    
    private void OnDisable()
    {
        if (_pauseInput != null)
        {
            _pauseInput.action.performed -= TogglePause;
        }

        if (_player != null)
        {
            _playerHealth.OnHealthDeplete.RemoveListener(PlayerDead);
        }
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

    private void PlayerDead(float health)
    {
        ShowLoseMenu();
    }

    public void ShowWinMenu()
    {
        _winMenu?.SetActive(true);

        Time.timeScale = 0;
        AudioListener.pause = true;
        IsGamePaused = true;
    }

    private void ShowLoseMenu()
    {
        _loseMenu?.SetActive(true);

        Time.timeScale = 0;
        AudioListener.pause = true;
        IsGamePaused = true;
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
