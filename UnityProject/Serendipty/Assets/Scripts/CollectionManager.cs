using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class CollectionManager : MonoBehaviour
{
    public Text goldText;
    public GameObject cardPage;
    public GameObject cardObject;
    public GameObject cardInfo;
    public GameObject deckObject;
    public GameObject deckInfo;

    private int curPage;
    private int maxPage;
    private int selectedCardIndex;
    private int selectedDeckIndex;
    private int gold;
    private int deckCount;

    private void Start()
    {
        curPage = 0;
        maxPage = (StaticVariable.CardCount - 1) / 8 + 1;
        selectedCardIndex = -1;
        InitializeInfo();
        UpdateInfo();
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void SelectDeck(int index)
    {

    }

    public void ExitDeckInfo()
    {
        deckInfo.SetActive(false);
    }

    public void CreateDeck()
    {
        if (PlayerPrefs.HasKey("DeckCount"))
        {
            deckCount = PlayerPrefs.GetInt("DeckCount");
            if (deckCount < StaticVariable.MaxDeckCount)
            {
                deckCount++;
                PlayerPrefs.SetInt("DeckCount", deckCount);
                string deck = "";
                for (int i = 0; i < StaticVariable.CardCount; i++)
                {
                    deck += "0";
                }
                PlayerPrefs.SetString("Deck" + deckCount, deck);
                PlayerPrefs.SetString("Deck" + deckCount + "Name", "µ¦ " + deckCount);
                PlayerPrefs.Save();
                deckObject.transform.GetChild(deckCount - 1).gameObject.SetActive(true);
            }
        }
    }

    public void SelectCard(int index)
    {
        selectedCardIndex = curPage * 8 + index;
        if (PlayerPrefs.HasKey("Card" + selectedCardIndex))
        {
            cardInfo.SetActive(true);
            cardInfo.transform.GetChild(2).GetComponent<Text>().text = "Card " + selectedCardIndex;
            cardInfo.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + selectedCardIndex);
            cardInfo.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);
            cardInfo.transform.GetChild(4).GetComponent<Text>().text = "This is card " + selectedCardIndex;
        }
    }

    public void ExitCardInfo()
    {
        cardInfo.SetActive(false);
    }

    public void BuyCard()
    {
        if (PlayerPrefs.HasKey("Gold") && PlayerPrefs.HasKey("Card" + selectedCardIndex))
        {
            gold = PlayerPrefs.GetInt("Gold");
            int count = PlayerPrefs.GetInt("Card" + selectedCardIndex);
            if (IsLegendary(selectedCardIndex))
            {
                if (gold >= StaticVariable.LegendaryCardPrice)
                {
                    gold -= StaticVariable.LegendaryCardPrice;
                    PlayerPrefs.SetInt("Gold", gold);
                    PlayerPrefs.SetInt("Card" + selectedCardIndex, count + 1);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                if (gold >= StaticVariable.NormalCardPrice)
                {
                    gold -= StaticVariable.NormalCardPrice;
                    PlayerPrefs.SetInt("Gold", gold);
                    PlayerPrefs.SetInt("Card" + selectedCardIndex, count + 1);
                    PlayerPrefs.Save();
                }
            }
            UpdateCardInfo();
            UpdateGoldText();
            UpdateCardPage();
        }
    }

    public void SellCard()
    {
        if (PlayerPrefs.HasKey("Gold") && PlayerPrefs.HasKey("Card" + selectedCardIndex))
        {
            gold = PlayerPrefs.GetInt("Gold");
            int count = PlayerPrefs.GetInt("Card" + selectedCardIndex);
            if (IsLegendary(selectedCardIndex))
            {
                if (count > 0)
                {
                    gold += StaticVariable.LegendaryCardPrice / 4;
                    PlayerPrefs.SetInt("Gold", gold);
                    PlayerPrefs.SetInt("Card" + selectedCardIndex, count - 1);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                if (count > 0)
                {
                    gold += StaticVariable.NormalCardPrice / 4;
                    PlayerPrefs.SetInt("Gold", gold);
                    PlayerPrefs.SetInt("Card" + selectedCardIndex, count - 1);
                    PlayerPrefs.Save();
                }
            }
            UpdateCardInfo();
            UpdateGoldText();
            UpdateCardPage();
        }
    }

    private void UpdateCardInfo()
    {
        if (PlayerPrefs.HasKey("Card" + selectedCardIndex))
        {
            cardInfo.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);
        }
    }

    private bool IsLegendary(int index)
    {
        return StaticVariable.LegendaryCardIndexArray.Contains(index);
    }

    private void InitializeInfo()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < StaticVariable.CardCount; i++)
        {
            PlayerPrefs.SetInt("Card" + i, 0);
        }
        PlayerPrefs.SetInt("Gold", 1000);
        PlayerPrefs.SetInt("DeckCount", 0);
        PlayerPrefs.Save();
    }

    private void UpdateInfo()
    {
        UpdateGoldText();
        UpdateCardPage();
        UpdateDeckPage();
    }

    private void UpdateGoldText()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            gold = PlayerPrefs.GetInt("Gold");
            goldText.text = "Gold : " + gold;
        }
    }

    private void UpdateCardPage()
    {
        int index;
        Transform cardTransform;
        for (int i = 0; i < 8; i++)
        {
            cardTransform = cardObject.transform.GetChild(i);
            index = curPage * 8 + i;
            if (index < StaticVariable.CardCount)
            {
                cardTransform.gameObject.SetActive(true);
                cardTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + index);
                if (PlayerPrefs.HasKey("Card" + index))
                {
                    cardTransform.GetChild(0).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + index);
                }
            }
            else
            {
                cardTransform.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateDeckPage()
    {
        if (PlayerPrefs.HasKey("DeckCount"))
        {
            deckCount = PlayerPrefs.GetInt("DeckCount");
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

    public void NextPage()
    {
        if (curPage < maxPage - 1)
        {
            curPage++;
            UpdateCardPage();
        }
    }

    public void PrevPage()
    {
        if (curPage > 0)
        {
            curPage--;
            UpdateCardPage();
        }
    }
}
