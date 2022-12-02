using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public bool isAttackFinished = true;

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
        isAttackFinished = false;

        Creature firstCreature = FieldManager.Instance.fieldObject[fieldIndex1].transform.GetChild(0).GetComponent<Creature>();
        Creature secondCreature = FieldManager.Instance.fieldObject[fieldIndex2].transform.GetChild(0).GetComponent<Creature>();

        if (firstCreature != null && secondCreature != null)
        {
            if (firstCreature.power >= secondCreature.health)
            {
                firstCreature.Attack(fieldIndex2);

                while (!firstCreature.isAttackFinished) yield return null;
                firstCreature.isAttackFinished = false;

                firstCreature.Deactive();
            }
            else
            {
                firstCreature.Attack(fieldIndex2);

                while (!firstCreature.isAttackFinished) yield return null;
                firstCreature.isAttackFinished = false;

                yield return new WaitForSecondsRealtime(0.6f);

                if (secondCreature != null && secondCreature.health > 0)
                {
                    secondCreature.Attack(fieldIndex1);

                    while (!secondCreature.isAttackFinished) yield return null;
                    secondCreature.isAttackFinished = false;
                }

                if (firstCreature != null)
                {
                    firstCreature.Deactive();
                }
            }
        }

        isAttackFinished = true;
    }
}
