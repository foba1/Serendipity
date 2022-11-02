using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelectManager : MonoBehaviour
{
    public GameObject deckSelectPanel;
    
    public void QuickMatching()
    {
        deckSelectPanel.SetActive(true);
    }
}
