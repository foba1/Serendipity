using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Card[] cardArray = new Card[5] { null, null, null, null, null };
    public int selectedCard;
    public Texture2D testTex;

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
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Sprite.Create(testTex, new Rect(0.0f, 0.0f, testTex.width, testTex.height), new Vector2(0.5f, 0.5f), 100.0f);
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
