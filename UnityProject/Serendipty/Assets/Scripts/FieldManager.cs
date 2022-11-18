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

    private void UpdateFieldColor()
    {
        if (selectedFieldIndex == -1)
        {
            for (int i = 0; i < fieldObject.Length; i++)
            {
                fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            for (int i = 0; i < fieldObject.Length; i++)
            {
                if (i / 6 != selectedFieldIndex / 6)
                {
                    if (fieldObject[i].transform.childCount > 0)
                    {
                        fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 160f / 255f, 160f / 255f, 1f);
                    }
                    else
                    {
                        fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                }
                else if (i == selectedFieldIndex)
                {
                    fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(199f / 255f, 1f, 170f / 255f, 1f);
                }
                else
                {
                    fieldObject[i].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
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
                }
                else
                {
                    AttackManager.Instance.Attack(selectedFieldIndex, fieldIndex);
                }
            }
            else
            {
                if (selectedFieldIndex / 6 == fieldIndex / 6)
                {
                    Move(selectedFieldIndex, fieldIndex);
                }
            }
            selectedFieldIndex = -1;
        }
        UpdateFieldColor();
    }

    public void Move(int fieldIndex1, int fieldIndex2)
    {

    }
}
