using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.FireDragon;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Fire;
        cardType = StaticVariable.Creature;
        cost = 8;
        additionalCost = 0;
    }
}
