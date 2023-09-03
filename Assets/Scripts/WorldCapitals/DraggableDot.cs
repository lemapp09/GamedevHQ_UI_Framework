using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LemApperson.WorldCapitals
{
    
    public class DraggableDot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private int _countryIndex, _tileIndex;
        [SerializeField] private Image _image;
        [SerializeField] private  GameObject _parentDot;
        [SerializeField] private UnityEngine.UI.Extensions.UILineRenderer _uiLineRenderer;
        [SerializeField] private bool _reachedCapitalSlot , _isWiggling;
        private Vector2 _scaleReference;

        void Start()
        {
            CanvasScaler canvasScaler = GetComponentInParent<CanvasScaler>();
            _scaleReference = new Vector2(canvasScaler.referenceResolution.x / Screen.width,
                canvasScaler.referenceResolution.y / Screen.height);
            _image = GetComponent<Image>();
            _isWiggling = true;
            StartCoroutine(Wiggling());
        }

        private void Update()
        {
            Vector2 point1 = new Vector2(0, 0);
            Vector2 point2 = new Vector2((transform.position.x - _parentDot.transform.position.x) * _scaleReference.y,
                (transform.position.y - _parentDot.transform.position.y) * _scaleReference.y);
            Vector2[] line = { point1, point2 };
            _uiLineRenderer.Points = line;
        }

        public void SetData(int countryIndex, int _tileIndex, Color dotColor)
        {
            _countryIndex = countryIndex;
            _image.color = dotColor;
            _uiLineRenderer.color = dotColor;
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
            if (!_reachedCapitalSlot)
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

        public void ReachedCapital(bool reached)
        {
            _reachedCapitalSlot = true;
        }

        public int GetCountryIndex()
        {
            return _countryIndex;
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