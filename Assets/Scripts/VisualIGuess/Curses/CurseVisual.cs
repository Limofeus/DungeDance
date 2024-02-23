using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CurseVisual : MonoBehaviour //Base class for curse visual classes
{
    abstract public void StartCurse();
    abstract public void EndCurse(); //DO NOT FORGET TO SELF DESTRUCT HERE
}
