using UnityEngine;
using TMPro;

public class SettingsTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _average;
    [SerializeField] private TextMeshProUGUI _numberPlayed;


    public void SetTitle(string title) {
        _title.text = title;
    }

    public void SetAverage(float average){
        _average.text = "Avg: " + average.ToString("F2");
    }    
    
    public void SetNumGamesPlayed(int numGamesPlayed){
        _title.text = "Games Played: " + numGamesPlayed.ToString();
    }

}
