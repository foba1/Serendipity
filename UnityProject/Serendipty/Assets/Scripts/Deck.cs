using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<int> cardList;

    public Deck()
    {
        cardList = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            cardList.Add(StaticVariable.ExampleCreatureIndex);
        }
    }

    public void Shuffle()
    {

    }

    public int Pop()
    {
        int cardIndex = cardList[0];
        cardList.RemoveAt(0);
        return cardIndex;
    }
}
