using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingOfSea : Creature
{
    IEnumerator DeathCoroutine()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 148f / 255f, 148f / 255f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 80f / 255f, 80f / 255f, 1f);

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Death");

        yield return new WaitForSecondsRealtime(0.350f);

        Destroy(gameObject);
    }

    IEnumerator GetDamagedCoroutine()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 148f / 255f, 148f / 255f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 80f / 255f, 80f / 255f, 1f);

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("GetDamaged");

        yield return new WaitForSecondsRealtime(0.833f);

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator AttackCoroutine(int pos)
    {
        if (curPosition / 6 == 0)
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(-20f, 0f, 0f);
        }
        else
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(20f, 0f, 0f);
        }

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Attack");

        yield return new WaitForSecondsRealtime(0.280f);

        FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(power);

        yield return new WaitForSecondsRealtime(0.737f);

        transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        isAttackFinished = true;
    }

    public override void Instantiate(int pos)
    {
        cardIndex = StaticVariable.KingOfSea;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Water;
        cardType = StaticVariable.Creature;
        cost = 8;
        additionalCost = 0;

        power = 80;
        health = 200;
        ableToAct = true;
        curPosition = pos;
        isAttackFinished = false;
        UpdateInfoText();

        for (int i = 0; i < FieldManager.Instance.fieldObject.Length; i++)
        {
            if (FieldManager.Instance.fieldObject[i].transform.childCount > 0)
            {
                Creature creature = FieldManager.Instance.fieldObject[i].transform.GetChild(0).GetComponent<Creature>();
                if (creature != null)
                {
                    if (i / 6 == curPosition / 6 && i != pos)
                    {
                        if (creature.cardProperty == StaticVariable.Water)
                        {
                            creature.health += 50;
                            creature.power += 50;
                            creature.UpdateInfoText();
                        }
                    }
                    if (!creature.isPlayer)
                    {
                        int temp = creature.health;
                        creature.health = creature.power;
                        creature.power = temp;
                        creature.UpdateInfoText();
                    }
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (FieldManager.Instance.fieldObject[i * 6 + j].transform.childCount > 0)
                {
                    Creature creature = FieldManager.Instance.fieldObject[i * 6 + j].transform.GetChild(0).GetComponent<Creature>();
                    if (creature != null)
                    {
                        FieldManager.Instance.Move(i * 6 + j, i * 6 + j + 3);
                    }
                    else
                    {
                        creature = FieldManager.Instance.fieldObject[i * 6 + j + 3].transform.GetChild(0).GetComponent<Creature>();
                        if (creature != null)
                        {
                            FieldManager.Instance.Move(i * 6 + j + 3, i * 6 + j);
                        }
                    }
                }
            }
        }
    }

    public override void Attack(int pos)
    {
        StartCoroutine(AttackCoroutine(pos));
    }

    public override void GetDamaged(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            UpdateInfoText();
            StartCoroutine(GetDamagedCoroutine());
        }
        else
        {
            health = 0;
            UpdateInfoText();
            Death();
        }
    }

    public override void Death()
    {
        if (curPosition / 6 == 0)
        {
            GraveManager.Instance.Add(0, cardIndex);
        }
        else
        {
            GraveManager.Instance.Add(1, cardIndex);
        }
        StartCoroutine(DeathCoroutine());
    }

    public override void Active()
    {
        ableToAct = true;
        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("ableToAct", true);
    }

    public override void Deactive()
    {
        ableToAct = false;
        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("ableToAct", false);
    }
}
