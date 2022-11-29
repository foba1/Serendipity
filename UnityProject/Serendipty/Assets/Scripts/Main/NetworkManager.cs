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
            connectStateText.text = "�¶���";
        }
        else
        {
            connectStateText.text = "������ ������...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        connectStateText.text = "�¶���";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectStateText.text = "���� ���ӿ� �����Ͽ����ϴ�..";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void QuickMatching()
    {
        if (PhotonNetwork.IsConnected)
        {
            StaticVariable.MyDeck = "33000000333030003330";
            if (StaticVariable.MyDeck == "" || StaticVariable.MyDeck.Length != StaticVariable.CardCount)
            {
                selectedDeckText.GetComponent<Text>().text = "���� �����ؾ� �մϴ�.";
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        else
        {
            connectStateText.text = "���� ���ӿ� �����Ͽ����ϴ�..";
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
