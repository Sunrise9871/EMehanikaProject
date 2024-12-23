using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class HelpScreenOpenButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _helpScreen;
        [SerializeField] private RectTransform _firstSlide;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenHelpScreen);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenHelpScreen);
        }

        private void OpenHelpScreen()
        {
            _helpScreen.gameObject.SetActive(true);
            _firstSlide.gameObject.SetActive(true);
        }
    }
}