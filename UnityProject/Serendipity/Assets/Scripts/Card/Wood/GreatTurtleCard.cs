using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatTurtleCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.GreatTurtle;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Wood;
        cardType = StaticVariable.Creature;
        cost = 8;
        additionalCost = 0;
    }
}
