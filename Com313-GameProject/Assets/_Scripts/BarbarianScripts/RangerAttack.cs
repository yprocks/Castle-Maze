using System.Collections;
using System.Timers;
using UnityEngine;

namespace _Scripts.BarbarianScripts
{
    public class RangerAttack : EnemyAttack
    {
//        [SerializeField] private float _range = 3f;
//        [SerializeField] private float _attackRate = 1f;

//        private Animator _anim;
//        private GameObject _player;
//        private bool _playerInRange;
//        private EnemyHealth _enemyHealth;

        [SerializeField] private Transform _fireLocation;
        
        private GameObject _arrow;
        
        private new void Start()
        {
            base.Start();
            _arrow = GameManager.Instance.Arrow;
           
//            _enemyHealth = GetComponent<EnemyHealth>();
//            _player = GameManager.Instance.Player;
//            _anim = GetComponent<Animator>();
//            StartCoroutine(Attack());
        }

        private new void Update()
        {
           base.Update();
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
//        private IEnumerator Attack()
//        {
//            if (_playerInRange && !GameManager.Instance.GameOver)
//            {
//                _anim.Play("Attack");
//                yield return new WaitForSeconds(_attackRate);
//            }
//
//            yield return null;
//            StartCoroutine(Attack());
//        }

       

//        public void EnemyBeginAttack()
//        {
//        }
//
//        public void EnemyEndAttack()
//        {
//        }

        public void FireArrow()
        {
           var spawnedArrow = Instantiate (_arrow) as GameObject;
            spawnedArrow.transform.position = _fireLocation.position;
            spawnedArrow.transform.rotation = transform.rotation;
            spawnedArrow.GetComponent<Rigidbody>().velocity = transform.forward * 45f;
        }
    }
}