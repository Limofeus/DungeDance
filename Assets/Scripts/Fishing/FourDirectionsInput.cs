using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FourDirectionInputTarget
{
    public enum FourArrowDir { Up = 0, Right = 1, Down = 2, Left = 3}
    public void OnFourDirInput(FourArrowDir direction);
}
public class FourDirectionsInput : MonoBehaviour
{
    private FourDirectionInputTarget inputTarget;
    public bool disableInputs;
    private void Start()
    {
        inputTarget = GetComponent<FourDirectionInputTarget>();
    }
    void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        if (!disableInputs)
        {
            if (Input.GetButtonDown("R"))
            {
                inputTarget.OnFourDirInput(FourDirectionInputTarget.FourArrowDir.Right);
            }
            if (Input.GetButtonDown("L"))
            {
                inputTarget.OnFourDirInput(FourDirectionInputTarget.FourArrowDir.Left);
            }
            if (Input.GetButtonDown("U"))
            {
                inputTarget.OnFourDirInput(FourDirectionInputTarget.FourArrowDir.Up);
            }
            if (Input.GetButtonDown("D"))
            {
                inputTarget.OnFourDirInput(FourDirectionInputTarget.FourArrowDir.Down);
            }
        }

        // Buttons up (Not needed for now)
        /*
        if (Input.GetButtonUp("R"))
        {
            ArrowHitStop(1);
        }
        if (Input.GetButtonUp("L"))
        {
            ArrowHitStop(3);
        }
        if (Input.GetButtonUp("U"))
        {
            ArrowHitStop(0);
        }
        if (Input.GetButtonUp("D"))
        {
            ArrowHitStop(2);
        }
        */
    }
}
