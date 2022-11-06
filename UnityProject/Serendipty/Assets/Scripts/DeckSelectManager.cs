using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectManager : MonoBehaviour
{
    public GameObject deckSelectPanel;
    public GameObject deckObject;
    
    public void QuickMatching()
    {
        deckSelectPanel.SetActive(true);
        UpdateDeckSelectPanel();
    }

    public void SelectDeck(int index)
    {
        if (PlayerPrefs.HasKey("Deck" + (index + 1)))
        {
            StaticVariable.MyDeck = PlayerPrefs.GetString("Deck" + (index + 1));
        }
    }

    public void ExitDeckSelect()
    {
        deckSelectPanel.SetActive(false);
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
