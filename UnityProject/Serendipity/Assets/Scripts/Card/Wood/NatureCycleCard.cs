using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureCycleCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.NatureCycle;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Wood;
        cardType = StaticVariable.Spell;
        cost = 4;
        additionalCost = 0;
    }
}
