using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.BarbarianScripts
{
    public class EnemyHealth : MonoBehaviour
    {
        public bool IsAlive { get; private set; }

        [SerializeField] private int _health = 20;
        [SerializeField] private float _timeSinceLastHit = 0.5f;
        [SerializeField] private float _destroySpeed = 2f;

        private AudioSource _audioSource;
        private NavMeshAgent _navAgent;
        private Rigidbody _rigidbody;
        private Animator _anim;
        private CapsuleCollider _capsuleCollider;
        private int _currentHealth;
        private bool _destroyEnemy;
        private float _timer;
        private ParticleSystem _blood;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _navAgent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _blood = GetComponentInChildren<ParticleSystem>();
            IsAlive = true;
            _currentHealth = _health;
            _timer = 0f;
            _destroyEnemy = false;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_destroyEnemy)
            {
                transform.Translate(-Vector3.up * _destroySpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(_timer >= _timeSinceLastHit) || GameManager.Instance.GameOver || !IsAlive) return;
            if (!other.CompareTag("PlayerWeapon")) return;
            _blood.Play();
            TakeHit();
            _timer = 0;
        }

        private void TakeHit()
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= 10;
                _anim.Play("Hurt");
                _audioSource.PlayOneShot(_audioSource.clip);
            }

            if (_currentHealth > 0) return;
            IsAlive = false;
            ScoreManager.Instance.KillEnemy();
            KillEnemy();
        }

        private void KillEnemy()
        {
            _capsuleCollider.enabled = false;
            _anim.SetTrigger("enemyDie");
            _rigidbody.isKinematic = true;
            _navAgent.enabled = false;
            _audioSource.PlayOneShot(_audioSource.clip);
            StartCoroutine(RemoveEnemy());
        }

        private IEnumerator RemoveEnemy()
        {
            yield return new WaitForSeconds(4f);
            _destroyEnemy = true;
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}