using UnityEngine;
using UnityEngine.EventSystems;


namespace LemApperson.WorldCapitals
{
    public class CapitalDots : MonoBehaviour,  IDropHandler, IPointerUpHandler
    {
        [SerializeField] public string _countryID;
        private Vector3 _initialPosition;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop: " + eventData.pointerDrag.name);
            // String stateName = eventData.pointerDrag.GetComponent<StatePiece>()._stateName;
            //    eventData.pointerDrag.transform.position = transform.position;
            //    DraggableDot _draggableDot = eventData.pointerDrag.GetComponent<DraggableDot>();
             //   _draggableDot.ReachedCapital(true);
             //   AudioManager.Instance.PlayClick();
        }


        public void OnPointerUp(PointerEventData eventData)
        {       
            Debug.Log("OnPointerUp: " + eventData.pointerDrag.name);
        }
    }
}