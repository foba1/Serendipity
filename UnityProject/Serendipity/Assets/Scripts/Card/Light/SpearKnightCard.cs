using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearKnightCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.SpearKnight;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Light;
        cardType = StaticVariable.Creature;
        cost = 3;
        additionalCost = 0;
    }
}
