using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public GameObject npc;
    public int npcPrefabId;
    private MainManager mainManager;
    public float beatSpeed;
    public void InitializeNPCParameters(MainManager mm, int prefabId)
    {
        npcPrefabId = prefabId;
        mainManager = mm;
        npc = Instantiate(npcPrefabs[prefabId], transform);
        beatSpeed = 1f / mainManager.TimeBetweenBeats;
        npc.GetComponent<Animator>().speed = beatSpeed;
        //Debug.Log(npc.GetComponent<Animator>().speed);
    }
}
