using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LemApperson.WorldCapitals
{
    public class CountryTile : MonoBehaviour
    {
        [SerializeField] private Image _dot;
        [SerializeField] private DraggableDot _draggableDot;
        [SerializeField] private Image _flagImage;
        [SerializeField] private TextMeshProUGUI _countryNameText;
        private Sprite _countrySprite;
        private string _countryName;
        private int _countryIndex, _tileIndex;
        private Color _dotColor;

        public void SetCountryData(int countryIndex, int tileIndex, string countryName,Sprite sprite, Color dotColor)
        {
            _countryIndex = countryIndex;
            _tileIndex = tileIndex;
            _draggableDot.SetData(countryIndex, tileIndex, dotColor);
            _countryName = countryName;
            _countryNameText.text = countryName;
            _countrySprite = sprite;
            _flagImage.sprite = sprite;
            _dotColor = dotColor;
            _dot.color = dotColor;
        }
        
        
    }
}