using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FieldManager : MonoBehaviourPun
{
    public GameObject[] fieldObject;

    private int selectedFieldIndex = -1;
    private bool handSelectMode = false;
    private Color red = new Color(1f, 160f / 255f, 160f / 255f, 1f);
    private Color green = new Color(199f / 255f, 1f, 170f / 255f, 1f);

    static FieldManager instance;
    public static FieldManager Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void InstantiatePlayer()
    {
        GameObject redPlayer = Instantiate(Resources.Load<GameObject>("RedPlayer"), fieldObject[4].transform);
        GameObject bluePlayer = Instantiate(Resources.Load<GameObject>("BluePlayer"), fieldObject[10].transform);

        redPlayer.GetComponent<Creature>().Instantiate(4);
        bluePlayer.GetComponent<Creature>().Instantiate(10);
    }

    public void UseSpell(int pos, int cardIndex)
    {
        GameObject spellObject = Instantiate(Resources.Load<GameObject>("Spell/" + cardIndex.ToString()), fieldObject[pos].transform);
        if (pos > 5)
        {
            spellObject.transform.GetChild(0).localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        spellObject.GetComponent<Spell>().UseAbility(pos);
    }

    public void SpawnCreature(int pos, int cardIndex)
    {
        GameObject creatureObject = Instantiate(Resources.Load<GameObject>("Creature/" + cardIndex.ToString()), fieldObject[pos].transform);
        if (pos > 5)
        {
            creatureObject.transform.GetChild(0).localEulerAngles = creatureObject.transform.GetChild(0).localEulerAngles + new Vector3(0f, 180f, 0f);
        }
        creatureObject.GetComponent<Creature>().Instantiate(pos);

        GameObject spawnObject = Instantiate(Resources.Load<GameObject>("Creature/Spawn"), fieldObject[pos].transform);
        if (pos > 5)
        {
            spawnObject.transform.GetChild(0).localEulerAngles = spawnObject.transform.GetChild(0).localEulerAngles + new Vector3(0f, 180f, 0f);
        }
        spawnObject.GetComponent<Spell>().UseAbility(pos);
    }

    public void ActiveCreature(int turn)
    {
        for (int i = 0; i < fieldObject.Length; i++)
        {
            int area = turn % 2;
            if ((area * 6) / 6 == i / 6)
            {
                if (fieldObject[i].transform.childCount > 0)
                {
                    Creature creature = fieldObject[i].transform.GetChild(0).GetComponent<Creature>();
                    creature.Active();
                    if (creature.isPoisoned)
                    {
                        creature.GetDamaged(10);
                    }
                }
            }
        }
    }

    private bool ableToAttack(int pos)
    {
        if (pos % 6 < 3) return true;
        else if (fieldObject[pos - 3].transform.childCount > 0) return false;
        else return true;
    }

    public bool hasCreature(int area)
    {
        bool hasCreature = false;
        for (int i = 0; i < fieldObject.Length; i++)
        {
            if ((area * 6) / 6 != i / 6) continue;
            if (fieldObject[i].transform.childCount > 0)
            {
                if (!fieldObject[i].transform.GetChild(0).GetComponent<Creature>().isPlayer)
                {
                    hasCreature = true;
                    break;
                }
            }
        }
        return hasCreature;
    }

    public bool hasSpaceToSpawn(int area)
    {
        bool hasSpace = false;
        for (int i = 0; i < fieldObject.Length; i++)
        {
            if ((area * 6) / 6 != i / 6) continue;
            if (fieldObject[i].transform.childCount == 0)
            {
                hasSpace = true;
                break;
            }
        }
        return hasSpace;
    }

    public void UseHandCard()
    {
        handSelectMode = true;
        UpdateFieldColor();
    }

    public void ResetBeforeNextTurn()
    {
        HandManager.Instance.ResetHandSelection();
        if (handSelectMode)
        {
            handSelectMode = false;
            for (int i = 0; i < fieldObject.Length; i++)
            {
                fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            for (int i = 0; i < fieldObject.Length; i++)
            {
                fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            selectedFieldIndex = -1;
        }
    }

    private void UpdateFieldColor()
    {
        if (handSelectMode)
        {
            if (HandManager.Instance.usedCard.cardType == StaticVariable.Creature)
            {
                for (int i = 0; i < fieldObject.Length; i++)
                {
                    if ((GameManager.Instance.myArea * 6) / 6 == i / 6)
                    {
                        if (fieldObject[i].transform.childCount == 0)
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = green;
                        }
                        else
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                        }
                    }
                    else
                    {
                        fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
            else if (HandManager.Instance.usedCard.cardType == StaticVariable.Spell)
            {
                if (HandManager.Instance.usedCard.cardIndex == StaticVariable.Resurrection)
                {
                    for (int i = 0; i < fieldObject.Length; i++)
                    {
                        if ((GameManager.Instance.myArea * 6) / 6 == i / 6)
                        {
                            if (fieldObject[i].transform.childCount == 0)
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = green;
                            }
                            else
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                            }
                        }
                        else
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        }
                    }
                }
                else if (HandManager.Instance.usedCard.cardIndex == StaticVariable.NatureCycle)
                {
                    for (int i = 0; i < fieldObject.Length; i++)
                    {
                        if ((GameManager.Instance.myArea * 6) / 6 == i / 6)
                        {
                            if (fieldObject[i].transform.childCount == 0)
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                            }
                            else
                            {
                                if (fieldObject[i].transform.GetChild(0).GetComponent<Creature>().isPlayer)
                                {
                                    fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                                }
                                else
                                {
                                    fieldObject[i].GetComponent<SpriteRenderer>().color = green;
                                }
                            }
                        }
                        else
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < fieldObject.Length; i++)
                    {
                        if ((GameManager.Instance.myArea * 6) / 6 != i / 6)
                        {
                            if (fieldObject[i].transform.childCount > 0)
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                            }
                            else
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                            }
                        }
                        else
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        }
                    }
                }
            }
        }
        else
        {
            if (selectedFieldIndex == -1)
            {
                for (int i = 0; i < fieldObject.Length; i++)
                {
                    fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
            else
            {
                for (int i = 0; i < fieldObject.Length; i++)
                {
                    if (i / 6 != selectedFieldIndex / 6)
                    {
                        if (fieldObject[i].transform.childCount > 0)
                        {
                            if (ableToAttack(i))
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = red;
                            }
                            else
                            {
                                fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                            }
                        }
                        else
                        {
                            fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        }
                    }
                    else if (i == selectedFieldIndex)
                    {
                        fieldObject[i].GetComponent<SpriteRenderer>().color = green;
                    }
                    else
                    {
                        fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
        }
    }

    public void SelectField(int fieldIndex)
    {
        if (fieldIndex < 0 || fieldIndex >= fieldObject.Length) return;

        if (!GameManager.Instance.IsMyTurn()) return;

        if (handSelectMode)
        {
            if (HandManager.Instance.usedCard.cardType == StaticVariable.Creature)
            {
                if (fieldObject[fieldIndex].transform.childCount > 0) return;
                else
                {
                    if ((GameManager.Instance.myArea * 6) / 6 == fieldIndex / 6)
                    {
                        GameManager.Instance.curMana -= HandManager.Instance.usedCard.cost;
                        GameManager.Instance.photonView.RPC("UpdateMana", RpcTarget.AllBuffered, GameManager.Instance.curMana, GameManager.Instance.myArea);
                        GameManager.Instance.photonView.RPC("SpawnCreature", RpcTarget.AllBuffered, fieldIndex, HandManager.Instance.usedCard.cardIndex);
                        handSelectMode = false;
                        UpdateFieldColor();
                        Destroy(HandManager.Instance.usedCard.gameObject);
                    }
                }
            }
            else if (HandManager.Instance.usedCard.cardType == StaticVariable.Spell)
            {
                if (HandManager.Instance.usedCard.cardIndex == StaticVariable.Resurrection)
                {
                    if ((GameManager.Instance.myArea * 6) / 6 == fieldIndex / 6)
                    {
                        if (fieldObject[fieldIndex].transform.childCount == 0)
                        {
                            GameManager.Instance.curMana -= HandManager.Instance.usedCard.cost;
                            GameManager.Instance.photonView.RPC("UpdateMana", RpcTarget.AllBuffered, GameManager.Instance.curMana, GameManager.Instance.myArea);
                            GameManager.Instance.photonView.RPC("UseSpell", RpcTarget.AllBuffered, fieldIndex, HandManager.Instance.usedCard.cardIndex);
                            handSelectMode = false;
                            UpdateFieldColor();
                            Destroy(HandManager.Instance.usedCard.gameObject);
                        }
                    }
                }
                else if (HandManager.Instance.usedCard.cardIndex == StaticVariable.NatureCycle)
                {
                    if ((GameManager.Instance.myArea * 6) / 6 == fieldIndex / 6)
                    {
                        if (fieldObject[fieldIndex].transform.childCount > 0)
                        {
                            if (!fieldObject[fieldIndex].transform.GetChild(0).GetComponent<Creature>().isPlayer)
                            {
                                GameManager.Instance.curMana -= HandManager.Instance.usedCard.cost;
                                GameManager.Instance.photonView.RPC("UpdateMana", RpcTarget.AllBuffered, GameManager.Instance.curMana, GameManager.Instance.myArea);
                                GameManager.Instance.photonView.RPC("UseSpell", RpcTarget.AllBuffered, fieldIndex, HandManager.Instance.usedCard.cardIndex);
                                handSelectMode = false;
                                UpdateFieldColor();
                                Destroy(HandManager.Instance.usedCard.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    if ((GameManager.Instance.myArea * 6) / 6 != fieldIndex / 6)
                    {
                        if (fieldObject[fieldIndex].transform.childCount > 0)
                        {
                            GameManager.Instance.curMana -= HandManager.Instance.usedCard.cost;
                            GameManager.Instance.photonView.RPC("UpdateMana", RpcTarget.AllBuffered, GameManager.Instance.curMana, GameManager.Instance.myArea);
                            GameManager.Instance.photonView.RPC("UseSpell", RpcTarget.AllBuffered, fieldIndex, HandManager.Instance.usedCard.cardIndex);
                            handSelectMode = false;
                            UpdateFieldColor();
                            Destroy(HandManager.Instance.usedCard.gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            if (selectedFieldIndex == -1)
            {
                if (fieldObject[fieldIndex].transform.childCount > 0)
                {
                    if ((GameManager.Instance.myArea * 6) / 6 == fieldIndex / 6)
                    {
                        if (fieldObject[fieldIndex].transform.GetChild(0).GetComponent<Creature>().ableToAct)
                        {
                            selectedFieldIndex = fieldIndex;
                        }
                    }
                }
            }
            else
            {
                if (fieldObject[fieldIndex].transform.childCount > 0)
                {
                    if (selectedFieldIndex / 6 == fieldIndex / 6)
                    {
                        GameManager.Instance.photonView.RPC("Move", RpcTarget.AllBuffered, selectedFieldIndex, fieldIndex);
                    }
                    else
                    {
                        if (ableToAttack(fieldIndex))
                        {
                            GameManager.Instance.photonView.RPC("Attack", RpcTarget.AllBuffered, selectedFieldIndex, fieldIndex);
                        }
                    }
                }
                else
                {
                    if (selectedFieldIndex / 6 == fieldIndex / 6)
                    {
                        GameManager.Instance.photonView.RPC("Move", RpcTarget.AllBuffered, selectedFieldIndex, fieldIndex);
                    }
                }
                selectedFieldIndex = -1;
            }

            UpdateFieldColor();
        }
    }

    public void Move(int fieldIndex1, int fieldIndex2)
    {
        if (fieldObject[fieldIndex1].transform.childCount == 0) return;

        if (fieldObject[fieldIndex2].transform.childCount > 0)
        {
            Transform firstTransform = fieldObject[fieldIndex1].transform.GetChild(0);
            Transform secondTransform = fieldObject[fieldIndex2].transform.GetChild(0);

            firstTransform.SetParent(fieldObject[fieldIndex2].transform, false);
            firstTransform.localPosition = new Vector3(0f, 0f, 0f);
            firstTransform.GetComponent<Creature>().curPosition = fieldIndex2;

            secondTransform.SetParent(fieldObject[fieldIndex1].transform, false);
            secondTransform.localPosition = new Vector3(0f, 0f, 0f);
            secondTransform.GetComponent<Creature>().curPosition = fieldIndex1;
        }
        else
        {
            Transform transform = fieldObject[fieldIndex1].transform.GetChild(0);
            transform.SetParent(fieldObject[fieldIndex2].transform, false);
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.GetComponent<Creature>().curPosition = fieldIndex2;
        }
    }
}
