using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMonsterAway : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void Initialaze(Sprite minimonSprite)
    {
        spriteRenderer.sprite = minimonSprite;
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
