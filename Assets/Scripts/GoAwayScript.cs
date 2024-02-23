using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAwayScript : MonoBehaviour
{
    public float TimToRemove;
    public Animator Animatoro;
    public void REEEOVEE(string HowIDid)
    {
        Animatoro.SetTrigger(HowIDid);
    }

    private void Update()
    {
            TimToRemove -= Time.deltaTime;
            if (TimToRemove <= 0f)
                Destroy(gameObject);

    }
}
