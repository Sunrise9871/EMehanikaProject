using Level.SpawnableObjects;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(LineRenderer), typeof(BoxCollider2D))]
    public class Rope : MonoBehaviour
    {
        [SerializeField] private float _colliderWidth = 1f;
        [SerializeField] private Vector3 _visualOffset = Vector3.forward;

        private LineRenderer _lineRenderer;
        private BoxCollider2D _boxCollider;

        public StickySphere AttachedSphere { get; private set; }
        public bool IsAttached => AttachedSphere != null;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();

            _lineRenderer.enabled = false;
            _boxCollider.enabled = false;
        }
        
        private void Update()
        {
            if (IsAttached)
                UpdateRope();
        }

        public void Attach(StickySphere stickySphere)
        {
            AttachedSphere = stickySphere;
            _lineRenderer.enabled = true;
            _boxCollider.enabled = true;
        }

        public void Detach()
        {
            AttachedSphere = null;
            _lineRenderer.enabled = false;
            _boxCollider.enabled = false;
        }

        public Vector2 GetAttractionForce(Vector2 playerPosition, float attractionStrength)
        {
            if (!IsAttached) 
                return Vector2.zero;

            var direction = ((Vector2)AttachedSphere.transform.position - playerPosition).normalized;
            return direction * attractionStrength;
        }

        private void UpdateRope()
        {
            var startPosition = transform.position;
            var endPosition = AttachedSphere.transform.position;
            
            _lineRenderer.SetPosition(0, startPosition + _visualOffset);
            _lineRenderer.SetPosition(1, endPosition + _visualOffset);
            
            var ropeDirection = endPosition - startPosition;
            var ropeLength = ropeDirection.magnitude;
            
            transform.right = ropeDirection;

            _boxCollider.size = new Vector2(ropeLength, _colliderWidth);
            _boxCollider.offset = new Vector2(ropeLength / 2f, 0f);
        }
    }
}
