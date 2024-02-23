using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCounter : MonoBehaviour
{
    public GameObject Background;
    public GameObject YCS;
    public GameObject WCS;
    public GameObject BCS;
    public float Spacing;
    public void UpdateCounter(string[] CircleTypes)
    {
        Background.transform.localScale = new Vector3(CircleTypes.Length * Spacing, 1f, 1f);
        foreach (Transform child in transform)
            if(child != Background.transform)
            GameObject.Destroy(child.gameObject);
        float size = Spacing * (CircleTypes.Length - 1);
        for(int i = 0; i < CircleTypes.Length; i++)
        {
            GameObject WhatToInstate;
            if (CircleTypes[i] == "YCS")
                WhatToInstate = YCS;
            else if (CircleTypes[i] == "BCS")
                WhatToInstate = BCS;
            else
                WhatToInstate = WCS;
            //Instantiate(WhatToInstate, new Vector3(transform.position.x - (size / 2) + (i * Spacing), transform.position.y, transform.position.z), Quaternion.identity, transform);
            Instantiate(WhatToInstate,transform.position + transform.right * (- (size / 2) + (i * Spacing)), Quaternion.identity, transform);
        }
    }

    public void HideCounter()
    {
        foreach (Transform child in transform)
            if (child != Background.transform)
                GameObject.Destroy(child.gameObject);
        Background.transform.localScale = new Vector3(0f, 1f, 1f);
    }
}
