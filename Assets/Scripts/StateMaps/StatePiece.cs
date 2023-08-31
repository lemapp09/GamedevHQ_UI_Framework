using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LemApperson.StateMaps
{
    public class StatePiece : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        // Use USPS State Abbreviations
        [SerializeField] public string _stateName;
        private Vector3 _initialPosition;
        private Image _image;
        public bool _reachedStateSlot , _isWiggling;

        void Start()
        {
            _image = GetComponent<Image>();
            _initialPosition = transform.position;
            _isWiggling = true;
            StartCoroutine(Wiggling());
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var temp = _image.color;
            temp.a = 1.0f;
            _image.color = temp;
            _image.raycastTarget = true;
            if (!_reachedStateSlot)
            {
                transform.position = _initialPosition;
                _isWiggling = true;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var temp = _image.color;
            temp.a = 0.5f;
            _image.color = temp;
            _image.raycastTarget = false;
            _isWiggling = false;
        }

        private IEnumerator Wiggling()
        {
            while(_isWiggling) {
                transform.position += new Vector3(UnityEngine.Random.Range(1f, 15f), UnityEngine.Random.Range(1f, 15f), 0);
                yield return new WaitForSeconds(0.3f);
                transform.position = _initialPosition;
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            }
        }
    }
}
