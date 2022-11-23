using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Player : Creature
{
    private void UpdateInfoText()
    {
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = power.ToString();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = health.ToString();
    }

    IEnumerator DeathCoroutine()
    {
        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Death");

        yield return new WaitForSecondsRealtime(0.767f);
        Destroy(gameObject);
        if (GameManager.Instance.myArea == 0)
        {
            GameManager.Instance.photonView.RPC("FinishGame", RpcTarget.AllBuffered, 1);
        }
        else
        {
            GameManager.Instance.photonView.RPC("FinishGame", RpcTarget.AllBuffered, 0);
        }
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
        float xOffset = 15f;
        if (curPosition / 6 == 0)
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(-xOffset, 0f, 0f);
        }
        else
        {
            transform.position = FieldManager.Instance.fieldObject[pos].transform.position + new Vector3(xOffset, 0f, 0f);
        }

        Animator animator = transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Attack");

        yield return new WaitForSecondsRealtime(0.280f);

        FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(power);

        yield return new WaitForSecondsRealtime(0.403f);

        transform.position = FieldManager.Instance.fieldObject[curPosition].transform.position;
        isAttackFinished = true;
    }

    public override void Instantiate(int pos)
    {
        health = StaticVariable.PlayerMaxHealth;
        power = 10;
        ableToAct = true;
        curPosition = pos;
        isAttackFinished = false;
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
        health -= damage;
        if (health > 0)
        {
            StartCoroutine(GetDamagedCoroutine());
        }
        else
        {
            health = 0;
            Death();
        }
        UpdateInfoText();
        if (PhotonNetwork.IsMasterClient)
        {
            if (name == "RedPlayer(Clone)")
            {
                GameManager.Instance.photonView.RPC("UpdateHealth", RpcTarget.AllBuffered, health, 0);
            }
            else
            {
                GameManager.Instance.photonView.RPC("UpdateHealth", RpcTarget.AllBuffered, health, 1);
            }
        }
    }

    public override void Death()
    {
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
