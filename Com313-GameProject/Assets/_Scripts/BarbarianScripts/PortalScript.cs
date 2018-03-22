using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Vuforia;

namespace _Scripts.BarbarianScripts
{
    public class PortalScript : MonoBehaviour
    {
//        private GameObject _cutScenesCamera;
//
//        private GameObject _player;
//        private GameObject[] _portals;
//
//        private bool _enteredPortal = false;
//        private GameObject _destPortal;
//        private Vector3 _targetPosition;
//
        private SphereCollider _collider;

        private void Start()
        {
            _collider = GetComponent<SphereCollider>();
            Invoke("EnableColliders", 1f);
        }

        private void EnableColliders()
        {
            _collider.enabled = true;
        }
        
//        private void Update()
//        {
//            if (_enteredPortal)
//                MoveCameraToTargetPortal();
//        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            GameManager.Instance.EnterPortal(this.gameObject, other.gameObject);
//            _portals = GameObject.FindGameObjectsWithTag("Portal");
//            _destPortal = this.gameObject;
//            while (_destPortal == this.gameObject)
//                _destPortal = _portals[Random.Range(0, _portals.Length)];
//            _targetPosition = _destPortal.transform.parent.position;
//            _cutScenesCamera.GetComponent<CameraFollow>().enabled = false;
//            _player = other.gameObject;
//            _player.SetActive(false);
//            StartCoroutine(EnterPortal(true));
        }

//        private IEnumerator EnterPortal(bool value)
//        {
//            yield return new WaitForSeconds(1f);
//            _enteredPortal = value;
//            if (!_enteredPortal)
//            {
//                _player.transform.position = _targetPosition + new Vector3(2, 0, 2);
//                _player.SetActive(true);
//                _cutScenesCamera.GetComponent<CameraFollow>().enabled = true;
//            }
//
//            yield return null;
//        }
//
//        private void MoveCameraToTargetPortal()
//        {
//            var position = Vector3.Lerp(_cutScenesCamera.transform.position,
//                _targetPosition,
//                Time.deltaTime * 2);
//            position.y = _cutScenesCamera.transform.position.y;
//            _cutScenesCamera.transform.position = position;
//
//            var point1 = new Vector2(_cutScenesCamera.transform.position.x, _cutScenesCamera.transform.position.z);
//            var point2 = new Vector2(_targetPosition.x, _targetPosition.z);
//
//            if (Vector2.Distance(point1, point2) > 3.0f)
//                return;
//
//            StartCoroutine(EnterPortal(false));
//        }
        
        
    }
}