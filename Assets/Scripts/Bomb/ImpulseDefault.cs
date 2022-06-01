using UnityEngine;
using Cinemachine;

namespace Bombsite
{
    public class ImpulseDefault : MonoBehaviour, IImpulse
    {
        [SerializeField]
        private float _intensity = 5.0f, _distance = 10.0f;

        private Vector3 _cameraPosition;

        private CinemachineImpulseSource _impulseSource;

        private void Awake() 
        {
            _cameraPosition = Camera.main.transform.position;
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        } 

        public void Invoke()
        {
            var position = transform.position;
            
            var difference = _cameraPosition - position;
            
            var direction = difference.normalized;
            var magnitude = 
                (difference.magnitude - _distance) * _distance;

            if (magnitude < 1.0f)
                magnitude = 1.0f;

            var velocity = direction * (_intensity / magnitude);

            _impulseSource?.GenerateImpulseAt(transform.position, velocity);
        }
    }
}
