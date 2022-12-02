using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Resurrection : Spell
{
    IEnumerator AnimationCoroutine(int pos)
    {
        yield return new WaitForSecondsRealtime(0.567f);

        int cardIndex;
        if (pos / 6 == 0)
        {
            cardIndex = GraveManager.Instance.Pop(0);
        }
        else
        {
            cardIndex = GraveManager.Instance.Pop(1);
        }
        if (cardIndex != -1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GameManager.Instance.photonView.RPC("SpawnCreature", RpcTarget.AllBuffered, pos, cardIndex);
            }
        }

        Destroy(gameObject);
    }

    public override void UseAbility(int pos)
    {
        StartCoroutine(AnimationCoroutine(pos));
    }
}
