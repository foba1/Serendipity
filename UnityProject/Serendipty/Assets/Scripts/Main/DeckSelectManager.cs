using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectManager : MonoBehaviour
{
    public GameObject deckSelectPanel;
    public GameObject deckObject;

    public void ButtonDown(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition -= new Vector3(0f, 20f, 0f);
    }

    public void ButtonUp(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition += new Vector3(0f, 20f, 0f);
    }

    public void QuickMatching()
    {
        deckSelectPanel.SetActive(true);
        UpdateDeckSelectPanel();
    }

    public void SelectDeck(int index)
    {
        if (PlayerPrefs.HasKey("Deck" + (index + 1)))
        {
            string deck = PlayerPrefs.GetString("Deck" + (index + 1));
            if (GetCountFromDeck(deck) < StaticVariable.MinDeckCardCount || GetCountFromDeck(deck) > StaticVariable.MaxDeckCardCount)
            {
                Debug.Log("Cannot select deck.");
            }
            else
            {
                StaticVariable.MyDeck = deck;
            }
        }
    }

    public void ExitDeckSelect()
    {
        deckSelectPanel.SetActive(false);
    }

    private int GetCountFromDeck(string deck)
    {
        int result = 0;
        for (int i = 0; i < deck.Length; i++)
        {
            result += deck[i] - '0';
        }
        return result;
    }

    private void UpdateDeckSelectPanel()
    {
        if (PlayerPrefs.HasKey("DeckCount"))
        {
            int deckCount = PlayerPrefs.GetInt("DeckCount");
            for (int i = 0; i < StaticVariable.MaxDeckCount; i++)
            {
                if (i < deckCount)
                {
                    deckObject.transform.GetChild(i).gameObject.SetActive(true);
                    if (PlayerPrefs.HasKey("Deck" + (i + 1) + "Name"))
                    {
                        deckObject.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("Deck" + (i + 1) + "Name");
                    }
                }
                else
                {
                    deckObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
