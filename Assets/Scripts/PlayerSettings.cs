using MyNamespace;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace LemApperson
{
    public class PlayerSettings : MonoBehaviour
    {
        [SerializeField] private GameObject _playerSettingsDisplay;
        [SerializeField] private TMP_InputField _playerNameInput;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _ambienceSlider;
        [SerializeField] private Slider _effectsSlider;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private GameObject _scores_Grid;
        [SerializeField] private GameObject _prefabTile;
        private GeoQuizActions _toggleSettings;
        private bool _screenVisible;

        private void Start()
        {
            _toggleSettings = new GeoQuizActions();
            _toggleSettings.Settings.Enable();
            _toggleSettings.Settings.Toggle.performed += ctx => ToggleSettings();
            GetVolumes();
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            int displayLimit = 16;
            int numberOfGames = GameManager.Instance.NumberOfGames;
            for (int i = 1; i < numberOfGames; i++)
            {
                int gamesplayed = GameManager.Instance.GetNumberOfGamesPlayed(i);
                if (gamesplayed > 0) {
                    GameObject newGameTile = Instantiate(_prefabTile, _scores_Grid.transform.position, Quaternion.identity, _scores_Grid.transform);
                    SettingsTile tile = newGameTile.GetComponent<SettingsTile>();
                    tile.SetData(gamesplayed,GetGameName(i),GameManager.Instance.GetAverage(i)); 
                }
                displayLimit--;
                if (displayLimit < 1) { break; }
            }
        }

        private string GetGameName(int i)
        {
            switch (i)
            {
                case 1:
                    return "State Maps A";
                case 2:
                    return "State Maps B";
                case 3:
                    return "State Maps C";
                case 4:
                    return "State Maps D";
                case 5:
                    return "State Maps E";
                case 6:
                    return "Flags - Africa";
                case 7:
                    return "Flags - Asia";
                case 8:
                    return "Flags - Europe";
                case 9:
                    return "Flags - North America";
                case 10:
                    return "Flags - Oceania";
                case 11:
                    return "Flags - South America";
                case 12:
                    return "Capitals - Africa";
                case 13:
                    return "Capitals - Asia";
                case 14:
                    return "Capitals - Europe";
                case 15:
                    return "Capitals - North America";
                case 16:
                    return "Capitals - Oceania";
                case 17:
                    return "Capitals - South America";
                default:
                    return "Game";
            }
        }

        private void ToggleSettings() {
            _screenVisible = !_screenVisible;
            _playerSettingsDisplay.SetActive(_screenVisible);
            if (_screenVisible) {
                Time.timeScale = 0; 
            }  else {
                Time.timeScale = 1;
            }
            _playerNameInput.text = GameManager.Instance.GetPlayerName();
        }

        public void GetVolumes()
        {
            (float mVol, float aVol, float eVol) = GameManager.Instance.GetVolumes();
            _mixer.SetFloat("MasterVolume", mVol);
            _volumeSlider.value = mVol;
            _mixer.SetFloat("AmbientVolume", aVol);
            _ambienceSlider.value = aVol;
            _mixer.SetFloat("SFXVolume", eVol);
            _effectsSlider.value = eVol;
        }


        public void ChangeVolume() {
            _mixer.SetFloat("MasterVolume", _volumeSlider.value);
            _mixer.SetFloat("AmbientVolume", _ambienceSlider.value);
            _mixer.SetFloat("SFXVolume", _effectsSlider.value);
            GameManager.Instance.SetVolumes(_volumeSlider.value, _ambienceSlider.value, _effectsSlider.value);
        }
        
        public void SetPlayerName(){
            GameManager.Instance.SetPlayerName(_playerNameInput.text); 
        }

        private void OnDisable()
        {
            
            _toggleSettings.Settings.Disable();
            _toggleSettings.Settings.Toggle.performed += ctx => ToggleSettings();
        }
    }
}