using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBobMovement : MonoBehaviour
{
    [SerializeField] private Vector3 horBobOffsetSpeedPow;
    [SerializeField] private Vector3 verBobOffsetSpeedPow;
    void Update()
    {
        transform.localPosition = (Vector3.up * Mathf.Sin((Time.time + verBobOffsetSpeedPow.x) * verBobOffsetSpeedPow.y) * verBobOffsetSpeedPow.z) + (Vector3.right * Mathf.Sin((Time.time + horBobOffsetSpeedPow.x) * horBobOffsetSpeedPow.y) * horBobOffsetSpeedPow.z);
    }
}
