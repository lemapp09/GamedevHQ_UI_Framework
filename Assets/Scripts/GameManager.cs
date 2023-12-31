using UnityEngine;

namespace MyNamespace
{
    
    public class GameManager : MonoSingleton<GameManager>
    {
        #region PlayerPrefsVariables
            // Player Settings for 3 games : recent score, average, number of times played
            // PlayerPref does not support arrays
            private string _playerName = "";
            private float[] _rScr, _avgScr;
            private int[] _numPly;
            private float _masterVolume, _ambienceVolume, _sfxVolume;
            private int _numberOfGames;
            public int NumberOfGames { get { return _numberOfGames; } }
        #endregion

        private GeoQuizActions _screenCaptureInputs;

        private void Start()
        {
            _numberOfGames = 18;
            _rScr = new float[_numberOfGames];
            _avgScr = new float[_numberOfGames];
            _numPly = new int[_numberOfGames];
            if (PlayerPrefs.HasKey("PlyrNm")) {
                GetFromPlayerPref();
            }
            _screenCaptureInputs = new GeoQuizActions();
            _screenCaptureInputs.Screen.Enable();
            _screenCaptureInputs.Screen.Capture.performed += ctx => ScreenCapture01();
        }
        
        public void ScreenCapture01()
        {
            ScreenCapture.CaptureScreenshot("Screenshot" + Time.time.ToString() + ".png");
            Debug.Log("Screenshot Captured");
        }

        public void SetRecentScore(int gameNumber, float score)
        {
            if (gameNumber <= _numberOfGames)
            {
                _rScr[gameNumber] = score;
                _avgScr[gameNumber] = FindAverage(score, _avgScr[gameNumber], _numPly[gameNumber]);
                _numPly[gameNumber]++;
            }
        }

        private float FindAverage(float score, float recentScore, int numberOfTimesPlayed)
        {
            return ((recentScore * numberOfTimesPlayed) + score) / (numberOfTimesPlayed + 1);
        }

        public float OverAllAverage()
        {
                float sum = 0;
                int numOfGamesPlayed = 0;
            for (int i = 0; i < _numberOfGames; i++)
            {
                sum += _avgScr[i];
                if (_avgScr[i] > 0) numOfGamesPlayed ++;
            }
            return sum / numOfGamesPlayed;
        }

        public int TotalNumberOfGamesPlayed()
        {
            int sum = 0;
            for (int i = 0; i < _numberOfGames; i++) {
                sum += _numPly[i];
            }
            return sum;
        }

        public float GetRecentScore(int gameNumber)
        {
            if (gameNumber <= _numberOfGames)
            {
                return _rScr[gameNumber];
            }
            else
            {
                return 0;
            }
        }

        public float GetAverage(int gameNumber)
        {
            if (gameNumber <= _numberOfGames)
            {
                return _avgScr[gameNumber];
            }
            else
            {
                return 0;
            }
        }

        public int GetNumberOfGamesPlayed(int gameNumber)
        {
            if (gameNumber <= _numberOfGames)
            {
                return _numPly[gameNumber];
            }
            else
            {
                return 0;
            }
        }

        public void SetPlayerName(string playerName)
        {
            _playerName = playerName;
            SaveToPlayerPref();
        }

        public string GetPlayerName()
        {
            return _playerName;
        }

        public void SetVolumes(float mVol, float aVol, float eVol)
        {
            _masterVolume = mVol;
            _ambienceVolume = aVol;
            _sfxVolume = eVol;
        }

        public (float, float, float) GetVolumes()
        {
            _masterVolume = PlayerPrefs.GetFloat("mVol");
            _ambienceVolume = PlayerPrefs.GetFloat("aVol");
            _sfxVolume = PlayerPrefs.GetFloat("eVol");
            return (_masterVolume, _ambienceVolume, _sfxVolume);
        }

        public void GameWon(int gameNumber, int score, float lengthOfPlay)
        {
            if (lengthOfPlay >= 60.0f)
            {
                _rScr[gameNumber] = score * 0.95f ;
            }
            else
            {
                _rScr[gameNumber] = score ;
            }

            SetRecentScore(gameNumber, _rScr[gameNumber]);
            SaveToPlayerPref();
        }

        public void ResetPlayerPrefs()
        {
            for (int i = 1; i < _numberOfGames; i++)
            {
                _rScr[i] = _avgScr[i] = _numPly[i] = 0;
            }
            SaveToPlayerPref();
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
            PlayerPrefs.SetFloat("rScr11", _rScr[11]);
            PlayerPrefs.SetFloat("rScr12", _rScr[12]);
            PlayerPrefs.SetFloat("rScr13", _rScr[13]);
            PlayerPrefs.SetFloat("rScr14", _rScr[14]);
            PlayerPrefs.SetFloat("rScr15", _rScr[15]);
            PlayerPrefs.SetFloat("rScr16", _rScr[16]);
            PlayerPrefs.SetFloat("rScr17", _rScr[17]);
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
            PlayerPrefs.SetFloat("avgScr11", _avgScr[11]);
            PlayerPrefs.SetFloat("avgScr12", _avgScr[12]);
            PlayerPrefs.SetFloat("avgScr13", _avgScr[13]);
            PlayerPrefs.SetFloat("avgScr14", _avgScr[14]);
            PlayerPrefs.SetFloat("avgScr15", _avgScr[15]);
            PlayerPrefs.SetFloat("avgScr16", _avgScr[16]);
            PlayerPrefs.SetFloat("avgScr17", _avgScr[17]);
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
            PlayerPrefs.SetInt("numPly11", _numPly[11]);
            PlayerPrefs.SetInt("numPly12", _numPly[12]);
            PlayerPrefs.SetInt("numPly13", _numPly[13]);
            PlayerPrefs.SetInt("numPly14", _numPly[14]);
            PlayerPrefs.SetInt("numPly15", _numPly[15]);
            PlayerPrefs.SetInt("numPly16", _numPly[16]);
            PlayerPrefs.SetInt("numPly17", _numPly[17]);
            PlayerPrefs.SetString("PlyrNm", _playerName);
            PlayerPrefs.SetFloat("mVol", _masterVolume);
            PlayerPrefs.SetFloat("aVol", _ambienceVolume);
            PlayerPrefs.SetFloat("eVol", _sfxVolume);
            PlayerPrefs.Save();
        }

        public void GetFromPlayerPref()
        {
            _rScr[1] = PlayerPrefs.GetFloat("rScr1");
            _rScr[2] = PlayerPrefs.GetFloat("rScr2");
            _rScr[3] = PlayerPrefs.GetFloat("rScr3");
            _rScr[4] = PlayerPrefs.GetFloat("rScr4");
            _rScr[5] = PlayerPrefs.GetFloat("rScr5");
            _rScr[6] = PlayerPrefs.GetFloat("rScr6");
            _rScr[7] = PlayerPrefs.GetFloat("rScr7");
            _rScr[8] = PlayerPrefs.GetFloat("rScr8");
            _rScr[9] = PlayerPrefs.GetFloat("rScr9");
            _rScr[10] = PlayerPrefs.GetFloat("rScr10");
            _rScr[11] = PlayerPrefs.GetFloat("rScr11");
            _rScr[12] = PlayerPrefs.GetFloat("rScr12");
            _rScr[13] = PlayerPrefs.GetFloat("rScr13");
            _rScr[14] = PlayerPrefs.GetFloat("rScr14");
            _rScr[15] = PlayerPrefs.GetFloat("rScr15");
            _rScr[16] = PlayerPrefs.GetFloat("rScr16");
            _rScr[17] = PlayerPrefs.GetFloat("rScr17");
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
            _avgScr[11] = PlayerPrefs.GetFloat("avgScr11");
            _avgScr[12] = PlayerPrefs.GetFloat("avgScr12");
            _avgScr[13] = PlayerPrefs.GetFloat("avgScr13");
            _avgScr[14] = PlayerPrefs.GetFloat("avgScr14");
            _avgScr[15] = PlayerPrefs.GetFloat("avgScr15");
            _avgScr[16] = PlayerPrefs.GetFloat("avgScr16");
            _avgScr[17] = PlayerPrefs.GetFloat("avgScr17");
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
            _numPly[11] = PlayerPrefs.GetInt("numPly11");
            _numPly[12] = PlayerPrefs.GetInt("numPly12");
            _numPly[13] = PlayerPrefs.GetInt("numPly13");
            _numPly[14] = PlayerPrefs.GetInt("numPly14");
            _numPly[15] = PlayerPrefs.GetInt("numPly15");
            _numPly[16] = PlayerPrefs.GetInt("numPly16");
            _numPly[17] = PlayerPrefs.GetInt("numPly17"); 
            _playerName = PlayerPrefs.GetString("PlyrNm");
            _masterVolume = PlayerPrefs.GetFloat("mVol");
            _ambienceVolume = PlayerPrefs.GetFloat("aVol");
            _sfxVolume = PlayerPrefs.GetFloat("eVol");
        }
        

        #endregion

        private void OnDisable()
        {
            _screenCaptureInputs.Screen.Disable();
            _screenCaptureInputs.Screen.Capture.performed -= ctx => ScreenCapture01();
            SaveToPlayerPref();
        }

    }
}
/*
 * Inspiring Cinematic Ambient by Lexin Music [ Music by <a
 * href="https://pixabay.com/users/lexin_music-28841948/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">
 * Aleksey Chistilin</a> from
 * <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">Pixabay</a> ]
*/