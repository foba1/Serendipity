using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBall : Spell
{
    IEnumerator AnimationCoroutine(int pos)
    {
        yield return new WaitForSecondsRealtime(0.3f);

        if (FieldManager.Instance.fieldObject[pos].transform.childCount > 0)
        {
            FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(30);
            for (int i = 0; i < FieldManager.Instance.fieldObject.Length; i++)
            {
                if (i / 6 != pos / 6)
                {
                    Transform transform = FieldManager.Instance.fieldObject[i].transform;
                    if (transform.childCount > 0)
                    {
                        if (transform.GetChild(0).GetComponent<Creature>().cardProperty == StaticVariable.Fire)
                        {
                            transform.GetChild(0).GetComponent<Creature>().Active();
                        }
                    }
                }
            } 
        }

        yield return new WaitForSecondsRealtime(0.683f);

        Destroy(gameObject);
    }

    public override void UseAbility(int pos)
    {
        StartCoroutine(AnimationCoroutine(pos));
    }
}
