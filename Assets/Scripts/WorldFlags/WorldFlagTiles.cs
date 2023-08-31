using System;
using UnityEditor;
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
            _coverImage.GetComponent<GameObject>().SetActive(false);
            GameManager.Instance.FlagTileSelected(_keyIndex);
        }

    }
}