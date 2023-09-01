using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LemApperson.WorldCapitals
{
    [RequireComponent(typeof(LineRenderer))]
    public class DraggableDot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private int _countryIndex;
        [SerializeField] private Image _image;
        [SerializeField] private  GameObject _parentDot;
        [SerializeField] private LineRenderer _lineRenderer;
        private bool _reachedStateSlot , _isWiggling;

        void Start()
        {
            _image = GetComponent<Image>();
            _isWiggling = true;
            StartCoroutine(Wiggling());
        }

        private void Update()
        {
             _lineRenderer.SetPosition(0, _parentDot.transform.position);
             _lineRenderer.SetPosition(2, transform.position);
        }

        public void SetData(int countryIndex, Color dotColor)
        {
            _countryIndex = countryIndex;
            _image.color = dotColor;
            _lineRenderer.startColor = dotColor;
            _lineRenderer.endColor = dotColor;
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
                transform.position = _parentDot.transform.position;
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
                transform.position = _parentDot.transform.position;
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            }
        }
    }
}