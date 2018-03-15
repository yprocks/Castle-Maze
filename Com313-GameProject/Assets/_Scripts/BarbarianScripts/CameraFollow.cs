using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.BarbarianScripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _smoothing = 5f;

        private Vector3 _offset;

        private void Awake()
        {
//            Assert.IsNotNull(_target);
        }

        private void Start()
        {
            _offset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            var targetCameraPosition = _target.position + _offset;

//            var targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _target.rotation.eulerAngles.y, 0f);

            transform.position = Vector3.Lerp(transform.position, 
                targetCameraPosition,
                _smoothing * Time.deltaTime);

            //transform.rotation = Quaternion.Slerp(transform.rotation, 
            //targetRotation,
            //smoothing * Time.deltaTime);
        }
    }
}
