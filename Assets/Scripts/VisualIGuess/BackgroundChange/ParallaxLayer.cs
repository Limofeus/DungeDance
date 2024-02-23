using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
    [HideInInspector] public float currOffset;
    [SerializeField] private float distBetweenObjs;
    [SerializeField] private Vector2 spawnRandomness;
    [SerializeField] private float activeArea;
    [SerializeField] private GameObject[] spawnables;
    private List<GameObject> currentlySpawned = new List<GameObject>();
    private float nextSpawnPos;

    private void Start()
    {
        nextSpawnPos = transform.position.x - activeArea;
        //Fill BG with all the stuff
        //Nope, Ima do it at first frame of update
    }
    void Update()
    {
        while(activeArea - transform.position.x > nextSpawnPos)
        {
            GameObject currSpawnable = Instantiate(spawnables[Random.Range(0, spawnables.Length)], transform);
            currSpawnable.transform.position = new Vector3(nextSpawnPos + transform.position.x + Random.Range(-spawnRandomness.x, spawnRandomness.x), Random.Range(-spawnRandomness.y, spawnRandomness.y) + transform.position.y, 0f);
            currentlySpawned.Add(currSpawnable);
            nextSpawnPos += distBetweenObjs;
        }
        while(currentlySpawned.Count > 0 && currentlySpawned[0].transform.position.x < -activeArea)
        {
            GameObject thingToDestroy = currentlySpawned[0];
            currentlySpawned.RemoveAt(0);
            Destroy(thingToDestroy);
        }
    }
}
