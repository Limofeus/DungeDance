using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDict : MonoBehaviour
{
    public Sprite[] fishSpritesToAssign;
    public bool forceAssign;
    public static Sprite[] fishSprites;
    void Awake()
    {
        if (forceAssign)
        {
            fishSprites = fishSpritesToAssign;
        }
        else
        {
            if (fishSprites == null)
            {
                fishSprites = fishSpritesToAssign;
                //Debug.Log("Assigned");
            }
        }
    }
}
