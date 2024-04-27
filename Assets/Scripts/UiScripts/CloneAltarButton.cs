using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering.LookDev;

public class CloneAltarButton : MonoBehaviour
{
    public enum InteractMode {Hidden, Locked, Normal}

    [SerializeField] private Transform scalingTransform;
    [SerializeField] private float lerpPower;

    [SerializeField] private Color unselectedColor;
    [SerializeField] private Color selectedColor;

    [SerializeField] private Color[] rarityColours;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private TextMeshPro costText;

    [SerializeField] private Vector2 unselectedSelectedScale;

    public InteractMode interactMode = InteractMode.Hidden;

    [SerializeField] private Color targetBgColor = Color.black;

    private bool mouseOver;

    private void Update()
    {
        if(interactMode == InteractMode.Hidden)
        {
            LerpScaleAndColor(Vector3.zero, new Color(1f, 1f, 1f, 0f));
        }
        else
        {
            if(mouseOver && interactMode == InteractMode.Normal)
            {
                LerpScaleAndColor(Vector3.one * unselectedSelectedScale.y, selectedColor);
            }
            else
            {
                LerpScaleAndColor(Vector3.one * unselectedSelectedScale.x, unselectedColor);
            }
        }
    }

    public void ChangeMode(InteractMode newMode)
    {
        interactMode = newMode;
    }
    public void UpdateCloneButton(int itemId, string cloneCost = "?")
    {
        sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.sprite = ItemSpriteDictionary.itemSprites[itemId];
        targetBgColor = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
        costText.text = cloneCost;
    }

    private void LerpScaleAndColor(Vector3 desiredScale, Color desiredColor)
    {
        scalingTransform.localScale = Vector3.Lerp(scalingTransform.localScale, desiredScale, 1f - Mathf.Exp(-lerpPower * Time.deltaTime));
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
