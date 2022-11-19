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

    private void Start()
    {

    }

    public void SelectHand(int handIndex)
    {
        Debug.Log("Select " + handIndex.ToString());
    }
}
