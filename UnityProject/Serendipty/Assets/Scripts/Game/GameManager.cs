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

    public int turn;
    public int myArea;
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
        myArea = 0;
        curMana = mana[0];
        UpdateMana();
        UpdateHealth(600, 0);
        UpdateHealth(600, 1);
    }

    public void UpdateHealth(int health, int playerIndex)
    {
        if (health >= 0 && health <= 600)
        {
            playerProfile[playerIndex].transform.GetChild(1).GetComponent<Slider>().value = health / 600f;
        }
        else
        {
            playerProfile[playerIndex].transform.GetChild(1).GetComponent<Slider>().value = 1f;
        }
    }

    public void UpdateMana()
    {
        playerProfile[myArea].transform.GetChild(4).GetComponent<Text>().text = "x " + curMana.ToString();
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
