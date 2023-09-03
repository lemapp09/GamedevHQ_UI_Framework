using System.Collections;
using MyNamespace;
using UnityEngine;
using UnityEngine.UI;


namespace LemApperson.WorldFlags
{
    [RequireComponent(typeof(Image))]
    public class WorldFlagTiles : MonoBehaviour
    {
        [SerializeField] private Image _flagImage, _coverImage;
        [SerializeField] private string _countryName;
        [SerializeField] private int _spriteSheetNumber, _spriteNumber;
        [SerializeField] private int _keyIndex;
        [SerializeField] private Sprite _sprite;
        private bool _isActive = true;

        public void SetUp(string returnCountryName, int SheetNumber, int SpriteNumber, int returnCountryIndex) {
            _countryName = returnCountryName;
            _spriteSheetNumber = SheetNumber;
            _spriteNumber = SpriteNumber;
            _keyIndex = returnCountryIndex;
        }
        
        public void SetFlagImage( Sprite  sprite) {
            _flagImage.sprite = sprite;
        }

        public void TileSelected()
        {
            if (_isActive)
            {
                _isActive = false;
                AudioManager.Instance.PlayClick();
                _coverImage.gameObject.SetActive(false);
                WorldFlags.Instance.FlagTileSelected(_keyIndex, this);
            }
        }

        public void TilesMatched()
        {
            _isActive = false;
            AudioManager.Instance.PlayWin();
            Image tempImage = GetComponent<Image>();
            Color tempColor = tempImage.color;
            tempColor.a = 0.5f;
            this.gameObject.GetComponent<Image>().color = tempColor;
        }

        public void TilesDidntMatch()
        {
            _isActive = true;
            AudioManager.Instance.PlayLose();
            StartCoroutine(WaitToFlip());
        }

        private IEnumerator WaitToFlip()
        {
            yield return new WaitForSeconds(2.5f);
            if (_isActive) {
                _coverImage.gameObject.SetActive(true);
            }
        }
    }
}