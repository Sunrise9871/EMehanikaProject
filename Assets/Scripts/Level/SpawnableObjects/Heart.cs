using DG.Tweening;
using UnityEngine;

namespace Level.SpawnableObjects
{
    public class Heart : SpawnableObject
    {
        private const float AnimationDuration = 0.5f;
        
        [SerializeField] private int _healingAmount = 1;
        
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = _originalScale;
        }

        private void OnTriggerEnter2D(Collider2D enteredCollider)
        {
            var player = enteredCollider.GetComponent<Player.Player>();

            if (player is null)
                return;
            
            player.HealthSystem.TakeHealing(_healingAmount);
            
            PlayScaleAnimation();
        }

        private void PlayScaleAnimation()
        {
            transform
                .DOScale(Vector3.zero, AnimationDuration)
                .SetLink(gameObject)
                .OnKill(() =>
                {
                    if (gameObject != null)
                        Release();
                });
        }
    }
}