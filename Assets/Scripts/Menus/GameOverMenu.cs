using TMPro;
using UnityEngine;

namespace LevelManagement
{
    public class GameOverMenu : Menu<GameOverMenu>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        [SerializeField] private FloatScriptableObject _scoreSO;
        [SerializeField] private FloatScriptableObject _highScoreSO;
        [SerializeField] private FloatScriptableObject _timeElapsedSO;
        public void OnRestartPressed() 
        {
            Time.timeScale = 1;
            MenuManager.Instance.CloseAllActiveMenus();
            GameManager.Instance.ReloadCurrentScene();
            _scoreSO.Value = 0;
            _timeElapsedSO.Value = 0;
        }

        public void OnSettingsPressed()
        {
            Time.timeScale = 1;
            SettingsMenu.Open();
        }

        public void OnMainMenuPressed()
        {
            Time.timeScale = 1;
            MainMenu.Open();
        }

        public void OnQuitPressed() 
        {
            Application.Quit();
        }

        public void UpdateFinalScore()
        {
            _scoreText.text = _scoreSO.Value.ToString("#0.0");
        }

        public void UpdateHighScore() 
        {
            _highScoreText.text = _highScoreSO.Value.ToString("#0.0");
        }
    }
}