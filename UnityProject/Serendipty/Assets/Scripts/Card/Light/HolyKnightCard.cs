using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyKnightCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.HolyKnight;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Light;
        cardType = StaticVariable.Creature;
        cost = 4;
        additionalCost = 0;
    }
}
