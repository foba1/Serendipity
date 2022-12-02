using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NatureCycle : Spell
{
    IEnumerator AnimationCoroutine(int pos)
    {
        Creature creature = FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>();

        if (creature != null)
        {
            int mana = creature.cost * 2;
            creature.Death();
            if (GameManager.Instance.IsMyTurn())
            {
                GameManager.Instance.curMana += mana;
                GameManager.Instance.photonView.RPC("UpdateMana", RpcTarget.AllBuffered, GameManager.Instance.curMana, GameManager.Instance.myArea);
            }
        }

        yield return new WaitForSecondsRealtime(0.750f);

        Destroy(gameObject);
    }

    public override void UseAbility(int pos)
    {
        StartCoroutine(AnimationCoroutine(pos));
    }
}
