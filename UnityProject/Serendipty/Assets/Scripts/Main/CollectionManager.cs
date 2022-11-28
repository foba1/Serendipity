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
    public GameObject cardInfoPanel;
    public GameObject deckObject;
    public GameObject deckInfoPanel;
    public GameObject deckCardObject;
    public GameObject deckCardInfoPanel;

    private int curCardPage;
    private int maxCardPage;
    private int selectedCardIndex;
    private int curDeckCardPage;
    private int maxDeckCardPage;
    private int selectedDeckCardIndex;
    private int selectedDeckIndex;
    private int gold;
    private int deckCount;

    private void Start()
    {
        curCardPage = 0;
        curDeckCardPage = 0;
        maxCardPage = (StaticVariable.CardCount - 1) / 8 + 1;
        maxDeckCardPage = (StaticVariable.CardCount - 1) / 8 + 1;
        selectedCardIndex = -1;
        selectedDeckIndex = -1;
        selectedDeckCardIndex = -1;
        InitializeInfo();
        UpdateInfo();
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void AddCardToDeck()
    {
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            if (PlayerPrefs.HasKey("Card" + selectedDeckCardIndex))
            {
                string deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
                int deckCardCount = GetCardCountFromDeck(deck, selectedDeckCardIndex);
                int cardCount = PlayerPrefs.GetInt("Card" + selectedDeckCardIndex);
                if (GetCountFromDeck(deck) >= StaticVariable.MaxDeckCardCount) return;
                if (IsLegendary(selectedDeckCardIndex))
                {
                    if (cardCount - deckCardCount <= 0) return;
                    if (deckCardCount >= StaticVariable.MaxLegendaryCardCount) return;
                    deck = ReplaceAtChars(deck, selectedDeckCardIndex, (char)(deckCardCount + 1 + 48));
                    PlayerPrefs.SetString("Deck" + (selectedDeckIndex + 1), deck);
                    PlayerPrefs.Save();
                }
                else
                {
                    if (cardCount - deckCardCount <= 0) return;
                    if (deckCardCount >= StaticVariable.MaxNormalCardCount) return;
                    deck = ReplaceAtChars(deck, selectedDeckCardIndex, (char)(deckCardCount + 1 + 48));
                    PlayerPrefs.SetString("Deck" + (selectedDeckIndex + 1), deck);
                    PlayerPrefs.Save();
                }
                UpdateDeckCardPage();
                UpdateDeckCardInfo();
            }
        }
    }

    public void SubCardFromDeck()
    {
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            if (PlayerPrefs.HasKey("Card" + selectedDeckCardIndex))
            {
                string deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
                int deckCardCount = GetCardCountFromDeck(deck, selectedDeckCardIndex);
                if (deckCardCount <= 0) return;
                deck = ReplaceAtChars(deck, selectedDeckCardIndex, (char)(deckCardCount - 1 + 48));
                PlayerPrefs.SetString("Deck" + (selectedDeckIndex + 1), deck);
                PlayerPrefs.Save();
                UpdateDeckCardPage();
                UpdateDeckCardInfo();
            }
        }
    }

    private string ReplaceAtChars(string source, int index, char replacement)
    {
        var temp = source.ToCharArray();
        temp[index] = replacement;
        return new string(temp);
    }

    public void SelectDeckCard(int index)
    {
        selectedDeckCardIndex = curDeckCardPage * 8 + index;
        string deck = "";
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
        }
        if (PlayerPrefs.HasKey("Card" + selectedDeckCardIndex))
        {
            deckCardInfoPanel.SetActive(true);
            deckCardInfoPanel.transform.GetChild(2).GetComponent<Text>().text = "Card " + selectedDeckCardIndex;
            deckCardInfoPanel.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + selectedDeckCardIndex);
            deckCardInfoPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + GetCardCountFromDeck(deck, selectedDeckCardIndex);
            deckCardInfoPanel.transform.GetChild(4).GetComponent<Text>().text = "This is card " + selectedDeckCardIndex;
        }
    }

    private void UpdateDeckCardInfo()
    {
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            string deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
            deckCardInfoPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + GetCardCountFromDeck(deck, selectedDeckCardIndex);
        }
    }

    private void UpdateDeckCardPage()
    {
        int index;
        Transform cardTransform;
        string deck = "";
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
        }
        for (int i = 0; i < 8; i++)
        {
            cardTransform = deckCardObject.transform.GetChild(i);
            index = curDeckCardPage * 8 + i;
            if (index < StaticVariable.CardCount)
            {
                cardTransform.gameObject.SetActive(true);
                if (cardTransform.childCount > 0)
                {
                    for (int j = cardTransform.childCount - 1; j >= 0; j--)
                    {
                        Destroy(cardTransform.GetChild(j).gameObject);
                    }
                }
                GameObject card = Instantiate(Resources.Load<GameObject>("Collection/" + index), cardTransform);
                int temp = i;
                card.AddComponent<Button>();
                card.GetComponent<Button>().onClick.AddListener(() => SelectDeckCard(temp));
                card.GetComponent<Button>().transition = Selectable.Transition.None;
                GameObject text = Instantiate(Resources.Load<GameObject>("Collection/Text"), cardTransform);
                text.GetComponent<Text>().text = "x " + GetCardCountFromDeck(deck, index);
            }
            else
            {
                cardTransform.gameObject.SetActive(false);
            }
        }
    }

    public void NextDeckPage()
    {
        if (curDeckCardPage < maxDeckCardPage - 1)
        {
            curDeckCardPage++;
            UpdateDeckCardPage();
        }
    }

    public void PrevDeckPage()
    {
        if (curDeckCardPage > 0)
        {
            curDeckCardPage--;
            UpdateDeckCardPage();
        }
    }

    public void ExitDeckCardInfo()
    {
        selectedDeckCardIndex = -1;
        deckCardInfoPanel.SetActive(false);
    }

    private int GetCardCountFromDeck(string deck, int index)
    {
        if (index < 0 || index >= deck.Length) return -1;
        return deck[index] - '0';
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

    public void SelectDeck(int index)
    {
        selectedDeckIndex = index;
        curDeckCardPage = 0;
        if (PlayerPrefs.HasKey("Deck" + (index + 1)))
        {
            string deckName = PlayerPrefs.GetString("Deck" + (index + 1) + "Name");
            deckInfoPanel.SetActive(true);
            deckInfoPanel.transform.GetChild(1).GetComponent<Text>().text = deckName;
            UpdateDeckCardPage();
        }
    }

    public void ExitDeckInfo()
    {
        selectedDeckIndex = -1;
        deckInfoPanel.SetActive(false);
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
                deckObject.transform.GetChild(deckCount - 1).GetChild(0).GetComponent<Text>().text = "µ¦ " + deckCount;
            }
        }
    }

    public void DeleteDeck()
    {
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            if (selectedDeckIndex + 1 < deckCount)
            {
                string nextDeck;
                string nextDeckName;
                for (int i = selectedDeckIndex; i < deckCount - 1; i++)
                {
                    if (PlayerPrefs.HasKey("Deck" + (i + 2)))
                    {
                        nextDeck = PlayerPrefs.GetString("Deck" + (i + 2));
                        nextDeckName = PlayerPrefs.GetString("Deck" + (i + 2) + "Name");
                        PlayerPrefs.SetString("Deck" + (i + 1), nextDeck);
                        PlayerPrefs.SetString("Deck" + (i + 1) + "Name", nextDeckName);
                    }
                }
                PlayerPrefs.DeleteKey("Deck" + deckCount);
                PlayerPrefs.DeleteKey("Deck" + deckCount + "Name");
                PlayerPrefs.SetInt("DeckCount", deckCount - 1);
                PlayerPrefs.Save();
                deckCount--;
                selectedDeckIndex = -1;
                deckInfoPanel.SetActive(false);
                UpdateInfo();
            }
            else
            {
                PlayerPrefs.DeleteKey("Deck" + (selectedDeckIndex + 1));
                PlayerPrefs.DeleteKey("Deck" + (selectedDeckIndex + 1) + "Name");
                PlayerPrefs.SetInt("DeckCount", deckCount - 1);
                PlayerPrefs.Save();
                deckCount--;
                selectedDeckIndex = -1;
                deckInfoPanel.SetActive(false);
                UpdateInfo();
            }
        }
    }

    public void SelectCard(int index)
    {
        selectedCardIndex = curCardPage * 8 + index;
        if (PlayerPrefs.HasKey("Card" + selectedCardIndex))
        {
            cardInfoPanel.SetActive(true);
            cardInfoPanel.transform.GetChild(2).GetComponent<Text>().text = "Card " + selectedCardIndex;
            cardInfoPanel.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + selectedCardIndex);
            cardInfoPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);
            cardInfoPanel.transform.GetChild(4).GetComponent<Text>().text = "This is card " + selectedCardIndex;
        }
    }

    public void ExitCardInfo()
    {
        selectedCardIndex = -1;
        cardInfoPanel.SetActive(false);
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
            cardInfoPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);
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
            index = curCardPage * 8 + i;
            if (index < StaticVariable.CardCount)
            {
                cardTransform.gameObject.SetActive(true);
                if (cardTransform.childCount > 0)
                {
                    for (int j = cardTransform.childCount - 1; j >= 0; j--)
                    {
                        Destroy(cardTransform.GetChild(j).gameObject);
                    }
                }
                GameObject card = Instantiate(Resources.Load<GameObject>("Collection/" + index), cardTransform);
                int temp = i;
                card.AddComponent<Button>();
                card.GetComponent<Button>().onClick.AddListener(() => SelectCard(temp));
                card.GetComponent<Button>().transition = Selectable.Transition.None;
                GameObject text = Instantiate(Resources.Load<GameObject>("Collection/Text"), cardTransform);
                if (PlayerPrefs.HasKey("Card" + index))
                {
                    text.GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + index);
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

    public void NextCardPage()
    {
        if (curCardPage < maxCardPage - 1)
        {
            curCardPage++;
            UpdateCardPage();
        }
    }

    public void PrevCardPage()
    {
        if (curCardPage > 0)
        {
            curCardPage--;
            UpdateCardPage();
        }
    }
}
