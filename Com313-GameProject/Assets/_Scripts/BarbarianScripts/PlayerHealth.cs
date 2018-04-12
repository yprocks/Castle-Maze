using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _health = 100;
        [SerializeField] private float _timeSinceLastHit = 2f;
        public Image HealthIcon;
        public GameObject HitSpell;
        
        private float _timer;

        private CharacterController _charController;
        private PlayerController _playerController;
        private Animator _anim;
        private AudioSource _audioSource;
        private int _currentHealth;
        private ParticleSystem _blood;

        private void Start()
        {
            _anim = GetComponent<Animator>();
            _charController = GetComponent<CharacterController>();
            _playerController = GetComponent<PlayerController>();
            _audioSource = GetComponent<AudioSource>();
            _blood = GetComponentInChildren<ParticleSystem>();
            _currentHealth = _health;
            HealthIcon.fillAmount = (float) _currentHealth / _health;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            SetHealthSilder();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(_timer >= _timeSinceLastHit) || GameManager.Instance.GameOver) return;
            if (!other.CompareTag("Weapon") && !other.CompareTag("BossWeapon")) return;

            if (other.CompareTag("BossWeapon"))
                Instantiate(HitSpell, other.transform.position, Quaternion.identity);
            
            _blood.Play();
            TakeHit();
            _timer = 0;
        }

        private void TakeHit()
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= 10;
                GameManager.Instance.PlayerHit(_currentHealth);
                _anim.Play("Hurt");
                _audioSource.PlayOneShot(_audioSource.clip);
            }

            if (_currentHealth <= 0)
                KillPlayer();

        }

        private void KillPlayer()
        {
            GameManager.Instance.PlayerHit(_currentHealth);
            _anim.SetTrigger("heroDie");
            _charController.enabled = false;
            _playerController.enabled = false;
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        public void PowerUpHealth()
        {
            if (_currentHealth <= 70)
                _currentHealth += 30;
            else if(_currentHealth <= _health)
                _currentHealth = _health;
        }

        private void SetHealthSilder()
        {
            var healthAmount = HealthIcon.fillAmount;
            var toHealth = (float) _currentHealth / _health;
            HealthIcon.fillAmount = Mathf.Lerp(healthAmount, toHealth, Time.deltaTime * 15f);
        }
    }
}