using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Lich;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 7;
        additionalCost = 0;
    }
}
