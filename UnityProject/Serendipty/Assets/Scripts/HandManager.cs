using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject[] handObject;

    private int selectedHandIndex = -1;

    static HandManager instance;
    public static HandManager Instance
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

    private void UpdateHand()
    {
        if (selectedHandIndex == -1)
        {
            for (int i = 0; i < handObject.Length; i++)
            {
                handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < handObject.Length; i++)
            {
                if (i == selectedHandIndex)
                {
                    handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 70f, 0f);
                    handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    handObject[i].transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                    handObject[i].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectHand(int handIndex)
    {
        if (handIndex < 0 || handIndex >= handObject.Length) return;

        if (selectedHandIndex == -1)
        {
            selectedHandIndex = handIndex;
        }
        else
        {
            if (selectedHandIndex == handIndex)
            {
                selectedHandIndex = -1;
            }
            else
            {
                selectedHandIndex = handIndex;
            }
        }

        UpdateHand();
    }
}
