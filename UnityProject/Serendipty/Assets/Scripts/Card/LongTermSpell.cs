using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongTermSpell : Card
{
    public int spellType;
    public int maxTurn;
    public bool isActive;
    public abstract void ActiveAbility();
    public abstract void DeactiveAbility();
}
