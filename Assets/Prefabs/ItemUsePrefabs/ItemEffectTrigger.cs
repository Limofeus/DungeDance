using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectTrigger : MonoBehaviour
{
    [SerializeField] private bool activateOnStart;
    [SerializeField] private Animator animator;
    [SerializeField] private string animTriggerName;
    [SerializeField] protected float nextTriggerDelay;
    [SerializeField] protected ItemEffectTrigger nextTrigger;
    void Start()
    {
        Debug.Log($"IET: {animTriggerName}");
        if (activateOnStart)
            OnTrigger();
    }

    virtual public void OnTrigger()
    {
        Debug.Log($"IET-T: {animTriggerName}");
        TriggerAnim();
        StartCoroutine(FireNextTrigger());
    }

    protected void TriggerAnim()
    {
        Debug.Log($"IET-AT: {animTriggerName}");
        if (animator != null)
        {
            animator.SetTrigger(animTriggerName);
        }
    }
    private IEnumerator FireNextTrigger()
    {
        yield return new WaitForSeconds(nextTriggerDelay);
        nextTrigger?.OnTrigger();
    }
}
