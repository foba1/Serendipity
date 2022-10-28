using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public GameObject cardPage;
    public GameObject cardObject;

    private int curPage = 0;

    private void Start()
    {
        UpdateCardPage();
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void UpdateCardPage()
    {
        for (int i = 0; i < 8; i++)
        {
            Transform cardTransform = cardObject.transform.GetChild(i);
            cardTransform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Card/" + (curPage * 8 + i).ToString());
        }
    }
}
