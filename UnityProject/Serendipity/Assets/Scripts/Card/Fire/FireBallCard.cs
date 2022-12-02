using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.FireBall;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Fire;
        cardType = StaticVariable.Spell;
        cost = 1;
        additionalCost = 0;
    }
}
