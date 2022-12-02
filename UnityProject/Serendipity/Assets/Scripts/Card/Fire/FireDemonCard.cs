using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDemonCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.FireDemon;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Fire;
        cardType = StaticVariable.Creature;
        cost = 5;
        additionalCost = 0;
    }
}
