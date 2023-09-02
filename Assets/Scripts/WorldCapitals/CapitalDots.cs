using LemApperson.StateMaps;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LemApperson.WorldCapitals
{
    public class CapitalDots : MonoBehaviour,  IDropHandler
    {
        [SerializeField] public int _countryID;
        [SerializeField] private Vector3 _initialPosition;
        [SerializeField] private Score _score;

        public void SetCountryID(int countryIndex){
            _countryID = countryIndex;
        }
        
        public void OnDrop(PointerEventData eventData) {
            if (eventData.pointerDrag.GetComponent<DraggableDot>().GetCountryIndex() == _countryID) {
                eventData.pointerDrag.transform.position = transform.position;
                DraggableDot _draggableDot = eventData.pointerDrag.GetComponent<DraggableDot>();
                _draggableDot.ReachedCapital(true);
                AudioManager.Instance.PlayClick();
                WorldCapitals.Instance.CapitalMatched();
            }
        }

    }
}