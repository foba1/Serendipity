using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Card : MonoBehaviourPun
{
    public int cardIndex;
    public int cardClass;
    public int cardProperty;
    public int cardType;
    public int cost;
    public int additionalCost;
}
