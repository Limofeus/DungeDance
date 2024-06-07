using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticParallax : MonoBehaviour
{
    [SerializeField] private float parallaxPower = 1f;
    private Transform cameraTransform;
    private Vector3 startPos;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        startPos = transform.position;
    }
    void Update()
    {
        Vector3 moveVec = cameraTransform.position;
        moveVec.z = 0f;
        transform.position = startPos + (moveVec * parallaxPower);
    }
}
