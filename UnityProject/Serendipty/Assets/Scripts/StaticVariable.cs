using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariable
{
    // Card Class
    public static int Legendary = 200;
    public static int Normal = 201;

    // Card Price
    public static int LegendaryCardPrice = 200;
    public static int NormalCardPrice = 40;

    // Card Type
    public static int Light = 100;
    public static int Dark = 101;
    public static int Fire = 102;
    public static int Water = 103;
    public static int Wood = 104;

    // Deck
    public static int MaxDeckCount = 5;

    // Legendary Card Index
    public static int[] LegendaryCardIndexArray = new int[10] { 10, 11, 22, 23, 34, 35, 46, 47, 58, 59 };

    // Card Index
    public static int CardCount = 60;
    public static int ExampleCreatureIndex = 0;
}
