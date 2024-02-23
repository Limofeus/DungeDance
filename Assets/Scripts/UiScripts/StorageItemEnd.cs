using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItemEnd : MonoBehaviour
{
    public SpriteRenderer spriteSR;
    public SpriteRenderer raritySR;
    public float destroyTime;
    public float lerpPower;
    private float desiredScale;
    private bool redAnim;

    public void UpdateItem(Sprite itemSprite, Color rarityColor, float scaleMultip)
    {
        UpdateItem(itemSprite, rarityColor, scaleMultip, false);
    }
    public void UpdateItem(Sprite itemSprite, Color rarityColor, float scaleMultip, bool redAnimT)
    {
        spriteSR.sprite = itemSprite;
        raritySR.sprite = itemSprite;
        raritySR.color = rarityColor;
        desiredScale = scaleMultip * 1.3f;
        transform.localScale = Vector3.one * scaleMultip;
        redAnim = redAnimT;
    }
    private void Update()
    {
        if (!redAnim)
        {
            spriteSR.color = Color.Lerp(spriteSR.color, new Color(1f, 1f, 1f, 0f), lerpPower * Time.deltaTime);
            raritySR.color = Color.Lerp(raritySR.color, new Color(raritySR.color.r, raritySR.color.g, raritySR.color.b, 0f), lerpPower * Time.deltaTime);
        }
        else
        {
            spriteSR.color = Color.Lerp(spriteSR.color, new Color(1f, 0f, 0f, 0f), lerpPower * Time.deltaTime);
            raritySR.color = Color.Lerp(raritySR.color, new Color(1f, 0f, 0f, 0f), lerpPower * Time.deltaTime);
        }
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale * Vector3.one, lerpPower * Time.deltaTime);
        if (destroyTime <= 0f)
        {
            Destroy(gameObject);
        }
        destroyTime -= Time.deltaTime;
    }
}
