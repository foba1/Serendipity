using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfSeaCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.KingOfSea;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Creature;
        cost = 5;
        additionalCost = 0;
    }
}
