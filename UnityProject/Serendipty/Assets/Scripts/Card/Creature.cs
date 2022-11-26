using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Creature : Card
{
    public int health;
    public int power;
    public bool ableToAct;
    public int curPosition;
    public bool isAttackFinished;
    public bool isPoisoned = false;
    public bool isPlayer = false;

    public void UpdateInfoText()
    {
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = power.ToString();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = health.ToString();
    }

    public abstract void Instantiate(int pos);
    public abstract void Attack(int pos);
    public abstract void UseAbility(int index);
    public abstract void GetDamaged(int damage);
    public abstract void Death();
    public abstract void Active();
    public abstract void Deactive();
}
