using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private FloatScriptableObject _scoreSO;
        [SerializeField] private FloatScriptableObject _timeElapsedSO;
        public void OnPlayPressed()
        {
            if (GameManager.Instance != null && MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseAllActiveMenus();

                var mainMenuSceneIndex = 0;
                var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                if (currentSceneIndex != mainMenuSceneIndex)
                {
                    GameManager.Instance.ReloadCurrentScene();
                    _scoreSO.Value = 0;
                    _timeElapsedSO.Value = 0;
                }
                else
                {
                    GameManager.Instance.LoadNextLevel();
                }
            }
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }

        public void OnLeaderboardPressed()
        {
            LeaderboardMenu.Open();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }

    }
}