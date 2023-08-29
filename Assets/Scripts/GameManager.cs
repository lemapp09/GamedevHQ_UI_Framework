using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region PlayerPrefsVariables
        // Player Settings for 3 games : recent score, average, number of times played
        // PlayerPref does not support arrays
        private int _rScr1, _rScr2, _rScr3,  _rScr4, _rScr5, _rScr6; 
        private int _numPly1, _numPly2, _numPly3,  _numPly4, _numPly5, _numPly6 ;
        private float _avgScr1, _avgScr2, _avgScr3, _avgScr4, _avgScr5, _avgScr6 ;
        private string _playerName = "";
    #endregion  
    #region PlayerPrefMethods
        public void SetRecentScore(int gameNumber, int score)
    {
        switch(gameNumber)
        {
            case 1:
                _rScr1 = score;
                _avgScr1 = FindAverage(score, _avgScr1, _numPly1);
                _numPly1++;
                break;
            case 2:
                _rScr2 = score;
                _avgScr2 = FindAverage(score, _avgScr2, _numPly2);
                _numPly2++;
                break;
            case 3:
                _rScr3 = score;
                _avgScr3 = FindAverage(score, _avgScr3, _numPly3);
                _numPly3++;
                break;
            case 4:
                _rScr4 = score;
                _avgScr4 = FindAverage(score, _avgScr4, _numPly4);
                _numPly4++;
                break;
            case 5:
                _rScr5 = score;
                _avgScr5 = FindAverage(score, _avgScr5, _numPly5);
                _numPly5++;
                break;
            case 6:
                _rScr6 = score;
                _avgScr6 = FindAverage(score, _avgScr6, _numPly6);
                _numPly6++;
                break;
        }
    }
        private float FindAverage(int score, float recentScore, int numberOfTimesPlayed)
    {
        return ((recentScore * numberOfTimesPlayed) + score) / (numberOfTimesPlayed + 1);
    }

        private float GetAverage(int gameNumber)
    {
        switch(gameNumber)
        {
            case 1:
                return _avgScr1;
            case 2:
                return _avgScr2;
            case 3:
                return _avgScr3;
            case 4:
                return _avgScr4;
            case 5:
                return _avgScr5;
            case 6:
                return _avgScr6;
        }
        return 0;
    }

        private int GetNumberOfGamesPlayed(int gameNumber)
    {
        switch (gameNumber)
        {
            case 1:
                return _numPly1;
            case 2:
                return _numPly2;
            case 3:
                return _numPly3;
            case 4:
                return _numPly4;
            case 5:
                return _numPly5;
            case 6:
                return _numPly6;
        }

        return 0;
    }

        public void SetPlayerName(string playerName) {
        _playerName = playerName;
    }
    
        public  string GetPlayerName() {
            return _playerName;
    }

        public void SaveToPlayerPref()
    {
        PlayerPrefs.SetInt("rScr1", _rScr1);
        PlayerPrefs.SetInt("rScr2", _rScr2);
        PlayerPrefs.SetInt("rScr3", _rScr3);
        PlayerPrefs.SetInt("rScr4", _rScr4);
        PlayerPrefs.SetInt("rScr5", _rScr5);
        PlayerPrefs.SetInt("rScr6", _rScr6);
        PlayerPrefs.SetFloat("avgScr1", _avgScr1);
        PlayerPrefs.SetFloat("avgScr2", _avgScr2);
        PlayerPrefs.SetFloat("avgScr3", _avgScr3);
        PlayerPrefs.SetFloat("avgScr4", _avgScr4);
        PlayerPrefs.SetFloat("avgScr5", _avgScr5);
        PlayerPrefs.SetFloat("avgScr6", _avgScr6);
        PlayerPrefs.SetInt("numPly1", _numPly1);
        PlayerPrefs.SetInt("numPly2", _numPly2);
        PlayerPrefs.SetInt("numPly3", _numPly3);
        PlayerPrefs.SetInt("numPly4", _numPly4);
        PlayerPrefs.SetInt("numPly5", _numPly5);
        PlayerPrefs.SetInt("numPly6", _numPly6);
        PlayerPrefs.SetString("PlyrNm", _playerName);
    }

        public void GetFromPlayerPref()
    {
        _rScr1 = PlayerPrefs.GetInt("rScr1");
        _rScr2 = PlayerPrefs.GetInt("rScr");
        _rScr3 = PlayerPrefs.GetInt("rScr3");
        _rScr4 = PlayerPrefs.GetInt("rScr4");
        _rScr5 = PlayerPrefs.GetInt("rScr5");
        _rScr6 = PlayerPrefs.GetInt("rScr6");
        _avgScr1 = PlayerPrefs.GetFloat("avgScr1");
        _avgScr2 = PlayerPrefs.GetFloat("avgScr2");
        _avgScr3 = PlayerPrefs.GetFloat("avgScr3");
        _avgScr4 = PlayerPrefs.GetFloat("avgScr4");
        _avgScr5 = PlayerPrefs.GetFloat("avgScr5");
        _avgScr6 = PlayerPrefs.GetFloat("avgScr6");
        _numPly1 = PlayerPrefs.GetInt("numPly1");
        _numPly2 = PlayerPrefs.GetInt("numPly2");
        _numPly3 = PlayerPrefs.GetInt("numPly3");
        _numPly4 = PlayerPrefs.GetInt("numPly4");
        _numPly5 = PlayerPrefs.GetInt("numPly5");
        _numPly6 = PlayerPrefs.GetInt("numPly6");
        _playerName = PlayerPrefs.GetString("PlyrNm");
    }
    #endregion
}

/*
 * Inspiring Cinematic Ambient by Lexin Music [ Music by <a
 * href="https://pixabay.com/users/lexin_music-28841948/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">
 * Aleksey Chistilin</a> from
 * <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=116199">Pixabay</a> ]
*/