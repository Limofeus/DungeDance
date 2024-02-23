using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterTime : MonoBehaviour
{
    public float removeTime;
    private float endTime;
    void Start()
    {
        endTime = Time.time + removeTime;
    }
    void Update()
    {
        if(Time.time > endTime)
        {
            Destroy(gameObject);
        }
    }
}
