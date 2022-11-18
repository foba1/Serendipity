using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject[] fieldObject;

    private int selectedFieldIndex = -1;

    static FieldManager instance;
    public static FieldManager Instance
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
        DontDestroyOnLoad(this);
    }

    public void SelectField(int fieldIndex)
    {
        if (fieldIndex < 0 || fieldIndex >= fieldObject.Length) return;
        if (selectedFieldIndex == -1)
        {
            if (fieldObject[fieldIndex].transform.childCount > 0)
            {
                selectedFieldIndex = fieldIndex;
            }
        }
        else
        {
            if (fieldObject[fieldIndex].transform.childCount > 0)
            {
                if (selectedFieldIndex / 6 == fieldIndex / 6)
                {
                    Move(selectedFieldIndex, fieldIndex);
                    selectedFieldIndex = -1;
                }
                else
                {
                    AttackManager.Instance.Attack(selectedFieldIndex, fieldIndex);
                    selectedFieldIndex = -1;
                }
            }
            else
            {
                if (selectedFieldIndex / 6 == fieldIndex / 6)
                {
                    Move(selectedFieldIndex, fieldIndex);
                    selectedFieldIndex = -1;
                }
                else
                {
                    selectedFieldIndex = -1;
                }
            }
        }
    }

    public void Move(int fieldIndex1, int fieldIndex2)
    {

    }
}
