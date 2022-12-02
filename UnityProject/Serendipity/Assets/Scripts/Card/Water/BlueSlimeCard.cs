using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.BlueSlime;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Creature;
        cost = 2;
        additionalCost = 0;
    }
}
