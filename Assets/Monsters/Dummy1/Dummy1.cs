using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy1 : Monster
{
    public override void ArrowThere(Arrow WhatArrow)
    {
        // ! Анимация приседания или чё нибудь такого !

        //base.ArrowThere(WhatArrow);
    }

    public override void End4Y(float Joy)
    {
        Instantiate(GoAway, transform.position, Quaternion.identity).GetComponent<GoAwayScript>().REEEOVEE("Away");
        //base.End4Y(Joy); // <- Я не знаю зачем я это закоментировал, будто бы я это когда нибудь буду использовать ххах
    }

    public override int GetRelationLevel(float Joy)
    {
        return 3;
    }
}
