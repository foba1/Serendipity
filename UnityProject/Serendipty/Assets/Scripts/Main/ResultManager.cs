using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
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
        SceneManager.LoadScene("Main");
    }
}
