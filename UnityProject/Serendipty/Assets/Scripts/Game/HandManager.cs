using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject[] handObject;
    public Card usedCard = null;

    private int selectedHandIndex = -1;

    static HandManager instance;
    public static HandManager Instance
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GameManager.Instance.IsMyTurn())
            {
                if (selectedHandIndex != -1)
                {
                    usedCard = handObject[selectedHandIndex].transform.GetChild(0).GetComponent<Card>();
                    if (GameManager.Instance.curMana >= usedCard.cost + usedCard.additionalCost)
                    {
                        if (usedCard.cardType == StaticVariable.Creature)
                        {
                            if (FieldManager.Instance.hasSpaceToSpawn(GameManager.Instance.myArea))
                            {
                                handObject[selectedHandIndex].transform.GetChild(0).gameObject.SetActive(false);
                                selectedHandIndex = -1;
                                UpdateHand();
                                FieldManager.Instance.UseHandCard();
                            }
                        }
                        else if (usedCard.cardType == StaticVariable.Spell)
                        {
                            if (usedCard.cardIndex == StaticVariable.Resurrection)
                            {
                                if (FieldManager.Instance.hasSpaceToSpawn(GameManager.Instance.myArea))
                                {
                                    handObject[selectedHandIndex].transform.GetChild(0).gameObject.SetActive(false);
                                    selectedHandIndex = -1;
                                    UpdateHand();
                                    FieldManager.Instance.UseHandCard();
                                }
                            }
                            else if (usedCard.cardIndex == StaticVariable.NatureCycle)
                            {
                                if (FieldManager.Instance.hasCreature(GameManager.Instance.myArea))
                                {
                                    handObject[selectedHandIndex].transform.GetChild(0).gameObject.SetActive(false);
                                    selectedHandIndex = -1;
                                    UpdateHand();
                                    FieldManager.Instance.UseHandCard();
                                }
                            }
                            else
                            {
                                handObject[selectedHandIndex].transform.GetChild(0).gameObject.SetActive(false);
                                selectedHandIndex = -1;
                                UpdateHand();
                                FieldManager.Instance.UseHandCard();
                            }
                        }
                    }
                    else
                    {
                        usedCard = null;
                    }
                }
            }
        }
    }

    public void DeleteSpellCard(int myArea)
    {
        if (GameManager.Instance.myArea == myArea) return;

        for (int i = 0; i < handObject.Length; i++)
        {
            if (handObject[i].transform.childCount > 0)
            {
                if (handObject[i].transform.GetChild(0).GetComponent<Card>().cardType == StaticVariable.Spell)
                {
                    Destroy(handObject[i].transform.GetChild(0).gameObject);
                }
            }
        }
    }

    public void InstantiateCard(int cardIndex)
    {
        int emptyHand = -1;
        for (int i = 0; i < handObject.Length; i++)
        {
            if (handObject[i].transform.childCount == 0)
            {
                emptyHand = i;
                break;
            }
        }
        if (emptyHand == -1) return;
        else
        {
            GameObject cardObject = Instantiate(Resources.Load<GameObject>("Card/" + cardIndex.ToString()), handObject[emptyHand].transform);
            if (emptyHand > 2)
            {
                cardObject.transform.GetChild(2).localPosition = new Vector3(-cardObject.transform.GetChild(2).localPosition.x, cardObject.transform.GetChild(2).localPosition.y, cardObject.transform.GetChild(2).localPosition.z);
            }
        }
    }

    private void UpdateHand()
    {
        if (selectedHandIndex == -1)
        {
            for (int i = 0; i < handObject.Length; i++)
            {
                if (handObject[i].transform.childCount > 0)
                {
                    handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                    handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < handObject.Length; i++)
            {
                if (i == selectedHandIndex)
                {
                    if (handObject[i].transform.childCount > 0)
                    {
                        handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 70f, 0f);
                        handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (handObject[i].transform.childCount > 0)
                    {
                        handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                        handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void SelectHand(int handIndex)
    {
        if (handIndex < 0 || handIndex >= handObject.Length) return;

        if (!GameManager.Instance.IsMyTurn()) return;

        if (selectedHandIndex == -1)
        {
            selectedHandIndex = handIndex;
        }
        else
        {
            if (selectedHandIndex == handIndex)
            {
                selectedHandIndex = -1;
            }
            else
            {
                selectedHandIndex = handIndex;
            }
        }

        UpdateHand();
    }
}
