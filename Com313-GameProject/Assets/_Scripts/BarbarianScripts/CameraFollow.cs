using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothing = 5f;

    private Vector3 offset;

    private void Awake()
    {
        Assert.IsNotNull(target);
    }

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetCameraPosition = target.position + offset;

        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, 0f);

        transform.position = Vector3.Lerp(transform.position, 
                                          targetCameraPosition,
                                          smoothing * Time.deltaTime);

        //transform.rotation = Quaternion.Slerp(transform.rotation, 
                                             //targetRotation,
                                             //smoothing * Time.deltaTime);
    }
}
