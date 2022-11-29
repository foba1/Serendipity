using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

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

    public void ButtonDown(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition -= new Vector3(0f, 20f, 0f);
    }

    public void ButtonUp(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition += new Vector3(0f, 20f, 0f);
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
            if (deckCardInfoPanel.transform.childCount > 0)
            {
                for (int i = deckCardInfoPanel.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(deckCardInfoPanel.transform.GetChild(i).gameObject);
                }
            }
            GameObject cardInfo = Instantiate(Resources.Load<GameObject>("Collection/Info/" + selectedDeckCardIndex), deckCardInfoPanel.transform);
            cardInfo.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ExitDeckCardInfo());
            cardInfo.transform.GetChild(7).GetChild(1).GetComponent<Text>().text = "x " + GetCardCountFromDeck(deck, selectedDeckCardIndex);

            GameObject buyButton = cardInfo.transform.GetChild(9).gameObject;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "³Ö±â";
            buyButton.GetComponent<Button>().onClick.AddListener(() => AddCardToDeck());
            EventTrigger.Entry buyEntry1 = new EventTrigger.Entry();
            buyEntry1.eventID = EventTriggerType.PointerDown;
            buyEntry1.callback.AddListener(data => ButtonDown(buyButton));
            buyButton.GetComponent<EventTrigger>().triggers.Add(buyEntry1);
            EventTrigger.Entry buyEntry2 = new EventTrigger.Entry();
            buyEntry2.eventID = EventTriggerType.PointerUp;
            buyEntry2.callback.AddListener(data => ButtonUp(buyButton));
            buyButton.GetComponent<EventTrigger>().triggers.Add(buyEntry2);

            GameObject sellButton = cardInfo.transform.GetChild(10).gameObject;
            sellButton.transform.GetChild(0).GetComponent<Text>().text = "»©±â";
            sellButton.GetComponent<Button>().onClick.AddListener(() => SubCardFromDeck());
            EventTrigger.Entry sellEntry1 = new EventTrigger.Entry();
            sellEntry1.eventID = EventTriggerType.PointerDown;
            sellEntry1.callback.AddListener(data => ButtonDown(sellButton));
            sellButton.GetComponent<EventTrigger>().triggers.Add(sellEntry1);
            EventTrigger.Entry sellEntry2 = new EventTrigger.Entry();
            sellEntry2.eventID = EventTriggerType.PointerUp;
            sellEntry2.callback.AddListener(data => ButtonUp(sellButton));
            sellButton.GetComponent<EventTrigger>().triggers.Add(sellEntry2);
        }
    }

    private void UpdateDeckCardInfo()
    {
        if (PlayerPrefs.HasKey("Deck" + (selectedDeckIndex + 1)))
        {
            string deck = PlayerPrefs.GetString("Deck" + (selectedDeckIndex + 1));
            deckCardInfoPanel.transform.GetChild(0).GetChild(7).GetChild(1).GetComponent<Text>().text = "x " + GetCardCountFromDeck(deck, selectedDeckCardIndex);
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
                GameObject card = Instantiate(Resources.Load<GameObject>("Collection/Card/" + index), cardTransform);
                int temp = i;
                card.AddComponent<Button>();
                card.GetComponent<Button>().onClick.AddListener(() => SelectDeckCard(temp));
                card.GetComponent<Button>().transition = Selectable.Transition.None;
                GameObject text = Instantiate(Resources.Load<GameObject>("Collection/Card/Text"), cardTransform);
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
            deckInfoPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = deckName;
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
            if (cardInfoPanel.transform.childCount > 0)
            {
                for (int i = cardInfoPanel.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(cardInfoPanel.transform.GetChild(i).gameObject);
                }
            }
            GameObject cardInfo = Instantiate(Resources.Load<GameObject>("Collection/Info/" + selectedCardIndex), cardInfoPanel.transform);
            cardInfo.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => ExitCardInfo());
            cardInfo.transform.GetChild(7).GetChild(1).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);

            GameObject buyButton = cardInfo.transform.GetChild(9).gameObject;
            buyButton.GetComponent<Button>().onClick.AddListener(() => BuyCard());
            EventTrigger.Entry buyEntry1 = new EventTrigger.Entry();
            buyEntry1.eventID = EventTriggerType.PointerDown;
            buyEntry1.callback.AddListener(data => ButtonDown(buyButton));
            buyButton.GetComponent<EventTrigger>().triggers.Add(buyEntry1);
            EventTrigger.Entry buyEntry2 = new EventTrigger.Entry();
            buyEntry2.eventID = EventTriggerType.PointerUp;
            buyEntry2.callback.AddListener(data => ButtonUp(buyButton));
            buyButton.GetComponent<EventTrigger>().triggers.Add(buyEntry2);

            GameObject sellButton = cardInfo.transform.GetChild(10).gameObject;
            sellButton.GetComponent<Button>().onClick.AddListener(() => SellCard());
            EventTrigger.Entry sellEntry1 = new EventTrigger.Entry();
            sellEntry1.eventID = EventTriggerType.PointerDown;
            sellEntry1.callback.AddListener(data => ButtonDown(sellButton));
            sellButton.GetComponent<EventTrigger>().triggers.Add(sellEntry1);
            EventTrigger.Entry sellEntry2 = new EventTrigger.Entry();
            sellEntry2.eventID = EventTriggerType.PointerUp;
            sellEntry2.callback.AddListener(data => ButtonUp(sellButton));
            sellButton.GetComponent<EventTrigger>().triggers.Add(sellEntry2);
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
            cardInfoPanel.transform.GetChild(0).GetChild(7).GetChild(1).GetComponent<Text>().text = "x " + PlayerPrefs.GetInt("Card" + selectedCardIndex);
        }
    }

    private bool IsLegendary(int index)
    {
        return StaticVariable.LegendaryCardIndexArray.Contains(index);
    }

    private void InitializeInfo()
    {
        if (!PlayerPrefs.HasKey("Gold"))
        {
            PlayerPrefs.DeleteAll();
            for (int i = 0; i < StaticVariable.CardCount; i++)
            {
                PlayerPrefs.SetInt("Card" + i, 0);
            }
            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.SetInt("DeckCount", 0);
            PlayerPrefs.Save();
        }
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
            goldText.text = "x " + gold;
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
                GameObject card = Instantiate(Resources.Load<GameObject>("Collection/Card/" + index), cardTransform);
                int temp = i;
                card.AddComponent<Button>();
                card.GetComponent<Button>().onClick.AddListener(() => SelectCard(temp));
                card.GetComponent<Button>().transition = Selectable.Transition.None;
                GameObject text = Instantiate(Resources.Load<GameObject>("Collection/Card/Text"), cardTransform);
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
