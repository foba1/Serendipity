using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    private List<int> redGraveList;
    private List<int> blueGraveList;

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
        redGraveList = new List<int>();
        blueGraveList = new List<int>();
    }

    public void Add(int area, int cardIndex)
    {
        if (area == 0)
        {
            redGraveList.Add(cardIndex);
        }
        else
        {
            blueGraveList.Add(cardIndex);
        }
    }

    public int RandomPop(int area)
    {
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            int index = Random.Range(0, redGraveList.Count);
            int result = redGraveList[index];
            redGraveList.RemoveAt(index);
            return result;
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            int index = Random.Range(0, blueGraveList.Count);
            int result = blueGraveList[index];
            blueGraveList.RemoveAt(index);
            return result;
        }
    }

    public int Pop(int area)
    {
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            int result = redGraveList[redGraveList.Count - 1];
            redGraveList.RemoveAt(redGraveList.Count - 1);
            return result;
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            int result = blueGraveList[blueGraveList.Count - 1];
            blueGraveList.RemoveAt(blueGraveList.Count - 1);
            return result;
        }
    }
}
