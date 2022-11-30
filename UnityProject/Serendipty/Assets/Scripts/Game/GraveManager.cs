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
        int result, index;
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            index = Random.Range(0, redGraveList.Count);
            result = redGraveList[index];
            redGraveList.RemoveAt(index);
            return result;
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            index = Random.Range(0, blueGraveList.Count);
            result = blueGraveList[index];
            blueGraveList.RemoveAt(index);
            return result;
        }
    }

    public int Pop(int area)
    {
        int result;
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            result = redGraveList[redGraveList.Count - 1];
            redGraveList.RemoveAt(redGraveList.Count - 1);
            return result;
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            result = blueGraveList[blueGraveList.Count - 1];
            blueGraveList.RemoveAt(blueGraveList.Count - 1);
            return result;
        }
    }

    public int HighLightPop(int area)
    {
        int result, index = -1, high = -1;
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            for (int i = redGraveList.Count - 1; i >= 0; i--)
            {
                if (redGraveList[i] / 4 == 0)
                {
                    if (high < redGraveList[i])
                    {
                        high = redGraveList[i];
                        index = i;
                    }
                }
            }
            if (index == -1) return -1;
            else
            {
                result = redGraveList[index];
                redGraveList.RemoveAt(index);
                return result;
            }
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            for (int i = blueGraveList.Count - 1; i >= 0; i--)
            {
                if (blueGraveList[i] / 4 == 0)
                {
                    if (high < blueGraveList[i])
                    {
                        high = blueGraveList[i];
                        index = i;
                    }
                }
            }
            if (index == -1) return -1;
            else
            {
                result = blueGraveList[index];
                blueGraveList.RemoveAt(index);
                return result;
            }
        }
    }

    public int UndeadPop(int area)
    {
        int result;
        if (area == 0)
        {
            if (redGraveList.Count == 0) return -1;
            for (int i = redGraveList.Count - 1; i >= 0; i--)
            {
                if (redGraveList[i] / 4 == 1 && redGraveList[i] != StaticVariable.Lich)
                {
                    result = redGraveList[i];
                    redGraveList.RemoveAt(i);
                    return result;
                }
                else continue;
            }
            result = -1;
            return result;
        }
        else
        {
            if (blueGraveList.Count == 0) return -1;
            for (int i = blueGraveList.Count - 1; i >= 0; i--)
            {
                if (blueGraveList[i] / 4 == 1 && blueGraveList[i] != StaticVariable.Lich)
                {
                    result = blueGraveList[i];
                    blueGraveList.RemoveAt(i);
                    return result;
                }
                else continue;
            }
            result = -1;
            return result;
        }
    }
}
