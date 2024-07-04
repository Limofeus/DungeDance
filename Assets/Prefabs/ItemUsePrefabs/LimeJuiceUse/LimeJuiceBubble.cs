using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeJuiceBubble : MonoBehaviour
{
    public float moneyDropTime;
    public float deletionTime;
    [SerializeField] private Vector2 upwardSpeedMinMax;
    [SerializeField] private Vector2Int moneyDropAmmount;
    [SerializeField] private Vector2 rotSpeed;
    [SerializeField] private Vector2 scaleMinMax;
    [SerializeField] private Rotate rotateEffect;

    private float upwardSpeed;

    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;
    public GameObject coin4;
    public GameObject coin5;

    void Start()
    {
        transform.localScale = Random.Range(scaleMinMax.x, scaleMinMax.y) * Vector3.one;
        rotateEffect.rotateSpeed = Random.Range(rotSpeed.x, rotSpeed.y);
        upwardSpeed = Random.Range(upwardSpeedMinMax.x, upwardSpeedMinMax.y);
        StartCoroutine(BubbleCoroutine());
    }
    private void Update()
    {
        transform.position += Vector3.up * upwardSpeed * Time.deltaTime;
    }

    public void DropMoney(int money)
    {
        Transform spawnPoint = this.transform;
        while (money > 0)
        {
            if (money > 1200)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin5, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                else
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                    }
                money += -1200;
            }
            else if (money > 300)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                else
                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                    }
                money += -300;
            }
            else if (money > 50)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                else
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                    }
                money += -50;
            }
            else if (money > 10)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                else
                    for (int i = 0; i < 10; i++)
                    {
                        Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                    }
                money += -10;
            }
            else
            {
                Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), Quaternion.identity);
                money += -1;
            }
        }
    }
    private IEnumerator BubbleCoroutine()
    {
        yield return new WaitForSeconds(moneyDropTime);
        DropMoney(Random.Range(moneyDropAmmount.x, moneyDropAmmount.y));
        yield return new WaitForSeconds(deletionTime - moneyDropTime);
        Destroy(gameObject);

    }
}
