using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeepBodyMoove : MonoBehaviour //Bro, what a poggers name for a script
{
    [SerializeField] private Transform bmTarget;
    [SerializeField] private Vector3 vecLerpPowOffsetAndScaling;
    private float startingVerticalPos;

    private void Start()
    {
        startingVerticalPos = transform.position.y;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3((bmTarget.position.x + vecLerpPowOffsetAndScaling.y) * vecLerpPowOffsetAndScaling.z, startingVerticalPos, 0f), vecLerpPowOffsetAndScaling.x * Time.deltaTime);
    }
}
