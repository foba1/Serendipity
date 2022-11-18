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
            if (!Input.GetMouseButtonDown(0))
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
            if (Input.GetMouseButtonDown(0))
            {
                if (isOver)
                {
                    ableToClick = true;
                }
            }
            else
            {
                ableToClick = false;
            }
        }

        prevMouseButtonDown = Input.GetMouseButtonDown(0);
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
