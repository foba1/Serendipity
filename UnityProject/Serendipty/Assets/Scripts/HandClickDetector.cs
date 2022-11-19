using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandClickDetector : MonoBehaviour
{
    private int handIndex;
    private bool isOver = false;
    private bool ableToClick = false;
    private bool prevMouseButtonDown = false;

    private void Start()
    {
        for (int i = 0; i < HandManager.Instance.handObject.Length; i++)
        {
            if (HandManager.Instance.handObject[i].transform == transform.parent)
            {
                handIndex = i;
                return;
            }
        }
    }

    private void Update()
    {
        if (prevMouseButtonDown)
        {
            if (!Input.GetMouseButton(0))
            {
                if (isOver && ableToClick)
                {
                    HandManager.Instance.SelectHand(handIndex);
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
