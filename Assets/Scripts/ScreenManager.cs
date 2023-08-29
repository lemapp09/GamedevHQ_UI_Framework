using UnityEngine;
using UnityEngine.SceneManagement;

namespace LemApperson
{

    public class ScreenManager : MonoBehaviour
    {
        [Header("Screen Number To Load")]
        [Tooltip("0-MainMenu, 1-ChooseGame, 2-Game1, 3-Game2, 4-Game3, 5-GameOver")]
        [SerializeField]
        private int _ScreenNumberToLoad;

        public void AdvanceScreen()
        {
            SceneManager.LoadSceneAsync(_ScreenNumberToLoad);
        }
    }
}