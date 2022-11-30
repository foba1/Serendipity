using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaperCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.GrimReaper;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 2;
        additionalCost = 0;
    }
}
