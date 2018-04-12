using System.Collections;
using UnityEngine;

namespace _Scripts.BarbarianScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] protected float _range = 3f;
        // ReSharper disable once InconsistentNaming
        [SerializeField] protected float _attackRate = 1f;

        // ReSharper disable once InconsistentNaming
        private Animator _anim;
        // ReSharper disable once InconsistentNaming
        private GameObject _player;
        // ReSharper disable once InconsistentNaming
        private bool _playerInRange;
        // ReSharper disable once InconsistentNaming
        private EnemyHealth _enemyHealth;
        // ReSharper disable once InconsistentNaming
        private BoxCollider[] _weaponColliders;

        protected void Start()
        {
            _weaponColliders = GetComponentsInChildren<BoxCollider>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _player = GameManager.Instance.Player;
            _anim = GetComponent<Animator>();
            StartCoroutine(Attack());
        }

        protected void Update()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < _range && _enemyHealth.IsAlive)
            {
                _playerInRange = true;
                RotateTowardsPlayer(_player.transform);
            }
            else

                _playerInRange = false;


            _anim.SetBool("playerInRange", _playerInRange);
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
        
        private void RotateTowardsPlayer(Transform player)
        {
            var direction = (player.position - transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
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

        public void BossFireTornadoAttack()
        {
            
        }

    }
}