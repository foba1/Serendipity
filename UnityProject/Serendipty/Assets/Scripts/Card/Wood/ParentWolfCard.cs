using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentWolfCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.ParentWolf;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Wood;
        cardType = StaticVariable.Creature;
        cost = 6;
        additionalCost = 0;
    }
}
