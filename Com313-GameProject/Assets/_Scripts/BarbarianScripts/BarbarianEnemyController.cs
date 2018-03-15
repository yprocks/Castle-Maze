using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.BarbarianScripts
{
    public class BarbarianEnemyController : MonoBehaviour
    {
        private Transform _player;
        private NavMeshAgent _navAgent;
        private Animator _anim;
        private EnemyHealth _enemyHealth;

        private void Start()
        {
            _player = GameManager.Instance.Player.transform;
            _navAgent = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            if (_enemyHealth.IsAlive)
            {
                if (!GameManager.Instance.GameOver)
                    _navAgent.SetDestination(_player.position);
                else
                {
                    _navAgent.enabled = false;
                    _anim.Play("Idle");
                }
            }
            else
                _navAgent.enabled = false;
        
        }
    }
}
