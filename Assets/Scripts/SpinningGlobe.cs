using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinningGlobe : MonoBehaviour
{
    [SerializeField] private Image _spinngGlobeImage;
    [SerializeField] private Sprite[] _SpinningGlobeFrames;
    [SerializeField] private bool _keepSpinning = true;
    private int _currentFrame = 0;

    private void Start() {
        StartCoroutine(SpinningGlobeAnimation());
    }

    private IEnumerator SpinningGlobeAnimation() {
        while (_keepSpinning) {
            _spinngGlobeImage.sprite = _SpinningGlobeFrames[_currentFrame];
            _currentFrame = (_currentFrame + 1) % _SpinningGlobeFrames.Length;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
