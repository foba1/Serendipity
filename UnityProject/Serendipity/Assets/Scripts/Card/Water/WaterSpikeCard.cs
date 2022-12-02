using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpikeCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.WaterSpike;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Spell;
        cost = 3;
        additionalCost = 0;
    }
}
