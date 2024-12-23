using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private float _scoreMultiplier = 1f;
        [SerializeField] private TMP_Text _score;

        private Transform _player;

        private float _startHeight;
        private int _scoreAmount;
        
        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player.transform;
        }

        private void Start()
        {
            _startHeight = _player.position.y;
        }

        private void Update()
        {
            var newScore = (_startHeight + _player.position.y) * _scoreMultiplier;

            if (newScore < _scoreAmount)
                return;

            _scoreAmount = (int)Mathf.Round(newScore);

            _score.text = _scoreAmount.ToString();
        }
    }
}