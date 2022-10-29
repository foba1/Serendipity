using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public GameObject cardPage;
    public GameObject cardObject;
    public GameObject cardInfo;
    public Text goldText;

    private int curPage;
    private int maxPage;
    private int selectedCardIndex;
    private int gold;

    private void Start()
    {
        curPage = 0;
        maxPage = (StaticVariable.CardCount - 1) / 8 + 1;
        selectedCardIndex = -1;
        InitializeCollectionInfo();
        UpdateCardPage();
        UpdateGold();
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void SelectCard(int index)
    {
        selectedCardIndex = curPage * 8 + index;
        cardInfo.SetActive(true);
        cardInfo.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + selectedCardIndex.ToString());
    }

    public void ExitCardInfo()
    {
        cardInfo.SetActive(false);
        UpdateCardPage();
    }

    private void InitializeCollectionInfo()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < StaticVariable.CardCount; i++)
        {
            PlayerPrefs.SetInt("Card" + i.ToString(), 0);
        }
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.Save();
    }

    private void UpdateGold()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            gold = PlayerPrefs.GetInt("Gold");
            goldText.text = "Gold : " + gold.ToString();
        }
    }

    private void UpdateCardPage()
    {
        for (int i = 0; i < 8; i++)
        {
            Transform cardTransform = cardObject.transform.GetChild(i);
            int index = curPage * 8 + i;
            if (index < StaticVariable.CardCount)
            {
                cardTransform.gameObject.SetActive(true);
                cardTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + index.ToString());
                if (PlayerPrefs.HasKey("Card" + index.ToString()))
                {
                    int count = PlayerPrefs.GetInt("Card" + index.ToString());
                    cardTransform.GetChild(0).GetComponent<Text>().text = "x " + count.ToString();
                }
            }
            else
            {
                cardTransform.gameObject.SetActive(false);
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
