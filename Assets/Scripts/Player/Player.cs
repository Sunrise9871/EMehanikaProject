using Level.SpawnableObjects;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _upwardSpeed;
        [SerializeField] private float _attractionStrength;
        [SerializeField] private Rope[] _ropes;

        private Rigidbody2D _rigidbody;

        public HealthSystem HealthSystem { get; private set; }

        [Inject]
        private void Construct(HealthSystem healthSystem)
        {
            HealthSystem = healthSystem;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var upwardVelocity = new Vector2(0f, _upwardSpeed);

            var attractionVelocity = Vector2.zero;
            var activeRopes = 0;

            foreach (var rope in _ropes)
            {
                if (!rope.IsAttached)
                    continue;

                attractionVelocity += rope.GetAttractionForce(transform.position, _attractionStrength);
                activeRopes++;
            }

            if (activeRopes > 0)
                attractionVelocity /= activeRopes;

            _rigidbody.velocity = upwardVelocity + attractionVelocity;
        }

        public void ToggleRopeAttachment(StickySphere stickySphere)
        {
            if (TryDetachRopeFromSphere(stickySphere))
                return;
            
            TryAttachRopeToSphere(stickySphere);
        }
        
        public bool TryDetachRopeFromSphere(StickySphere stickySphere)
        {
            foreach (var rope in _ropes)
            {
                if (!rope.IsAttached || rope.AttachedSphere != stickySphere)
                    continue;

                rope.Detach();
                return true;
            }

            return false;
        }
        
        private bool TryAttachRopeToSphere(StickySphere stickySphere)
        {
            foreach (var rope in _ropes)
            {
                if (rope.IsAttached)
                    continue;

                rope.Attach(stickySphere);
                return true;
            }

            return false;
        }

        public void DetachAllRopes()
        {
            foreach (var rope in _ropes)
                rope.Detach();
        }
    }
}