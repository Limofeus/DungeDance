using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldArrow : Arrow
{
    [SerializeField] private Transform tailTransform;
    [SerializeField] private ArrowHoldIndicator holdIndicator;
    [SerializeField] private float beatsToHold = 1f;

    private bool holdingArrow = false;
    override public void starto()
    {
        mainManager.arrowHandler.beatsToWait = Mathf.FloorToInt(beatsToHold);

        Debug.Log($"Float: {beatsToHold}, Floored int: {Mathf.FloorToInt(beatsToHold)}");

        holdIndicator.InitializeIndicator(arrowSpeed * mainManager.timeBetweenBeats * beatsToHold);

        if (directions == null)
            directions = new string[] { "R", "L", "U", "D" };
        MainManager.Arrows.Add(gameObject);
        direction = directions[Random.Range(0, directions.Length)];
        switch (direction)
        {
            case "R":
                transform.Rotate(Vector3.forward * -90);
                tailTransform.Rotate(Vector3.back * -90);
                break;
            case "L":
                transform.Rotate(Vector3.forward * 90);
                tailTransform.Rotate(Vector3.back * 90);
                break;
            case "U":
                transform.Rotate(Vector3.forward * 0f);
                tailTransform.Rotate(Vector3.back * 0f);
                break;
            case "D":
                transform.Rotate(Vector3.forward * 180);
                tailTransform.Rotate(Vector3.back * 180);
                break;
            default:
                break;
        }
    }

    public override void UpdateStuff()
    {
        if (holdingArrow)
        {
            holdIndicator.UpdateHoldProgress(-transform.localPosition.x / (arrowSpeed * mainManager.timeBetweenBeats * beatsToHold));
        }
        base.UpdateStuff();
    }

    public override void ArrowHit(string Direction)
    {
        base.ArrowHit(Direction);
        holdingArrow = true;
        holdIndicator.SetGradientStartColor(arrowVisual.SR.color);
    }

    public override void ArrowHitStop()
    {
        holdingArrow = false;
        TailEndHit();
        base.ArrowHitStop();
    }

    private void TailEndHit()
    {
        if (mainManager.Monster != null)
        {
            float offset = Mathf.Abs(transform.localPosition.x + (arrowSpeed * mainManager.timeBetweenBeats * beatsToHold));
            mainManager.ArrowHit(offset, arrowSpeed, true, transform.position + (Vector3.right * arrowSpeed * mainManager.timeBetweenBeats * beatsToHold), true);
            holdIndicator.CalculateEndColor(offset / arrowSpeed);
        }
    }
}
