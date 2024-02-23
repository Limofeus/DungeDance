using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAway : MonoBehaviour
{
    public bool npcGoAway;
    public bool spawnsObject;
    public GameObject objectToSpawn;
    public GameObject[] npcAwayPrefabs;
    private NPC npcComponent;

    private void Start()
    {
        if (npcGoAway)
        {
            npcComponent = gameObject.GetComponent<NPC>();
        }
    }
    public void GoAway()
    {
        if (npcGoAway)
        {
            GameObject npcAway = Instantiate(npcAwayPrefabs[npcComponent.npcPrefabId], transform.position, Quaternion.identity);
            npcAway.GetComponent<Animator>().speed = npcComponent.beatSpeed;
            //Debug.Log(npcAway.GetComponent<Animator>().speed);
        }
        else
        {
            if (spawnsObject)
            {
                Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}
