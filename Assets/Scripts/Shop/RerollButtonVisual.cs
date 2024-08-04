using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollButtonVisual : MonoBehaviour
{
    [SerializeField] private GameObject buttonLocked;
    [SerializeField] private GameObject buttonUnlocked;

    public void SetButtonUnlockment(bool unlocked)
    {
        buttonLocked.SetActive(!unlocked);
        buttonUnlocked.SetActive(unlocked);
    }
}
