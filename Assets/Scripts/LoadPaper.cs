using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LemApperson
{

    public class LoadPaper : MonoBehaviour
    {
        [SerializeField] private Image _paperImage;
        [SerializeField] private GameObject _gameMenu;
        public bool _loadingPaperImage { get; private set; }

        private void Start() {
            _loadingPaperImage = true;
            StartCoroutine(LoadImage());
        }

        private IEnumerator LoadImage() {
            float i = 0;
            Color temp = _paperImage.color;
            while (i < 1) {
                temp.a = i;
                _paperImage.color = temp;
                i += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            _loadingPaperImage = false;
            _gameMenu.SetActive(true);
        }
    }
}