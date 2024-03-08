using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public SpriteRenderer itemSprite;
    public Animator itemAnimator;
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public ParticleSystem particleSystemStart;
    public Color raretyColor;
    public int[] itemIds;
    public int itemId;
    private MainManager manager;
    private bool canPickup = true;
    private bool freeSpace;

    private void Start()
    {
        var particleSystemVar = particleSystemStart.main;
        manager = MainManager.thisMainManager;
        particleSystemVar.startColor = raretyColor;
        //particleSystem.main.startColor = raretyColor;
        light2D.color = raretyColor;
        itemId = itemIds[Random.Range(0, itemIds.Length)];
        //!!itemSprite.sprite = MainManager.itemSprites[itemId];
        itemSprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        manager.bottomTextHandler.DisplayItemText(itemId);
        freeSpace = manager.itemHolder.CheckForSpace();
        if (!freeSpace)
            manager.itemHolder.SpawnInventoryWarning();
        StartCoroutine(WaitForParticleSystem());
    }

    private void Update()
    {
        if ((Input.GetButtonDown("R") || (manager.mobileInput && Input.GetMouseButton(0))) && canPickup)
        {
            if(freeSpace)
            {
                Pickup();
                if (manager.mobileInput)
                    manager.PressThis("R");
            }
            else
            {
                if (manager.itemHolder.CheckForSpace())
                {
                    Pickup();
                }
            }
        }
        if(canPickup && manager.timeRemaining <= 0f)
        {
            if(!manager.itemHolder.CheckForSpace())
                manager.itemHolder.ClearInventoryWarning();
            particleSystemStart.Stop(); ;
            itemAnimator.SetTrigger("Ignore");
            canPickup = false;
            manager.ChangeUIType(false);
            StartCoroutine(WaitForDestroy());
        }
    }
    private void Pickup()
    {
        particleSystemStart.Stop();
        itemAnimator.SetTrigger("Pickup");
        manager.itemHolder.AddItem(itemId);
        canPickup = false;
        manager.ChangeUIType(false);
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForParticleSystem()
    {
        yield return new WaitForSeconds(0.70f);
        particleSystemStart.Play();
    }

    IEnumerator WaitForDestroy()
    {
        float timer = 0;
        while (timer <= 1f)
        {
            light2D.intensity = Mathf.Lerp(light2D.intensity, 0f, 3f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
