using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : Singleton<PlayFabManager>
{
    void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    } 

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }

            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    public GameObject rowPrefab;
    public Transform rowsParent;

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        rowsParent = GameObject.Find("Menus/LeaderboardMenu(Clone)/Body/Table").transform;

        foreach (var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, rowsParent);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            foreach( var text in texts)
            {
                texts[0].text = item.Position.ToString();
                texts[1].text = item.PlayFabId.ToString();
                texts[2].text = item.StatValue.ToString();
            }


            Debug.Log(item.Position + " " + item.PlayFabId + " " + ((float)item.StatValue/10).ToString("#0.0"));
        }
    }

}
