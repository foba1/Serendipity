using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Player : Creature
{
    private void Start()
    {
        if (name == "Player1")
        {
            Instantiate(4);
        }
        else
        {
            Instantiate(10);
        }
    }

    private void UpdateInfoText()
    {
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = power.ToString();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = health.ToString();
    }

    IEnumerator AttackCoroutine(int pos)
    {
        if (curPosition / 6 == 0)
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(-15f, 0f, 0f);
            Animator animator = transform.GetChild(0).GetComponent<Animator>();
            animator.SetTrigger("Attack");

            yield return new WaitForSecondsRealtime(0.42f);

            transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        }
        else
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(15f, 0f, 0f);
            Animator animator = transform.GetChild(0).GetComponent<Animator>();
            animator.SetTrigger("Attack");

            yield return new WaitForSecondsRealtime(0.42f);

            transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        }
    }

    public override void Instantiate(int pos)
    {
        health = 600;
        power = 10;
        ableToAct = true;
        curPosition = pos;
        UpdateInfoText();
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

    }

    public override void CounterAttack(int pos)
    {

    }
}
