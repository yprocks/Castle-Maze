using UnityEngine;

namespace _Scripts.BarbarianScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _jumpSpeed = 15f;
        [SerializeField]
        private float _moveSpeed = 10f;
//        [SerializeField]
//        private LayerMask _layerMask;

        private CharacterController _charController;
//        private Vector3 _currentLookTarget;
        private BoxCollider[] _swordColliders;

        private Vector3 _moveDirection;

        private Animator _anim;

        private void Start()
        {
            _swordColliders = GetComponentsInChildren<BoxCollider>();
            _anim = GetComponent<Animator>();
            _moveDirection = Vector3.zero;
//            _currentLookTarget = Vector3.zero;
        }

        public void Update()
        {
            _charController = GetComponent<CharacterController>();

            if (_charController.isGrounded)
            {
                _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _moveDirection = transform.TransformDirection(_moveDirection);
                _moveDirection *= _moveSpeed;
                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = _jumpSpeed;
                    //anim.SetTrigger("jump");
                }
            }
            //moveDirection = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
            transform.Rotate(0, Input.GetAxis("Horizontal") * 150 * Time.deltaTime, 0);

            //if (Input.GetKey(KeyCode.Space) && charController.isGrounded)
            //{
            //    moveDirection.y = jumpSpeed;
            //}

            _moveDirection.y -=  20f * Time.deltaTime;

            _charController.Move(_moveDirection * Time.deltaTime);

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

        //private void FixedUpdate()
        //{
        //    //RaycastHit hit;
        //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
        //    //{
        //    //    currentLookTarget = hit.point;
        //    //    Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    //    Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        //    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
        //    //}

        //}

        public void BeginAttack()
        {
            foreach (var swordCollider in _swordColliders)
                swordCollider.enabled = true;
        }

        public void EndAttack()
        {
            foreach (var swordCollider in _swordColliders)
                swordCollider.enabled = false;
        }
    }
}
