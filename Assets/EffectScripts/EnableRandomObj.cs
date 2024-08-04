using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomObj : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjs;
    [SerializeField] private bool setSeed;
    void Start()
    {
        if(setSeed)
            Random.InitState(System.DateTime.Now.Millisecond);
        gameObjs[Random.Range(0, gameObjs.Length)].SetActive(true); //For some reason this always gives me like the same 4-5 hats!?
    }
}
