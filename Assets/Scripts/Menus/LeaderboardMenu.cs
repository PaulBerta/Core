namespace LevelManagement
{
    class LeaderboardMenu : Menu<LeaderboardMenu>
    {
        private void Start()
        {
            PlayFabManager.Instance.GetLeaderboard();
        }

    }
}
