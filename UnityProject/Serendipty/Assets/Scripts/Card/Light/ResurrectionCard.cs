using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Resurrection;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Light;
        cardType = StaticVariable.Spell;
        cost = 6;
        additionalCost = 0;
    }
}
