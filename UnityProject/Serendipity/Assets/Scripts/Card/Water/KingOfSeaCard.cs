using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfSeaCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.KingOfSea;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Creature;
        cost = 8;
        additionalCost = 0;
    }
}
