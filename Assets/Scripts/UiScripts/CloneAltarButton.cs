using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering.LookDev;

public class CloneAltarButton : MonoBehaviour
{
    public enum InteractMode {Hidden, Locked, Normal}

    [SerializeField] private Transform scalingTransform;
    [SerializeField] private Transform priceHolderTransform;
    [SerializeField] private float lerpPower;

    [SerializeField] private Color unselectedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color lockedColor;

    [SerializeField] private Color[] rarityColours;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private TextMeshPro costText;

    [SerializeField] private Vector2 unselectedSelectedScale;

    public InteractMode interactMode = InteractMode.Hidden;

    [SerializeField] private Color targetBgColor = Color.black;

    private bool mouseOver;

    private void Start()
    {
        scalingTransform.localScale = Vector3.zero;
        priceHolderTransform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if(interactMode == InteractMode.Hidden)
        {
            LerpScaleAndColor(Vector3.zero, new Color(1f, 1f, 1f, 0f), 0f);
        }
        else
        {
            if(mouseOver && interactMode == InteractMode.Normal)
            {
                LerpScaleAndColor(Vector3.one * unselectedSelectedScale.y, selectedColor, 1f);
            }
            else
            {
                if(interactMode == InteractMode.Locked)
                {
                    LerpScaleAndColor(Vector3.one * unselectedSelectedScale.x, lockedColor, 1f);
                }
                else
                {
                    LerpScaleAndColor(Vector3.one * unselectedSelectedScale.x, unselectedColor, 1f);
                }
            }
        }
    }

    public void ChangeMode(InteractMode newMode)
    {
        if(newMode == InteractMode.Hidden)
        {

        }
        interactMode = newMode;
    }
    public void UpdateCloneButton(int itemId, string cloneCost = "?")
    {
        sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.sprite = ItemSpriteDictionary.itemSprites[itemId];
        targetBgColor = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
        costText.text = cloneCost;
    }

    private void LerpScaleAndColor(Vector3 desiredScale, Color desiredColor, float priceDesiredScale)
    {
        scalingTransform.localScale = Vector3.Lerp(scalingTransform.localScale, desiredScale, 1f - Mathf.Exp(-lerpPower * Time.deltaTime));
        priceHolderTransform.localScale = Vector3.Lerp(priceHolderTransform.localScale, priceDesiredScale * Vector3.one, 1f - Mathf.Exp(-lerpPower * Time.deltaTime));
        sprite.color = Color.Lerp(sprite.color, desiredColor, 1f - Mathf.Exp(-lerpPower * Time.deltaTime));
        background.color = Color.Lerp(background.color, desiredColor * targetBgColor, 1f - Mathf.Exp(-lerpPower * Time.deltaTime));
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
