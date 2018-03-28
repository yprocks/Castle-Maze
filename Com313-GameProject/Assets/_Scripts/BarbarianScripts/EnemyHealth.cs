using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.BarbarianScripts
{
    public class EnemyHealth : MonoBehaviour
    {
        public bool IsAlive { get; private set; }

        // ReSharper disable once InconsistentNaming
        public GameObject UI_Points;
        // ReSharper disable once InconsistentNaming
        public Sprite UI_PointSprite;
        
        [SerializeField] private int _health = 20;
        [SerializeField] private float _timeSinceLastHit = 0.5f;
        [SerializeField] private float _destroySpeed = 2f;
        [SerializeField] private float _xPPoints = 10f;
        
        private AudioSource _audioSource;
        private NavMeshAgent _navAgent;
        private Rigidbody _rigidbody;
        private Animator _anim;
        private CapsuleCollider _capsuleCollider;
        private int _currentHealth;
        private bool _destroyEnemy;
        private float _timer;
        private ParticleSystem _blood;

        private SpriteRenderer _uiRenderer;
        
        private void Awake()
        {
            IsAlive = true;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _navAgent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _blood = GetComponentInChildren<ParticleSystem>();
            _currentHealth = _health;
            _timer = 0f;
            _destroyEnemy = false;

   
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_destroyEnemy)
                transform.Translate(-Vector3.up * _destroySpeed * Time.deltaTime);
            
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
//            ScoreManager.Instance.KillEnemy();
            KillEnemy();
        }

        private void KillEnemy()
        {
            var point = Instantiate(UI_Points, transform.position + Vector3.up, this.transform.rotation);
            _uiRenderer = point.GetComponent<SpriteRenderer>();
            _uiRenderer.sprite = UI_PointSprite;
            _uiRenderer.flipX = true;
            GameManager.Instance.AddXP(_xPPoints);
            point.transform.parent  = this.transform;
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