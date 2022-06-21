using TMPro;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private FloatScriptableObject _scoreSO;
    private void Update()
    {
        UpdateScoreText(_scoreSO.Value);
    }
    public void UpdateScoreText(float score)
    {
        _scoreText.text = score.ToString("#0.0");
    }

}
