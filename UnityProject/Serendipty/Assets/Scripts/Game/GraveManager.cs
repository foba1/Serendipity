using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    private List<int> graveList;

    static GraveManager instance;
    public static GraveManager Instance
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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        graveList = new List<int>();
    }

    public void Add(int cardIndex)
    {
        graveList.Add(cardIndex);
    }

    public int RandomPop()
    {
        if (graveList.Count == 0) return -1;
        int index = Random.Range(0, graveList.Count);
        int result = graveList[index];
        graveList.RemoveAt(index);
        return result;
    }

    public int Pop()
    {
        if (graveList.Count == 0) return -1;
        int result = graveList[graveList.Count - 1];
        graveList.RemoveAt(graveList.Count - 1);
        return result;
    }
}
