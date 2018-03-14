using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10f;
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private LayerMask layerMask;

    private CharacterController charController;
    private Vector3 currentLookTarget = Vector3.zero;
    private BoxCollider[] swordColliders;

    private Vector3 moveDirection;

    private Animator anim;

    void Start()
    {
        swordColliders = GetComponentsInChildren<BoxCollider>();
        anim = GetComponent<Animator>();
        moveDirection = Vector3.zero;
    }

    void Update()
    {
        charController = GetComponent<CharacterController>();

        if (charController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                //anim.SetTrigger("jump");
            }
        }
        //moveDirection = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
        transform.Rotate(0, Input.GetAxis("Horizontal") * 150 * Time.deltaTime, 0);

        //if (Input.GetKey(KeyCode.Space) && charController.isGrounded)
        //{
        //    moveDirection.y = jumpSpeed;
        //}

        moveDirection.y -=  20f * Time.deltaTime;

        charController.Move(moveDirection * Time.deltaTime);

        float speed = new Vector3(moveDirection.x, 0f, moveDirection.z).magnitude;
        if (speed >= 0.1f) anim.SetBool("isWalking", true);
        else anim.SetBool("isWalking", false);

        if (Input.GetMouseButtonDown(0)) anim.Play("DoubleChop");

        if (Input.GetMouseButtonDown(1)) anim.Play("SpinAttack");
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
        foreach (var _collider in swordColliders)
            _collider.enabled = true;
    }

    public void EndAttack()
    {
        foreach (var _collider in swordColliders)
            _collider.enabled = false;
    }
}
