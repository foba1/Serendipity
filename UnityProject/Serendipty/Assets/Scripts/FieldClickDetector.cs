using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldClickDetector : MonoBehaviour
{
    [SerializeField] int fieldIndex;

    private bool isOver = false;
    private bool ableToClick = false;
    private bool prevMouseButtonDown = false;

    void Update()
    {
        if (prevMouseButtonDown)
        {
            if (!Input.GetMouseButton(0))
            {
                if (isOver && ableToClick)
                {
                    FieldManager.Instance.SelectField(fieldIndex);
                    ableToClick = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (isOver)
                {
                    ableToClick = true;
                }
            }
        }

        prevMouseButtonDown = Input.GetMouseButton(0);
    }

    void OnMouseOver()
    {
        isOver = true;
    }

    void OnMouseExit()
    {
        isOver = false;
    }
}
