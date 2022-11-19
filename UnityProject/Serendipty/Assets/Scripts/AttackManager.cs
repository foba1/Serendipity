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
        StartCoroutine(AttackCoroutine(fieldIndex1, fieldIndex2));
    }

    IEnumerator AttackCoroutine(int fieldIndex1, int fieldIndex2)
    {
        Creature firstCreature = FieldManager.Instance.fieldObject[fieldIndex1].transform.GetChild(0).GetComponent<Creature>();
        Creature secondCreature = FieldManager.Instance.fieldObject[fieldIndex2].transform.GetChild(0).GetComponent<Creature>();

        if (firstCreature != null && secondCreature != null)
        {
            firstCreature.Attack(fieldIndex2);

            while (!firstCreature.isAttackFinished) yield return null;
            firstCreature.isAttackFinished = false;

            yield return new WaitForSecondsRealtime(0.4f);
            secondCreature.Attack(fieldIndex1);

            while (!secondCreature.isAttackFinished) yield return null;
            secondCreature.isAttackFinished = false;
        }
        yield return null;
    }
}
