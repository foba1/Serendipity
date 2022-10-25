using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turn;
    public int[] curMana = new int[2];
    public List<Field> fieldList;
    public List<Hand> handList;
    public List<Deck> deckList;
    public List<Grave> graveList;

    public static int[] mana = new int[] { 3, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

    static GameManager instance;
    public static GameManager Instance
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

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        deckList = new List<Deck>();
        deckList.Add(new Deck());
        deckList.Add(new Deck());
    }

    public void StartGame()
    {

    }

    public void Draw()
    {

    }

    public void Surren()
    {

    }

    public void FinishGame()
    {

    }
}
