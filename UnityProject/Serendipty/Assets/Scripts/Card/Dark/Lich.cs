using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lich : Creature
{
    private bool ableToRespawn = true;

    IEnumerator DeathCoroutine()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 148f / 255f, 148f / 255f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 80f / 255f, 80f / 255f, 1f);

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Death");

        yield return new WaitForSecondsRealtime(0.683f);

        if (ableToRespawn)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);

            ableToRespawn = false;
            Instantiate(curPosition);
            animator.SetTrigger("Respawn");
            Active();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GetDamagedCoroutine()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 148f / 255f, 148f / 255f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 80f / 255f, 80f / 255f, 1f);

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("GetDamaged");

        yield return new WaitForSecondsRealtime(0.517f);

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        transform.GetChild(2).GetChild(0).GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator AttackCoroutine(int pos)
    {
        if (curPosition / 6 == 0)
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(-15f, 0f, 0f);
        }
        else
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(15f, 0f, 0f);
        }

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Attack");

        yield return new WaitForSecondsRealtime(0.280f);

        FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(power);

        yield return new WaitForSecondsRealtime(0.570f);

        transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        isAttackFinished = true;
    }

    public override void Instantiate(int pos)
    {
        cardIndex = StaticVariable.Lich;
        cardClass = StaticVariable.Legendary;
        cardProperty = StaticVariable.Dark;
        cardType = StaticVariable.Creature;
        cost = 6;
        additionalCost = 0;

        power = 60;
        health = 20;
        ableToAct = true;
        curPosition = pos;
        isAttackFinished = false;
        UpdateInfoText();

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("Spawn", true);

        int area;
        if (pos / 6 == 0) area = 0;
        else area = 1;
        for (int i = 0; i < 6; i++)
        {
            if (FieldManager.Instance.fieldObject[area * 6 + i].transform.childCount > 0) continue;
            else
            {
                int index = GraveManager.Instance.UndeadPop(area);
                if (index != -1)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        GameManager.Instance.photonView.RPC("SpawnCreature", RpcTarget.AllBuffered, area * 6 + i, index);
                    }
                }
            }
        }

        if (ableToRespawn)
        {
            GameManager.Instance.InstantiateLive2D(cardIndex);
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
