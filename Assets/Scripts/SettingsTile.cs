using UnityEngine;
using TMPro;

public class SettingsTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _average;
    [SerializeField] private TextMeshProUGUI _numberPlayed;

    public void SetData(int numGamesplayed, string setGameName, float setAverage)
    {
        _title.text = setGameName;
        _average.text = "Avg: " + setAverage.ToString("F2");
        _numberPlayed.text = "Games Played: " + numGamesplayed.ToString();
    }
}
