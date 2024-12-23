using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class HelpScreenCloseButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _helpScreen;
        [SerializeField] private RectTransform[] _slides;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(CloseHelpScreen);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CloseHelpScreen);
        }

        private void CloseHelpScreen()
        {
            foreach (var slide in _slides)
                slide.gameObject.SetActive(false);
            
            _helpScreen.gameObject.SetActive(false);
        }
    }
}