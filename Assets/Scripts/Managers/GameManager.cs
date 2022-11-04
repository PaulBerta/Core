using LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public KeyCode pauseKey;

    [SerializeField] private FloatScriptableObject _scoreSO;
    [SerializeField] private FloatScriptableObject _highScoreSO;
    [SerializeField] private FloatScriptableObject _timeElaspsedSO;

    public bool isGameActive;
    public bool isGamePaused;

    private void Start()
    {
        _scoreSO.Value = 0;
        _timeElaspsedSO.Value = 0;
    }

    private void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            isGameActive = true;
            isGamePaused = false;
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateScore();
            if (!isGamePaused && Input.GetKeyDown(pauseKey))
            {
                PauseGame();
            }
            else if (isGamePaused && Input.GetKeyDown(pauseKey))
            {
                UnpauseGame();
            }
        }
    }

    private void UnpauseGame()
    {
        isGamePaused = false;
        if (PauseMenu.Instance != null) PauseMenu.Instance.OnResumePressed();
    }

    private void PauseGame()
    {
        if (MenuManager.Instance != null && PauseMenu.Instance != null)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            MenuManager.Instance.OpenMenu(PauseMenu.Instance);
        }

    }

    public void UpdateScore()
    {
        _scoreSO.Value += Time.deltaTime;
        _timeElaspsedSO.Value += Time.deltaTime;
    }
    public void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        var nextSceneIndex = currentScene.buildIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReloadCurrentScene()
    {
        var currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void GameOver()
    {
        isGameActive = false;

        if (_highScoreSO.Value < _scoreSO.Value)
        {
            _highScoreSO.Value = _scoreSO.Value;
            PlayFabManager.Instance.SendLeaderboard((int)(_scoreSO.Value * 10));
        }

        Time.timeScale = 0;

        if (MenuManager.Instance != null && GameOverMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(GameOverMenu.Instance);
            GameOverMenu.Instance.UpdateFinalScore();
            GameOverMenu.Instance.UpdateHighScore();
        }
    }

}

