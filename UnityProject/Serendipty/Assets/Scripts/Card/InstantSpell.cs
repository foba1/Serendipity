using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantSpell : Card
{
    public int spellType;
    public abstract void UseAbility();
}
