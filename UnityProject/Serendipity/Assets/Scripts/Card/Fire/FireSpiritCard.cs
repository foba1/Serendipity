using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.FireSpirit;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Fire;
        cardType = StaticVariable.Creature;
        cost = 3;
        additionalCost = 0;
    }
}
