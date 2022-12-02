using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectStateText;
    public GameObject selectedDeckText;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectStateText.text = "온라인";
        }
        else
        {
            connectStateText.text = "서버에 접속중...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        if (!PlayerPrefs.HasKey("Gold"))
        {
            BasicCard();
        }
    }

    private void BasicCard()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < StaticVariable.CardCount; i++)
        {
            if (i % 4 == 3)
            {
                PlayerPrefs.SetInt("Card" + i, 0);
            }
            else
            {
                PlayerPrefs.SetInt("Card" + i, 2);
            }
        }
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("DeckCount", 0);
        PlayerPrefs.Save();
    }

    public void InitializeCardForTest()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < StaticVariable.CardCount; i++)
        {
            if (i % 4 == 3)
            {
                PlayerPrefs.SetInt("Card" + i, 1);
            }
            else
            {
                PlayerPrefs.SetInt("Card" + i, 3);
            }
        }
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("DeckCount", 0);
        PlayerPrefs.Save();
    }

    public override void OnConnectedToMaster()
    {
        connectStateText.text = "온라인";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectStateText.text = "서버 접속에 실패하였습니다..";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void QuickMatching()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (StaticVariable.MyDeck == "" || StaticVariable.MyDeck.Length != StaticVariable.CardCount)
            {
                selectedDeckText.GetComponent<Text>().text = "덱을 선택해야 합니다.";
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        else
        {
            connectStateText.text = "서버 접속에 실패하였습니다..";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Collection()
    {
        SceneManager.LoadScene("Collection");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Loading");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Room_" + Random.Range(0, 1000), new RoomOptions { MaxPlayers = 2 });
    }
}
