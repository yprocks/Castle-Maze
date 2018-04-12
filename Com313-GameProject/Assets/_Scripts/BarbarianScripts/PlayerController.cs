using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _jumpSpeed = 15f;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private LayerMask _layerMask;

        private CharacterController _charController;
        private Vector3 _currentLookTarget;
        private BoxCollider[] _swordColliders;
        private bool _followCamareOn;
        private GameObject _fireTrail;
        private ParticleSystem _fireTrailParticleSystem;
        
        private Vector3 _moveDirection;

        private Animator _anim;

        private PlayerSpecialAttack _playerSpecialAttack;

        private void Start()
        {
            _fireTrail = GameObject.FindWithTag("Fire") as GameObject;
            _fireTrail.SetActive(false);
            _swordColliders = GetComponentsInChildren<BoxCollider>();
            _anim = GetComponent<Animator>();
            _moveDirection = Vector3.zero;
            _followCamareOn = GameManager.Instance.CurrentLevel == 2;
            _playerSpecialAttack = GetComponent<PlayerSpecialAttack>();
//            _currentLookTarget = Vector3.zero;
        }

        private void Update()
        {
            _charController = GetComponent<CharacterController>();

            if (_charController.isGrounded)
            {
                if (!_followCamareOn)
                {
                    _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    _moveDirection = transform.TransformDirection(_moveDirection);
                    _moveDirection *= _moveSpeed;
                    if (Input.GetButton("Jump"))
                        _moveDirection.y = _jumpSpeed;
                }
            }

            if (!_followCamareOn)
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * 150 * Time.deltaTime, 0);
                _moveDirection.y -= 20f * Time.deltaTime;
                _charController.Move(_moveDirection * Time.deltaTime);
            }
            else
            {
                _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _charController.SimpleMove(_moveDirection * _moveSpeed);
            }


            var speed = new Vector3(_moveDirection.x, 0f, _moveDirection.z).magnitude;
            _anim.SetBool("isWalking", speed >= 0.1f);

            if (Input.GetMouseButtonDown(0)) _anim.Play("DoubleChop");

            if (Input.GetMouseButtonDown(1)) _anim.Play("SpinAttack");
            
        }

        //private void RotateCharacter(Vector3 moveDirection)
        //{
        //    Quaternion rotation = Quaternion.LookRotation(moveDirection - transform.position);
        //    float step = 3 * Time.deltaTime;
        //    Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirection, step, 0.0F);
        //    transform.rotation = Quaternion.LookRotation(newDir);
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
        //}

        private void FixedUpdate()
        {
            if (!_followCamareOn) return;
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit, 500, _layerMask, QueryTriggerInteraction.Ignore)) return;
            if (hit.point != _currentLookTarget)
                _currentLookTarget = hit.point;
            var targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            var rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }

        public void BeginAttack()
        {
            foreach (var swordCollider in _swordColliders)
                swordCollider.enabled = true;
            
            if(_playerSpecialAttack.TornadoSpecialAttack)
                _playerSpecialAttack.FireTornadoSpecilAttak();
        }

        public void EndAttack()
        {
            foreach (var swordCollider in _swordColliders)
                swordCollider.enabled = false;
            
            if(_playerSpecialAttack.LigntningSpecialAttack)
                _playerSpecialAttack.LightningSpecialAttack();
           
        }

        public void SpeedPowerUp()
        {
            StartCoroutine(FireTrailRoutine());
        }

        private IEnumerator FireTrailRoutine()
        {
            _fireTrail.SetActive(true);
            _moveSpeed = 25f;
            yield return new WaitForSeconds(10f);
            _moveSpeed = 10f;
//            _fireTrailParticleSystem = _fireTrail.GetComponent<ParticleSystem>();
//            var em = _fireTrailParticleSystem.emission;
//            em.enabled = false;
//            yield return new WaitForSeconds(2f);
//            em.enabled = true;
            _fireTrail.SetActive(false);
        }
    }
}