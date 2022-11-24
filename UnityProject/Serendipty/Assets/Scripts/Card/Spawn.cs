using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn : Spell
{
    IEnumerator AnimationCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.683f);

        Destroy(gameObject);
    }

    public override void UseAbility(int pos)
    {
        StartCoroutine(AnimationCoroutine());
    }
}
