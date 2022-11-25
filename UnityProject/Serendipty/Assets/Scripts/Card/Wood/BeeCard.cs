using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Bee;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Wood;
        cardType = StaticVariable.Creature;
        cost = 2;
        additionalCost = 0;
    }
}
