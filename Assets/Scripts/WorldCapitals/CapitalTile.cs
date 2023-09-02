using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LemApperson.WorldCapitals
{

    public class CapitalTile : MonoBehaviour
    {
        [SerializeField] private Image _dot;
        [SerializeField] private TextMeshProUGUI _capitalNameText;
        private string _capitalName;
        private int _countryIndex;
        private Color _dotColor;


        public void SetCapitalData(string capitalName, int countryIndex, Color dotColor)
        {    
            _capitalName = capitalName;
            _capitalNameText.text = capitalName;
            _countryIndex = countryIndex;
            _dot.GetComponent<CapitalDots>().SetCountryID(countryIndex);
            _dotColor = dotColor;
            _dot.color = dotColor;
        }
    }
}