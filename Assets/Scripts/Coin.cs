using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public float pickupRadius;
    public float minFloorHight;
    public float maxFloorHight;
    public float gravity;
    public float floorDrag;
    public float bounce;
    public float timeBeforeMagnet;
    public Vector3 velocity;
    private Transform characterTransform;
    private float floorHight;
    private float maxHieghtReached;
    private bool hasFloor;
    private bool magnetic;

    void Start()
    {
        characterTransform = MainManager.publicCharacter.transform;
        velocity = new Vector3(Random.Range(-5f, -1f), Random.Range(2f, 5f));
        floorHight = Random.Range(minFloorHight, maxFloorHight);
        StartCoroutine(Magnet());
    }

    void Update()
    {
        if(transform.position.x < characterTransform.position.x + pickupRadius)
        {
            MainManager.thisMainManager.AddMoney(value);
            Destroy(gameObject);
        }
        if (magnetic)
        {
            velocity += (characterTransform.position - transform.position).normalized * 50f * Time.deltaTime;
        }
        else
        {
            if (transform.position.y > maxHieghtReached)
            {
                maxHieghtReached = transform.position.y;
            }
            else
            {
                hasFloor = true;
            }
            velocity += Vector3.down * gravity * Time.deltaTime;
        }
        if (hasFloor && transform.position.y < floorHight)
        {
            velocity = new Vector3(velocity.x * floorDrag, velocity.y * -bounce, 0);
            transform.position = new Vector3(transform.position.x, floorHight, 0);
        }
        transform.Translate(velocity * Time.deltaTime);
    }

    IEnumerator Magnet()
    {
        yield return new WaitForSeconds(timeBeforeMagnet);
        floorHight = minFloorHight;
        magnetic = true;
    }
}
