using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCard : Card
{
    private void Start()
    {
        cardIndex = StaticVariable.Skeleton;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 1;
        additionalCost = 0;
    }
}
