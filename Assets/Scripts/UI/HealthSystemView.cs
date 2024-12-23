using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthSystemView : MonoBehaviour
    {
        [SerializeField] private Sprite _redHeart;
        [SerializeField] private Sprite _blackHeart;
        [SerializeField] private Image[] _hearts;

        private HealthSystem _healthSystem;

        [Inject]
        private void Construct(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
        }

        private void OnEnable()
        {
            _healthSystem.PlayerDamaged += UpdateHealthImages;
            _healthSystem.PlayerHealed += UpdateHealthImages;
            _healthSystem.PlayerDied += UpdateHealthImages;
        }

        private void OnDisable()
        {
            _healthSystem.PlayerDamaged -= UpdateHealthImages;
            _healthSystem.PlayerHealed -= UpdateHealthImages;
            _healthSystem.PlayerDied -= UpdateHealthImages;
        }

        private void UpdateHealthImages()
        {
            var health = _healthSystem.Health;

            for (var i = 0; i < health; i++)
                _hearts[i].sprite = _redHeart;

            for (var i = health; i < _hearts.Length; i++)
                _hearts[i].sprite = _blackHeart;
        }
    }
}