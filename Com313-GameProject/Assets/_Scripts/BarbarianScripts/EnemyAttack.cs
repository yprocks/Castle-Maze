using System.Collections;
using UnityEngine;

namespace _Scripts.BarbarianScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _range = 3f;
        [SerializeField] private float _attackRate = 1f;

        private Animator _anim;
        private GameObject _player;
        private bool _playerInRange;
        private EnemyHealth _enemyHealth;
        private BoxCollider[] _weaponColliders;

        private void Start()
        {
            _weaponColliders = GetComponentsInChildren<BoxCollider>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _player = GameManager.Instance.Player;
            _anim = GetComponent<Animator>();
            StartCoroutine(Attack());
        }

        public void Update()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < _range && _enemyHealth.IsAlive)
                _playerInRange = true;
            else
                _playerInRange = false;
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator Attack()
        {
            if (_playerInRange && !GameManager.Instance.GameOver)
            {
                _anim.Play("Attack");
                yield return new WaitForSeconds(_attackRate);
            }

            yield return null;
            StartCoroutine(Attack());
        }

        public void EnemyBeginAttack()
        {
            foreach (var weaponcollider in _weaponColliders)
                weaponcollider.enabled = true;
        }

        public void EnemyEndAttack()
        {
            foreach (var weaponCollider in _weaponColliders)
                weaponCollider.enabled = false;
        }
    }
}