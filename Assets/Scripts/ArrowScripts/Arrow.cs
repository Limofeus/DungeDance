using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public ArrowVisual arrowVisual;
    public MainManager mainManager;
    public string[] directions;
    //string[] directions = { "R", "L" };
    public string direction;
    public float arrowSpeed;
    public bool disabled = false;
    public bool Auto;
    public bool sent;
    public bool lastArrow = false;
    public virtual void starto()
    {
        if(directions == null)
             directions = new string[] { "R", "L", "U", "D" };
        MainManager.Arrows.Add(gameObject);
        direction = directions[Random.Range(0, directions.Length)];
        switch (direction)
        {
            case "R":
                transform.Rotate(Vector3.forward * -90);
                break;
            case "L":
                transform.Rotate(Vector3.forward * 90);
                break;
            case "U":
                transform.Rotate(Vector3.forward * 0f);
                break;
            case "D":
                transform.Rotate(Vector3.forward * 180);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        UpdateStuff();
    }
    public virtual void UpdateStuff()
    {
        transform.localPosition = transform.localPosition + Vector3.left * arrowSpeed * Time.deltaTime;
        if (transform.localPosition.x < -3 && !disabled)
        {
            ArrowHit("POH");
            ArrowHitStop();
        }
        if (transform.localPosition.x < -15)
            Despawn();
        if (transform.localPosition.x < 1 && !sent)
        {
            if (mainManager.Monster != null)
            {
                mainManager.MonsterComp.ArrowThere(this); // If this wont work, ill try lower one
                //Manager.Monster.SendMessage("ArrowThere", this);
                sent = true;
            }
        }
        if (Auto && transform.localPosition.x < 0 && !disabled)
        {
            //Debug.Log("NOW! " + direction);
            mainManager.PressThis(direction);
        }
    }
    public void Despawn()
    {
        MainManager.Arrows.Remove(gameObject);
        Destroy(gameObject);
    }
    public virtual void ArrowHit(string Direction)
    {
        if (mainManager.Monster != null)
        {
            if (!disabled)
            {
                if (Direction == direction)
                {
                    //Debug.Log("Good, good");
                    float offset = Mathf.Abs(transform.localPosition.x);
                    mainManager.ArrowHit(offset, arrowSpeed, true, transform);
                    arrowVisual.Effect(offset / arrowSpeed, true);
                }
                else
                {
                    //Debug.Log("Всё хуйня, ты мимо нажал");
                    mainManager.ArrowHit(1f, arrowSpeed, false, transform);
                    arrowVisual.Effect(0f / arrowSpeed, false);
                }
            }
            disabled = true;
        }
    }
    public virtual void ArrowHitStop()
    {
        Debug.Log($"Arrow hit stop, dir: {direction}");
    }
    public void SwitchDir(string dir)
    {
        transform.localRotation = Quaternion.identity;
        direction = dir;
        //Debug.Log(direction);
        switch (direction)
        {
            case "R":
                transform.Rotate(Vector3.forward * -90);
                break;
            case "L":
                transform.Rotate(Vector3.forward * 90);
                break;
            case "U":
                transform.Rotate(Vector3.forward * 0f);
                break;
            case "D":
                transform.Rotate(Vector3.forward * 180);
                break;
            default:
                break;
        }
    }
}
