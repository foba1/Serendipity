using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPun
{
    [Header("Top Object")]
    [SerializeField] GameObject topObject;

    [Header("Profile Object")]
    [SerializeField] GameObject[] playerProfile;

    [Header("Turn Object")]
    [SerializeField] GameObject turnObject;

    [Header("Live2D Panel")]
    [SerializeField] GameObject live2DPanel;

    [Header("Game Result Object")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    public int turn = 0;
    public int myArea = 0;
    public int curMana = 0;
    public static int[] mana = new int[12] { 3, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

    private float startTime = 0f;

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

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (startTime + 60f <= Time.time)
            {
                turn++;
                photonView.RPC("ResetBeforeNextTurn", RpcTarget.AllBuffered);
                photonView.RPC("GoToNextTurn", RpcTarget.AllBuffered, turn);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (IsMyTurn())
                {
                    turn++;
                    photonView.RPC("ResetBeforeNextTurn", RpcTarget.AllBuffered);
                    photonView.RPC("GoToNextTurn", RpcTarget.AllBuffered, turn);
                }
            }
        }
        else
        {
            if (startTime + 60f > Time.time)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (IsMyTurn())
                    {
                        turn++;
                        photonView.RPC("ResetBeforeNextTurn", RpcTarget.AllBuffered);
                        photonView.RPC("GoToNextTurn", RpcTarget.AllBuffered, turn);
                    }
                }
            }
        }

        topObject.transform.GetChild(1).GetComponent<Text>().text = Mathf.FloorToInt(60f + startTime - Time.time).ToString();
    }

    public void ButtonDown(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition -= new Vector3(0f, 20f, 0f);
    }

    public void ButtonUp(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition += new Vector3(0f, 20f, 0f);
    }

    public void InstantiateLive2D(int cardIndex)
    {
        Instantiate(Resources.Load<GameObject>("Live2D/" + cardIndex.ToString()), live2DPanel.transform);
    }

    IEnumerator MyTurnCoroutine()
    {
        turnObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);

        turnObject.SetActive(false);
    }

    [PunRPC]
    public void GoToNextTurn(int nextTurn)
    {
        startTime = Time.time;
        turn = nextTurn;
        if (turn % 2 == 0)
        {
            topObject.transform.GetChild(2).GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
            topObject.transform.GetChild(3).GetComponent<Image>().color = new Color(230f / 255f, 230f / 255f, 230f / 255f, 1f);
        }
        else
        {
            topObject.transform.GetChild(2).GetComponent<Image>().color = new Color(230f / 255f, 230f / 255f, 230f / 255f, 1f);
            topObject.transform.GetChild(3).GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
        }
        FieldManager.Instance.ActiveCreature(turn);

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
            photonView.RPC("UpdateMana", RpcTarget.AllBuffered, curMana, myArea);
            DeckManager.Instance.Draw();
            StartCoroutine(MyTurnCoroutine());
        }
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
    public void ResetBeforeNextTurn()
    {
        FieldManager.Instance.ResetBeforeNextTurn();
    }

    [PunRPC]
    public void UpdateMana(int mana, int playerIndex)
    {
        playerProfile[playerIndex].transform.GetChild(4).GetComponent<Text>().text = "x " + mana.ToString();
    }

    [PunRPC]
    public void UseSpell(int pos, int cardIndex)
    {
        FieldManager.Instance.UseSpell(pos, cardIndex);
    }

    [PunRPC]
    public void SpawnCreature(int pos, int cardIndex, int additionalPower, int additionalHealth)
    {
        FieldManager.Instance.SpawnCreature(pos, cardIndex, additionalPower, additionalHealth);
    }

    [PunRPC]
    public void SpawnCreature(int pos, int cardIndex)
    {
        FieldManager.Instance.SpawnCreature(pos, cardIndex);
    }

    [PunRPC]
    public void Attack(int selectedFieldIndex, int fieldIndex)
    {
        AttackManager.Instance.Attack(selectedFieldIndex, fieldIndex);
    }

    [PunRPC]
    public void Move(int fieldIndex1, int fieldIndex2)
    {
        FieldManager.Instance.Move(fieldIndex1, fieldIndex2);
    }

    public bool IsMyTurn()
    {
        if (myArea % 2 == turn % 2) return true;
        else return false;
    }

    public void StartGame()
    {
        startTime = Time.time;
        turn = 0;
        UpdateHealth(StaticVariable.PlayerMaxHealth, 0);
        UpdateHealth(StaticVariable.PlayerMaxHealth, 1);
        FieldManager.Instance.InstantiatePlayer();
        DeckManager.Instance.InitializeDeck();
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("GoToNextTurn", RpcTarget.AllBuffered, turn);
        }
    }

    public void Surrender()
    {
        if (myArea == 0)
        {
            photonView.RPC("FinishGame", RpcTarget.AllBuffered, 1);
        }
        else
        {
            photonView.RPC("FinishGame", RpcTarget.AllBuffered, 0);
        }
    }

    [PunRPC]
    public void FinishGame(int winner)
    {
        if (myArea == winner)
        {
            winPanel.SetActive(true);
            if (PlayerPrefs.HasKey("Gold"))
            {
                int gold = PlayerPrefs.GetInt("Gold");
                gold += StaticVariable.WinningResultGold;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.Save();
            }
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void Main()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main");
    }
}
