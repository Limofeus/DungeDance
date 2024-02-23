using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFromOrigin : MonoBehaviour // I should have called this just "LerpMultitool" or smth. but EEHHHH ITLL WORK LIKE THIS FIIINEEE
{
    public Transform lerpTarget;
    public bool mainCameraTarget;
    public bool lerpPosition;
    public bool lerpRotation;
    public bool disableZ;
    public bool useDeltaTime;
    public bool useObjectsTransform;
    public float lerpStrenght;
    private void Start()
    {
        if (mainCameraTarget)
        {
            lerpTarget = Camera.main.transform;
        }
    }
    void Update()
    {
        if (lerpPosition)
            transform.position = Vector3.Lerp(useObjectsTransform ? transform.position : Vector3.zero, lerpTarget.position, useDeltaTime ? lerpStrenght*Time.deltaTime : lerpStrenght);
        if (disableZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        if (lerpRotation)
            transform.rotation = Quaternion.Lerp(useObjectsTransform ? transform.rotation : Quaternion.identity, lerpTarget.rotation, useDeltaTime ? lerpStrenght*Time.deltaTime : lerpStrenght);
    }
}
