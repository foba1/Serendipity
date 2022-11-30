using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkyrieCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Valkyrie;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Light;
        cardType = StaticVariable.Creature;
        cost = 6;
        additionalCost = 0;
    }
}
