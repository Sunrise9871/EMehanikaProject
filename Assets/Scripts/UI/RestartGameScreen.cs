using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class RestartGameScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _loseScreen;
        [SerializeField] private Button _restartButton;
        
        private HealthSystem _healthSystem;

        [Inject]
        private void Construct(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
        }
        
        private void OnEnable()
        {
            _healthSystem.PlayerDied += ShowLoseScreen;
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _healthSystem.PlayerDied -= ShowLoseScreen;
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private void ShowLoseScreen()
        {
            Time.timeScale = 0f;
            
            _loseScreen.gameObject.SetActive(true);
        }

        private void RestartGame()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

            Time.timeScale = 1f;
        }
    }
}