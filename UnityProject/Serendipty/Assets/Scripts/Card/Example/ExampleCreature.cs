using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCreature : Creature
{
    private void Start()
    {
        cardIndex = StaticVariable.ExampleCreatureIndex;
        cardClass = StaticVariable.Normal;
        cardType = StaticVariable.Light;
        cost = 1;
        additionalCost = 0;
        health = 10;
        power = 10;
        ableToAct = false;
        curPosition = 0;
    }

    public override void Instantiate(int pos)
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(int pos)
    {
        throw new System.NotImplementedException();
    }

    public override void CounterAttack(int pos)
    {
        throw new System.NotImplementedException();
    }

    public override void GetDamaged(int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void UseAbility(int index)
    {
        throw new System.NotImplementedException();
    }
}
