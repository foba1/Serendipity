using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private List<int> deckList;

    static DeckManager instance;
    public static DeckManager Instance
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

    private void Start()
    {
        deckList = new List<int>();
        StaticVariable.MyDeck = "30000000000000000000";
        GenerateDeckFromDeckString();
        Draw();
    }

    private List<int> Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    private void GenerateDeckFromDeckString()
    {
        for (int i = 0; i < StaticVariable.MyDeck.Length; i++)
        {
            if (StaticVariable.MyDeck[i] - '0' > 0)
            {
                int count = StaticVariable.MyDeck[i] - '0';
                for (int j = 0; j < count; j++)
                {
                    deckList.Add(i);
                }
            }
        }

        deckList = Shuffle(deckList);
    }

    public void Draw()
    {
        int emptyCount = 0;
        for (int i = 0; i < HandManager.Instance.handObject.Length; i++)
        {
            if (HandManager.Instance.handObject[i].transform.childCount == 0)
            {
                emptyCount++;
            }
        }

        for (int i = 0; i < emptyCount; i++)
        {
            HandManager.Instance.InstantiateCard(deckList[0]);
            deckList.RemoveAt(0);
        }
    }
}
