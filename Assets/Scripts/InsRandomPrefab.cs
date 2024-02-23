using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsRandomPrefab : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float PrefabSpawnChance;

    // Update is called once per frame
    void Start()
    {
        if(PrefabSpawnChance >= Random.Range(0f, 1f))
            Instantiate(Prefabs[Random.Range(0, Prefabs.Length)],transform);
    }
}
