using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using TMPro;
using UnityEngine;

public class PlayerPrefDataDump : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] rscr, avg, numPlay;

    private void Start()
    {
        for (int i = 1; i < 18; i++)
        {
            Debug.Log(i + " , " + GameManager.Instance.GetRecentScore(i));
            rscr[i].text = GameManager.Instance.GetRecentScore(i).ToString();
            avg[i].text = GameManager.Instance.GetAverage(i).ToString("F2");
            numPlay[i].text = GameManager.Instance.GetNumberOfGamesPlayed(i).ToString();
        }
    }

    public void ResetPlayerPrefData()
    {
        GameManager.Instance.ResetPlayerPrefs();
    }
}
 