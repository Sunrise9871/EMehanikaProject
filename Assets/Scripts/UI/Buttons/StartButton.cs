using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class StartButton : MonoBehaviour
    {
        private Button _startButton;

        private void Awake()
        {
            _startButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
