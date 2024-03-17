using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private MainManager mainManager;
    //MOBILE STUFF
    [SerializeField] private bool setMobileInput;
    [SerializeField] private Arrow[] holdHitArrows = new Arrow[4]; // 0 - U, 1 - R, 2 - D, 3 - L (Ахуеть я шифруюсь, нет чтоб написать "сверху и по часовой")
    private int screenW;
    private int screenH;
    private int touchZone;
    static public bool mobileInput;

    private void Awake()
    {
        mobileInput = setMobileInput;
    }
    private void Start()
    {
        //Mobile st
        if (mobileInput)
        {
            screenH = Screen.height;
            screenW = Screen.width;
            touchZone = Mathf.RoundToInt(screenW / 3.75f);
        }
    }

    private void Update()
    {
        if (mobileInput)
            MobileYeeemput();
        else
            Yeeemput();
    }

    void Yeeemput()
    {
        if (!mainManager.disableMoving)
        {
            if (Input.GetButtonDown("R"))
            {
                holdHitArrows[1] = mainManager.PressThis("R", "Right");
            }
            if (Input.GetButtonDown("L"))
            {
                holdHitArrows[3] = mainManager.PressThis("L", "Left");
            }
            if (Input.GetButtonDown("U"))
            {
                holdHitArrows[0] = mainManager.PressThis("U", "Up");
            }
            if (Input.GetButtonDown("D"))
            {
                holdHitArrows[2] = mainManager.PressThis("D", "Down");
            }
        }

        // Buttons up
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


        if (!mainManager.disableItemUse)
        {
            if (Input.GetButtonDown("1"))
            {
                mainManager.ActivateItem(1);
            }
            if (Input.GetButtonDown("2"))
            {
                mainManager.ActivateItem(2);
            }
            if (Input.GetButtonDown("3"))
            {
                mainManager.ActivateItem(3);
            }
        }
    }
    private void ArrowHitStop(int dirId)
    {
        holdHitArrows[dirId]?.ArrowHitStop();
        holdHitArrows[dirId] = null;
    }
    private void MobileYeeemput()
    {
        Touch lastTouch;
        if (Input.touches.Length > 0)
        {
            lastTouch = Input.touches[Input.touches.Length - 1];
            if (lastTouch.phase == TouchPhase.Began)
            {
                if (lastTouch.position.y < touchZone)
                {
                    if (lastTouch.position.x < touchZone)
                    {
                        MobileYeeemput2(lastTouch.position.y / touchZone, lastTouch.position.x / touchZone);
                    }
                    else if (lastTouch.position.x > (screenW - touchZone))
                    {
                        MobileYeeemput2(lastTouch.position.y / touchZone, (lastTouch.position.x - (screenW - touchZone)) / touchZone);
                    }
                }
            }
        }
    }
    private void MobileYeeemput2(float touchX, float TouchY)
    {
        if (TouchY > (1f - touchX))
        {
            if (TouchY > touchX)
            {
                holdHitArrows[1] = mainManager.PressThis("R", "Right");
            }
            else
            {
                holdHitArrows[0] = mainManager.PressThis("U", "Up");
            }
        }
        else
        {
            if (TouchY > touchX)
            {
                holdHitArrows[2] = mainManager.PressThis("D", "Down");
            }
            else
            {
                holdHitArrows[3] = mainManager.PressThis("L", "Left");
            }
        }
    }
}
