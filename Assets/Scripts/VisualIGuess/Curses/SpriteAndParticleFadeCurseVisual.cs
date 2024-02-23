using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAndParticleFadeCurseVisual: CurseVisual
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private float lerpPow;
    private float targetAlpha = 1f;
    [SerializeField] private float waitBeforeDeletion;
    private void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, targetAlpha, lerpPow * Time.deltaTime));
    }
    public override void StartCurse()
    {
        Debug.Log("All fine");
    }
    public override void EndCurse()
    {
        targetAlpha = 0f;
        if(particleSys != null)
        {
            particleSys.enableEmission = false; //Plevat' chto ono ustarevshee
        }
        Debug.Log("StillFine");
        StartCoroutine(BeforeDeletion());
    }

    IEnumerator BeforeDeletion()
    {
        yield return new WaitForSeconds(waitBeforeDeletion);
        Destroy(gameObject);
    }
}
