using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NextSlideToggle : MonoBehaviour
    {
        [SerializeField] private Button _nextSlideButton;
        [SerializeField] private RectTransform _nextSlide;

        private void OnEnable()
        {
            _nextSlideButton.onClick.AddListener(OpenNextSlide);
        }

        private void OnDisable()
        {
            _nextSlideButton.onClick.RemoveListener(OpenNextSlide);
        }

        private void OpenNextSlide()
        {
            _nextSlide.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}