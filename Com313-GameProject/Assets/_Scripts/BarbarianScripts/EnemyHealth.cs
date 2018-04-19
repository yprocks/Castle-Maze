using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

        private Image _bossHealth;
        
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

            if (!gameObject.CompareTag("Boss")) return;
            if (!GameManager.Instance.BossSpawn) return;
            if (_bossHealth == null)
                _bossHealth = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<Image>();
            else
                SetHealthSilder();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!(_timer >= _timeSinceLastHit) || GameManager.Instance.GameOver || !IsAlive) return;
            if (!other.CompareTag("PlayerWeapon")) return;
            _blood.Play();
            TakeHit(10);
            _timer = 0;
        }

        public void TakeHit(int damage)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damage;
                _anim.Play("Hurt");
                _audioSource.PlayOneShot(_audioSource.clip);
            }
           
            if (_currentHealth > 0) return;
            IsAlive = false;
            KillEnemy();
            GameManager.Instance.KillEnemy(gameObject.tag);
            
            if(!gameObject.CompareTag("Boss")) return;
            GetComponent<BossAttack>().enabled = false;
        }
        
        private void SetHealthSilder()
        {
            var healthAmount = _bossHealth.fillAmount;
            var toHealth = (float) _currentHealth / _health;
            _bossHealth.fillAmount = Mathf.Lerp(healthAmount, toHealth, Time.deltaTime * 15f);
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