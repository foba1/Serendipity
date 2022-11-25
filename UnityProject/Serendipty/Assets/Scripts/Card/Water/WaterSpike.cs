using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterSpike : Spell
{
    IEnumerator AnimationCoroutine(int pos)
    {
        yield return new WaitForSecondsRealtime(0.3f);

        if (FieldManager.Instance.fieldObject[pos].transform.childCount > 0)
        {
            Creature creature = FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>();
            if (creature.health > 60)
            {
                FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(60);
            }
            else
            {
                FieldManager.Instance.fieldObject[pos].transform.GetChild(0).GetComponent<Creature>().GetDamaged(60);
                for (int i = 0; i < FieldManager.Instance.fieldObject.Length; i++)
                {
                    if (i / 6 != pos / 6)
                    {
                        Transform transform = FieldManager.Instance.fieldObject[i].transform;
                        if (transform.childCount > 0)
                        {
                            creature = transform.GetChild(0).GetComponent<Creature>();
                            if (creature != null)
                            {
                                if (creature.cardProperty == StaticVariable.Water)
                                {
                                    creature.health += 10;
                                    creature.power += 10;
                                    creature.UpdateInfoText();
                                }
                            }
                        }
                    }
                }
            }
        }

        yield return new WaitForSecondsRealtime(0.517f);

        Destroy(gameObject);
    }

    public override void UseAbility(int pos)
    {
        StartCoroutine(AnimationCoroutine(pos));
    }
}
