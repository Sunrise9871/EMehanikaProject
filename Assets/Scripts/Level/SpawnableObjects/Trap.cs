using DG.Tweening;
using UnityEngine;

namespace Level.SpawnableObjects
{
    public class Trap : SpawnableObject
    {
        private const float AnimationDuration = 0.5f;
        private const int RotationAngle = 360;
        
        [SerializeField] private int _damageAmount = 1;
        
        private void OnTriggerEnter2D(Collider2D enteredCollider)
        {
            var player = enteredCollider.GetComponent<Player.Player>();

            if (player == null)
                return;
            
            player.HealthSystem.TakeDamage(_damageAmount);
            
            PlayRotationAnimation();
        }

        private void PlayRotationAnimation()
        {
            transform
                .DORotate(new Vector3(0, 0, RotationAngle), AnimationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }
    }
}