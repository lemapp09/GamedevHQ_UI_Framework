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
        private int _countryIndex;
        private Color _dotColor;

        public void SetCountryData(int countryIndex, string countryName,Sprite sprite, Color dotColor)
        {
            _countryIndex = countryIndex;
            _draggableDot.SetData(countryIndex, dotColor);
            _countryName = countryName;
            _countryNameText.text = countryName;
            _countrySprite = sprite;
            _flagImage.sprite = sprite;
            _dotColor = dotColor;
            _dot.color = dotColor;
        }
        
        
    }
}