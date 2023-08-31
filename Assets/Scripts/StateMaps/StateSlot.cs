using System;
using System.Collections;
using LemApperson.StateMaps;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LemApperson.StateMaps
{
    public class StateSlot : MonoBehaviour,  IDropHandler
    {
        [SerializeField] private LemApperson.StateMaps.Score _score;
        // Use USPS State Abbreviations
        [SerializeField] public string _stateName;
        private Vector3 _initialPosition;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            String stateName = eventData.pointerDrag.GetComponent<StatePiece>()._stateName;
            if(stateName == _stateName) {
                eventData.pointerDrag.transform.position = transform.position;
                eventData.pointerDrag.GetComponent<StatePiece>()._reachedStateSlot = true;
                _score.UpdateScore(_stateName);
                AudioManager.Instance.PlayClick();
            }
        }

    }
}