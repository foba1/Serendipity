using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] GameObject[] fieldObject;

    private int selectedFieldIndex = -1;
    private bool grap = false;
    private int grapIndex = -1;

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
        Debug.Log("Select " + fieldIndex.ToString());
        if (fieldIndex < 0 || fieldIndex >= fieldObject.Length) return;
        if (fieldObject[fieldIndex].transform.childCount > 0)
        {
            selectedFieldIndex = fieldIndex;
        }
    }

    public void DeselectField()
    {
        selectedFieldIndex = -1;
    }

    private void GrapOrDrop(int fieldIndex)
    {
        if (grap)
        {

        }
        else
        {
            grap = true;
            grapIndex = fieldIndex;
        }
    }
}
