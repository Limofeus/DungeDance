using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    public int page;
    public int minPageLimit;
    public int maxPageLimit;
    public float lerpSpeed;
    public Transform mapContent;

    // Update is called once per frame
    void Update()
    {
        mapContent.localPosition = Vector3.Lerp(mapContent.localPosition ,Vector3.left * page * 18f, lerpSpeed * Time.deltaTime);
    }

    public void moveRight()
    {
        if(page < maxPageLimit)
        {
            page += 1;
        }
    }

    public void moveLeft()
    {
        if (page > minPageLimit)
        {
            page -= 1;
        }
    }
}
