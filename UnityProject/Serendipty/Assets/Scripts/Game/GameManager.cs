using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{
    [Header("Profile Object")]
    [SerializeField] GameObject[] playerProfile;

    public int turn = 0;
    public int myArea = 0;
    public int curMana = 0;
    public static int[] mana = new int[12] { 3, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

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
        if (PhotonNetwork.IsMasterClient)
        {
            myArea = 0;
        }
        else
        {
            myArea = 1;
        }
        StartGame();
    }

    [PunRPC]
    public void UpdateHealth(int health, int playerIndex)
    {
        if (health >= 0 && health <= StaticVariable.PlayerMaxHealth)
        {
            playerProfile[playerIndex].transform.GetChild(1).GetComponent<Slider>().value = health / (float)StaticVariable.PlayerMaxHealth;
        }
        else
        {
            playerProfile[playerIndex].transform.GetChild(1).GetComponent<Slider>().value = 1f;
        }
    }

    [PunRPC]
    public void UpdateMana(int mana, int playerIndex)
    {
        playerProfile[playerIndex].transform.GetChild(4).GetComponent<Text>().text = "x " + mana.ToString();
    }

    [PunRPC]
    public void SetTurnAndMana(int nextTurn)
    {
        turn = nextTurn;
        if (IsMyTurn())
        {
            if (mana.Length - 1 < turn)
            {
                curMana = mana[mana.Length - 1];
            }
            else
            {
                curMana = mana[turn];
            }
        }
        photonView.RPC("UpdateMana", RpcTarget.AllBuffered, curMana, myArea);
    }

    public bool IsMyTurn()
    {
        if (myArea % 2 == turn % 2) return true;
        else return false;
    }

    public void StartGame()
    {
        SetTurnAndMana(0);
        UpdateHealth(StaticVariable.PlayerMaxHealth, 0);
        UpdateHealth(StaticVariable.PlayerMaxHealth, 1);
        FieldManager.Instance.InstantiatePlayer();
        DeckManager.Instance.InitializeDeck();
    }

    public void Surren()
    {

    }

    public void FinishGame()
    {

    }
}
