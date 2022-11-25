using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGolemCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.WaterGolem;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Creature;
        cost = 5;
        additionalCost = 0;
    }
}
