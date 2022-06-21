using UnityEngine;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        [SerializeField] private FloatScriptableObject _scoreSO;
        [SerializeField] private FloatScriptableObject _timeElapsedSO;
        public void OnResumePressed()
        {
            Time.timeScale = 1;
            MenuManager.Instance.CloseAllActiveMenus();
        }

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
    }
}