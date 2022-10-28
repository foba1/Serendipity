using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Hand : MonoBehaviourPun
{
    public Card[] cardArray = new Card[5] { null, null, null, null, null };
    public int selectedCard;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (name == "Hand1") DrawCard();
    }

    public void DrawCard()
    {
        for (int i = 0; i < cardArray.Length; i++)
        {
            if (cardArray[i] == null)
            {
                if (name == "Hand1")
                {
                    int cardIndex = gameManager.deckList[0].Pop();
                }
            }
        }
    }

    public void UseCard(int index)
    {
        
    }

    public void SelectPosition(int pos)
    {

    }

    public void CancelUseCard(int index)
    {

    }
}
