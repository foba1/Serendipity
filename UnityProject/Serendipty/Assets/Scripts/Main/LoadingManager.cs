using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviourPun
{
    private float timeCount;

    public Text loadingText;
    public GameObject loadingObject;

    private void Start()
    {
        timeCount = 0f;
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length < 2)
        {
            if (timeCount <= 1.5f) timeCount += Time.deltaTime;
            if (timeCount <= 0.5f)
            {
                loadingText.text = "플레이어를 기다리는중.";
            }
            else if (timeCount <= 1f)
            {
                loadingText.text = "플레이어를 기다리는중..";
            }
            else if (timeCount <= 1.5f)
            {
                loadingText.text = "플레이어를 기다리는중...";
            }
            else
            {
                timeCount = 0f;
            }
            loadingObject.transform.eulerAngles += new Vector3(0f, 0f, 0.1f);
        }
        else
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public void Main()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main");
    }

    public void ButtonDown(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition -= new Vector3(0f, 20f, 0f);
    }

    public void ButtonUp(GameObject button)
    {
        button.transform.GetChild(0).transform.localPosition += new Vector3(0f, 20f, 0f);
    }
}
