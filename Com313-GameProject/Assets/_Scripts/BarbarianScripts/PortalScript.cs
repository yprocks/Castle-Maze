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
        public float DestroyTime = 6f;
        private SphereCollider _collider;

        private void Start()
        {
            _collider = GetComponent<SphereCollider>();
            Invoke("EnableColliders", 1f);
        }

        private void EnableColliders()
        {
            _collider.enabled = true;
            Invoke("DestroyScript", DestroyTime);
        }

        private void DestroyScript()
        {
            GameManager.Instance.PortalDestroyed();
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            GameManager.Instance.EnterPortal(this.gameObject, other.gameObject);
        }
    }
}