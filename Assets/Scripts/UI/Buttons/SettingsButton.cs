using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SettingsButton : MonoBehaviour
    {
        private const float AnimationDuration = 0.5f;

        [SerializeField] private RectTransform _settingsPanel;

        private Vector2 _targetPosition;
        private Vector2 _startPosition;
        private bool _isVisible;

        private Button _startButton;

        private void Awake()
        {
            _startButton = GetComponent<Button>();
        }

        private void Start()
        {
            _targetPosition = _settingsPanel.anchoredPosition;
            _startPosition = new Vector2(_settingsPanel.anchoredPosition.x,
                Screen.height / 2f + _settingsPanel.rect.height / 2f);
            
            _settingsPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(ToggleSettings);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(ToggleSettings);
        }

        private void ToggleSettings()
        {
            if (!_isVisible)
            {
                _settingsPanel.anchoredPosition = _startPosition;
                _settingsPanel.gameObject.SetActive(true);
                _settingsPanel
                    .DOAnchorPos(_targetPosition, AnimationDuration)
                    .SetLink(_settingsPanel.gameObject);
            }
            else
            {
                _settingsPanel.anchoredPosition = _targetPosition;
                _settingsPanel
                    .DOAnchorPos(_startPosition, AnimationDuration)
                    .SetLink(_settingsPanel.gameObject)
                    .OnKill(DisableSettingsPanel);
            }

            _isVisible = !_isVisible;
        }

        private void DisableSettingsPanel()
        {
            if (_settingsPanel != null)
                _settingsPanel.gameObject.SetActive(false);
        }
    }
}