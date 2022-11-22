using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ResultManager : MonoBehaviourPun
{
    [SerializeField] bool isWinning;

    private void Start()
    {
        if (isWinning)
        {
            if (PlayerPrefs.HasKey("Gold"))
            {
                int gold = PlayerPrefs.GetInt("Gold");
                gold += StaticVariable.WinningResultGold;
                PlayerPrefs.SetInt("Gold", gold);
                PlayerPrefs.Save();
            }
        }
    }

    public void Main()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main");
    }
}
