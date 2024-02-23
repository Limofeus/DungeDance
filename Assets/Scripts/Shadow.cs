using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Transform Objec;
    public Vector3 Dimensions;
    public float Multipup;
    public float Xofffff;

    private void Update()
    {
        if (Objec.localPosition.y > 0.3f)
            transform.localScale = Dimensions * Multipup * (1 / (Objec.localPosition.y + 0.6f));
        else
            transform.localScale = Dimensions * Multipup * (1 / (0.3f + 0.6f));
        transform.position = new Vector3(Objec.position.x + Xofffff, transform.position.y, transform.position.z);
    }
}
