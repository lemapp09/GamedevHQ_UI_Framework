using System;
using MyNamespace;
using UnityEngine;
using TMPro;

namespace LemApperson.StateMaps
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Score : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverDisplay;
        [Tooltip("The number of the game this score represents. This is used to determine which game is won.")]
        [Range(1, 11)]
        [SerializeField] private int _gameNumber;
        private TextMeshProUGUI _scoreText;
        private int _score, _flagCount;
        private float _lengthOfPlay = 0;
        private float _startTime;

        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
            _startTime = Time.time;
        }

        public void UpdateStateScore(string state)
        {
            string fullStateName = LookupState(state);
            _score++;
            _scoreText.text = "Score: " + _score.ToString() + " : " + fullStateName;
            if (_score == 10) {
                _lengthOfPlay = Time.time - _startTime;
                AudioManager.Instance.PlayWin();
                GameManager.Instance.GameWon(1, _score, _lengthOfPlay);
                _gameOverDisplay.SetActive(true);
            }
        }

        public void UpdateFlagScore(int score)
        {
            if (score > 0) {
                _flagCount++;
            }
            _score += score;
            _scoreText.text = "Score: " + _score.ToString() + " : " ;
            if (_flagCount == 18) {
                _lengthOfPlay = Time.time - _startTime;
                GameManager.Instance.GameWon(_gameNumber, _score, _lengthOfPlay);
                _gameOverDisplay.SetActive(true);
            }
        }

        private string LookupState(string state)
        {
            switch (state)
            {
                case "AL":
                    return "Alabama";
                case "AK":
                    return "Alaska";
                case "AZ":
                    return "Arizona";
                case "AR":
                    return "Arkansas";
                case "CA":
                    return "California";
                case "CO":
                    return "Colorado";
                case "CT":
                    return "Connecticut";
                case "DE":
                    return "Delaware";
                case "FL":
                    return "Florida";
                case "GA":
                    return "Georgia";
                case "HI":
                    return "Hawaii";
                case "ID":
                    return "Idaho";
                case "IL":
                    return "Illinois";
                case "IN":
                    return "Indiana";
                case "IA":
                    return "Iowa";
                case "KS":
                    return "Kansas";
                case "KY":
                    return "Kentucky";
                case "LA":
                    return "Louisiana";
                case "ME":
                    return "Maine";
                case "MD":
                    return "Maryland";
                case "MA":
                    return "Massachusetts";
                case "MI":
                    return "Michigan";
                case "MN":
                    return "Minnesota";
                case "MS":
                    return "Mississippi";
                case "MO":
                    return "Missouri";
                case "MT":
                    return "Montana";
                case "NE":
                    return "Nebraska";
                case "NV":
                    return "Nevada";
                case "NH":
                    return "New Hampshire";
                case "NJ":
                    return "New Jersey";
                case "NM":
                    return "New Mexico";
                case "NY":
                    return "New York";
                case "NC":
                    return "North Carolina";
                case "ND":
                    return "North Dakota";
                case "OH":
                    return "Ohio";
                case "OK":
                    return "Oklahoma";
                case "OR":
                    return "Oregon";
                case "PA":
                    return "Pennsylvania";
                case "RI":
                    return "Rhode Island";
                case "SC":
                    return "South Carolina";
                case "SD":
                    return "South Dakota";
                case "TN":
                    return "Tennessee";
                case "TX":
                    return "Texas";
                case "UT":
                    return "Utah";
                case "VT":
                    return "Vermont";
                case "VA":
                    return "Virginia";
                case "WA":
                    return "Washington";
                case "WV":
                    return "West Virginia";
                case "WI":
                    return "Wisconsin";
                case "WY":
                    return "Wyoming";
            }
            return "";
        }
    }
}