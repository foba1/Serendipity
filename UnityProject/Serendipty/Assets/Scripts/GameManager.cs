using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    public int turn;
    public int curMana;
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
    }

    private void Start()
    {

    }

    public void StartGame()
    {

    }

    public void Surren()
    {

    }

    public void FinishGame()
    {

    }
}
