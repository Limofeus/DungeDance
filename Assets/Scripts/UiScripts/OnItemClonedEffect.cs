using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnItemClonedEffect : MonoBehaviour
{
    [SerializeField] private Transform animStartTransform;
    [SerializeField] private Transform animEndTransform;

    [SerializeField] private AnimationCurve vertCurve;
    [SerializeField] private AnimationCurve horizCurve;
    [SerializeField] private AnimationCurve scaleCurve;

    [SerializeField] private float animTime;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void InitAndAnim(int itemId, Transform asTrans, Transform aeTrans)
    {
        animStartTransform = asTrans;
        animEndTransform = aeTrans;
        spriteRenderer.sprite = ItemSpriteDictionary.itemSprites[itemId];
        StartCoroutine(AnimateEnum());
    }

    private IEnumerator AnimateEnum()
    {
        float animTimer = 0f;
        float animProgress = 0f;
        while (animTimer < animTime)
        {
            animProgress = animTimer / animTime;

            transform.position = new Vector3(
                Mathf.Lerp(animStartTransform.position.x, animEndTransform.position.x, horizCurve.Evaluate(animProgress)),
                Mathf.Lerp(animStartTransform.position.y, animEndTransform.position.y, vertCurve.Evaluate(animProgress)),
                0f
                );

            transform.localScale = Vector3.one * scaleCurve.Evaluate(animProgress);

            animTimer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
