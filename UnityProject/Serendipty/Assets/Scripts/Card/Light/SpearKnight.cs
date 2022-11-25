using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpearKnight : Creature
{
    IEnumerator DeathCoroutine()
    {
        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Death");

        yield return new WaitForSecondsRealtime(0.767f);

        Destroy(gameObject);
    }

    IEnumerator GetDamagedCoroutine()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 148f / 255f, 148f / 255f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 80f / 255f, 80f / 255f, 1f);

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("GetDamaged");

        yield return new WaitForSecondsRealtime(0.600f);

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator AttackCoroutine(int pos)
    {
        if (curPosition / 6 == 0)
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(-22f, 0f, 0f);
        }
        else
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(22f, 0f, 0f);
        }

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Attack");

        yield return new WaitForSecondsRealtime(0.280f);

        FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(power);
        if (pos + 3 >= 0 && pos + 3 < 12)
        {
            if (FieldManager.Instance.fieldObject[pos + 3].transform.childCount > 0)
            {
                FieldManager.Instance.fieldObject[pos + 3].transform.GetChild(0).GetComponent<Creature>().GetDamaged(power);
            }
        }

        yield return new WaitForSecondsRealtime(0.403f);

        transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        isAttackFinished = true;
    }

    public override void Instantiate(int pos)
    {
        cardIndex = StaticVariable.SpearKnight;
        cardClass = StaticVariable.Normal;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 3;
        additionalCost = 0;

        power = 30;
        health = 70;
        ableToAct = true;
        curPosition = pos;
        isAttackFinished = false;
        UpdateInfoText();

        if (pos / 6 == 1)
        {
            transform.GetChild(0).localPosition = new Vector3(-0.3f, 0.55f, 0f);
        }
    }

    public override void Attack(int pos)
    {
        StartCoroutine(AttackCoroutine(pos));
    }

    public override void UseAbility(int index)
    {

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
