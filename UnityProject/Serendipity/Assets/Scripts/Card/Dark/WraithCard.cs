using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Wraith;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 2;
        additionalCost = 0;
    }
}
