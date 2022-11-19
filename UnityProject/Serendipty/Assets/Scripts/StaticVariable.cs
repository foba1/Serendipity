using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariable
{
    // Deck
    public static string MyDeck = "";

    // Card Class
    public static readonly int Legendary = 200;
    public static readonly int Normal = 201;

    // Card Price
    public static readonly int LegendaryCardPrice = 200;
    public static readonly int NormalCardPrice = 40;

    // Max & Min Card Count
    public static readonly int MaxLegendaryCardCount = 1;
    public static readonly int MaxNormalCardCount = 3;
    public static readonly int MaxDeckCardCount = 30;
    public static readonly int MinDeckCardCount = 20;

    // Card Property
    public static readonly int Light = 100;
    public static readonly int Dark = 101;
    public static readonly int Fire = 102;
    public static readonly int Water = 103;
    public static readonly int Wood = 104;

    // Card Type
    public static readonly int Creature = 300;
    public static readonly int InstantSpell = 301;
    public static readonly int LongTermSpell = 302;

    // Deck
    public static readonly int MaxDeckCount = 5;

    // Legendary Card Index
    public static readonly int[] LegendaryCardIndexArray = new int[10] { 10, 11, 22, 23, 34, 35, 46, 47, 58, 59 };

    // Card Index
    public static readonly int CardCount = 20;
    public static readonly int Skeleton = 0;
}
