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
        if (fieldObject[fieldIndex].transform.childCount > 0)
        {
            if (selectedFieldIndex == -1)
            {
                selectedFieldIndex = fieldIndex;
                Debug.Log("Select " + fieldIndex.ToString());

            }
            else
            {
                AttackManager.Instance.Attack(selectedFieldIndex, fieldIndex);
                Debug.Log(selectedFieldIndex.ToString() + " attacks " + fieldIndex.ToString());
                selectedFieldIndex = -1;
            }
        }
    }
}
