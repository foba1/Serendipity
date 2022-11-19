using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    static AttackManager instance;
    public static AttackManager Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    public void Attack(int fieldIndex1, int fieldIndex2)
    {
        Creature firstCreature = FieldManager.Instance.fieldObject[fieldIndex1].transform.GetChild(0).GetComponent<Creature>();
        Creature secondCreature = FieldManager.Instance.fieldObject[fieldIndex2].transform.GetChild(0).GetComponent<Creature>();

        if (firstCreature != null && secondCreature != null)
        {
            firstCreature.Attack(fieldIndex2);
            secondCreature.CounterAttack(fieldIndex1);
        }
    }
}
