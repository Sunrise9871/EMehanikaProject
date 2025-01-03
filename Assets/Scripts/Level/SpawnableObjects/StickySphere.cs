using DG.Tweening;
using UnityEngine;

namespace Level.SpawnableObjects
{
    public class StickySphere : SpawnableObject
    {
        private const float ShakeDuration = 0.5f;
        private const float ShakeStrength = 1f;

        private void OnTriggerStay2D(Collider2D enteredCollider)
        {
            var player = enteredCollider.GetComponent<Player.Player>();

            if (player == null)
                return;

            var detached = player.TryDetachRopeFromSphere(this);
            if (detached)
                PlayShakeAnimation();
        }

        public void AttachRope()
        {
            Player.ToggleRopeAttachment(this);

            PlayShakeAnimation();
        }

        private void PlayShakeAnimation()
        {
            var originalPosition = transform.localPosition;

            transform.DOShakePosition(ShakeDuration, new Vector3(ShakeStrength, ShakeStrength, 0))
                .SetLink(gameObject)
                .OnKill(() => transform.localPosition = originalPosition);
        }
    }
}