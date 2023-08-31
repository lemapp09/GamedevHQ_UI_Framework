using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region PlayerPrefsVariables
        // Player Settings for 3 games : recent score, average, number of times played
        // PlayerPref does not support arrays
        private string _playerName = "";
        private float[] _rScr, _avgScr;
        private int[] _numPly;
        private int _numberOfGames;
    #endregion

    private GeoQuizActions _screenCaptureInputs;
    public int _currentGameScore;
    private bool _isFirstTile;

    private void Start()
    {
        _numberOfGames = 10;
        _rScr = new float[_numberOfGames];
        _avgScr = new float[_numberOfGames];
        _numPly = new int[_numberOfGames];
        _screenCaptureInputs = new GeoQuizActions();
        _screenCaptureInputs.Screen.Enable();
        _screenCaptureInputs.Screen.Capture.performed += ctx => ScreenCapture01();
    }

    
    public void ScreenCapture01() {
        ScreenCapture.CaptureScreenshot("Screenshot" + Time.time.ToString() + ".png");
        Debug.Log("Screenshot Captured");
    }
    

        public void SetRecentScore(int gameNumber, float score)
        {
            if (gameNumber <= _numberOfGames) {
                _rScr[gameNumber] = score;
                _avgScr[gameNumber] = FindAverage(score, _avgScr[gameNumber], _numPly[gameNumber]);
                _numPly[gameNumber]++;
            }
        }
        private float FindAverage(float score, float recentScore, int numberOfTimesPlayed) {
            return ((recentScore * numberOfTimesPlayed) + score) / (numberOfTimesPlayed + 1); 
        }

        public float GetAverage(int gameNumber)
        {
            if (gameNumber <= _numberOfGames) {
                return _avgScr[gameNumber];
            } else {
                return 0;
            }
        }

        public int GetNumberOfGamesPlayed(int gameNumber) {
            if (gameNumber <= _numberOfGames) {
                return _numPly[gameNumber];
            }  else {
                return 0;
            }
        }

        public void SetPlayerName(string playerName) {
            _playerName = playerName;
        }
    
        public  string GetPlayerName() {
            return _playerName;
        }
        
        public void GameWon(int gameNumber, int score, float lengthOfPlay) {
            if (lengthOfPlay >= 30.0f) {
                _rScr[gameNumber] = score - (lengthOfPlay % 30.0f);
            } else  {
                _rScr[gameNumber] = score + (lengthOfPlay % 5.0f);
            }
            SetRecentScore(gameNumber, _rScr[gameNumber]);
        }

        public void FlagTileSelected(int keyIndex)
        {
            if (!_isFirstTile)
            {
                _isFirstTile = true;
            }
        }
        
#region PlayerPrefMethods
        public void SaveToPlayerPref()
    {
        PlayerPrefs.SetFloat("rScr1", _rScr[1]);
        PlayerPrefs.SetFloat("rScr2", _rScr[2]);
        PlayerPrefs.SetFloat("rScr3", _rScr[3]);
        PlayerPrefs.SetFloat("rScr4", _rScr[4]);
        PlayerPrefs.SetFloat("rScr5", _rScr[5]);
        PlayerPrefs.SetFloat("rScr6", _rScr[6]);
        PlayerPrefs.SetFloat("rScr7", _rScr[7]);
        PlayerPrefs.SetFloat("rScr8", _rScr[8]);
        PlayerPrefs.SetFloat("rScr9", _rScr[9]);
        PlayerPrefs.SetFloat("rScr10", _rScr[10]);
        PlayerPrefs.SetFloat("avgScr1", _avgScr[1]);
        PlayerPrefs.SetFloat("avgScr2", _avgScr[2]);
        PlayerPrefs.SetFloat("avgScr3", _avgScr[3]);
        PlayerPrefs.SetFloat("avgScr4", _avgScr[4]);
        PlayerPrefs.SetFloat("avgScr5", _avgScr[5]);
        PlayerPrefs.SetFloat("avgScr6", _avgScr[6]);
        PlayerPrefs.SetFloat("avgScr7", _avgScr[7]);
        PlayerPrefs.SetFloat("avgScr8", _avgScr[8]);
        PlayerPrefs.SetFloat("avgScr9", _avgScr[9]);
        PlayerPrefs.SetFloat("avgScr10", _avgScr[10]);
        PlayerPrefs.SetInt("numPly1", _numPly[1]);
        PlayerPrefs.SetInt("numPly2", _numPly[2]);
        PlayerPrefs.SetInt("numPly3", _numPly[3]);
        PlayerPrefs.SetInt("numPly4", _numPly[4]);
        PlayerPrefs.SetInt("numPly5", _numPly[5]);
        PlayerPrefs.SetInt("numPly6", _numPly[6]);
        PlayerPrefs.SetInt("numPly7", _numPly[7]);
        PlayerPrefs.SetInt("numPly8", _numPly[8]);
        PlayerPrefs.SetInt("numPly9", _numPly[9]);
        PlayerPrefs.SetInt("numPly10", _numPly[10]);
        PlayerPrefs.SetString("PlyrNm", _playerName);
    }

        public void GetFromPlayerPref()
    {
        _rScr[1] = PlayerPrefs.GetInt("rScr1");
        _rScr[2] = PlayerPrefs.GetInt("rScr2");
        _rScr[3] = PlayerPrefs.GetInt("rScr3");
        _rScr[4] = PlayerPrefs.GetInt("rScr4");
        _rScr[5] = PlayerPrefs.GetInt("rScr5");
        _rScr[6] = PlayerPrefs.GetInt("rScr6");
        _rScr[7] = PlayerPrefs.GetInt("rScr7");
        _rScr[8] = PlayerPrefs.GetInt("rScr8");
        _rScr[9] = PlayerPrefs.GetInt("rScr9");
        _rScr[10] = PlayerPrefs.GetInt("rScr10");
        _avgScr[1] = PlayerPrefs.GetFloat("avgScr1");
        _avgScr[2] = PlayerPrefs.GetFloat("avgScr2");
        _avgScr[3] = PlayerPrefs.GetFloat("avgScr3");
        _avgScr[4] = PlayerPrefs.GetFloat("avgScr4");
        _avgScr[5] = PlayerPrefs.GetFloat("avgScr5");
        _avgScr[6] = PlayerPrefs.GetFloat("avgScr6");
        _avgScr[7] = PlayerPrefs.GetFloat("avgScr7");
        _avgScr[8] = PlayerPrefs.GetFloat("avgScr8");
        _avgScr[9] = PlayerPrefs.GetFloat("avgScr9");
        _avgScr[10] = PlayerPrefs.GetFloat("avgScr10");
        _numPly[1] = PlayerPrefs.GetInt("numPly1");
        _numPly[2] = PlayerPrefs.GetInt("numPly2");
        _numPly[3] = PlayerPrefs.GetInt("numPly3");
        _numPly[4] = PlayerPrefs.GetInt("numPly4");
        _numPly[5] = PlayerPrefs.GetInt("numPly5");
        _numPly[6] = PlayerPrefs.GetInt("numPly6");
        _numPly[7] = PlayerPrefs.GetInt("numPly7");
        _numPly[8] = PlayerPrefs.GetInt("numPly8");
        _numPly[9] = PlayerPrefs.GetInt("numPly9");
        _numPly[10] = PlayerPrefs.GetInt("numPly10");
        _playerName = PlayerPrefs.GetString("PlyrNm");
    }
 #endregion

    private void OnDisable()
    {
        _screenCaptureInputs.Screen.Disable();
    }

}

/*
 * Inspiring Cinematic Ambient by Lexin Music [ Music by <a
 * href="https://pixabay.com/users/lexin_music-28841948/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">
 * Aleksey Chistilin</a> from
 * <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">Pixabay</a> ]
*/