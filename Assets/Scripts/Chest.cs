using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int moneyMax;
    public int moneyMin;
    private int money;
    public GameObject[] itemDropPrefabs;
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;
    public GameObject coin4;
    public GameObject coin5;
    public Transform spawnPoint;
    public MainManager mainManager;
    public int chestType; //I created this variable like 2-4 years ago and.. afaik it does ABSOLUTELY NOTHING!
    void Start()
    {
        money = Random.Range(moneyMin, moneyMax);
        StartCoroutine(WaitLoot());
    }

    public void InitializeChestParameters(MainManager manager, int type)
    {
        mainManager = manager;
        chestType = type;
    }

    void Update()
    {
        
    }

    public void DoMoney()
    {
        while (money > 0)
        {
            if (money > 1200)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin5, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -1200;
            }
            else if (money > 300)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -300;
            }
            else if (money > 50)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -50;
            }
            else if (money > 10)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 10; i++)
                    {
                        Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -10;
            }
            else
            {
                Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                money += -1;
            }
        }
    }

    public void Loot()
    {
        DoMoney();
        Instantiate(itemDropPrefabs[Random.Range(0, itemDropPrefabs.Length)], Vector3.zero, Quaternion.identity);
    }

    IEnumerator WaitLoot()
    {
        yield return new WaitForSeconds(1f);
        Loot();
    }
}
