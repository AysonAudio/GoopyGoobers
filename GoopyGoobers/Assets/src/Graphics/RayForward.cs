using UnityEngine;
namespace Graphics {

    /// <summary>
    /// Draws a 3D cylinder along an object's transform.forward.
    /// </summary>
    public class RayForward : MonoBehaviour {
        [SerializeField] GameObject cylinderPrefab;
        public float radius = 0.005f;
        public float clipDistance = 1000f;
        public float minLength = 0.4f;
        const float Offset = 0.5f;

        Quaternion _lastRotation;
        GameObject _cylinder;

        ////////////////////////////////////////
        // MonoBehaviour Events and Messages
        ////////////////////////////////////////
        void Awake() {
            _lastRotation = transform.rotation;
            UpdateVector();
        }

        void Update() {
            if (transform.rotation == _lastRotation) return;
            UpdateVector();
            _lastRotation = transform.rotation;
        }

        ////////////////////////////////////////
        // GameObject Management
        ////////////////////////////////////////
        void InitCylinder() {
            if (_cylinder) return;
            _cylinder = Instantiate(cylinderPrefab, transform);
            _cylinder.name = "RayForward";
            _cylinder.transform.localScale = new Vector3(radius * 2, radius * 2);
            _cylinder.transform.position = transform.position + Offset * transform.forward;
        }

        void UpdateVector() {
            InitCylinder();
            var isHit = Physics.Raycast(transform.position, transform.forward, out var hit);
            var isWithinClip = isHit && hit.distance < clipDistance;
            var distance = isWithinClip ? hit.distance : clipDistance;
            var scale = _cylinder.transform.localScale;
            scale.z = Mathf.Max(minLength, distance - Offset);
            _cylinder.transform.localScale = scale;
        }
    }
}
