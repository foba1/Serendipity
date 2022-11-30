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
    public static readonly int MinDeckCardCount = 20;
    public static readonly int MaxDeckCardCount = 30;

    // Card Property
    public static readonly int Light = 100;
    public static readonly int Dark = 101;
    public static readonly int Fire = 102;
    public static readonly int Water = 103;
    public static readonly int Wood = 104;

    // Card Type
    public static readonly int Creature = 300;
    public static readonly int Spell = 301;

    // Deck
    public static readonly int MaxDeckCount = 5;

    // Legendary Card Index
    public static readonly int[] LegendaryCardIndexArray = new int[5] { 3, 7, 11, 15, 19 };

    // Player Max Health
    public static readonly int PlayerMaxHealth = 600;

    // Winning Result Gold
    public static readonly int WinningResultGold = 20;

    // Card Index
    public static readonly int CardCount = 20;
    public static readonly int Resurrection = 0;
    public static readonly int SpearKnight = 1;
    public static readonly int HolyKnight = 2;
    public static readonly int Valkyrie = 3;
    public static readonly int Skeleton = 4;
    public static readonly int GrimReaper = 5;
    public static readonly int Wraith = 6;
    public static readonly int Lich = 7;
    public static readonly int FireBall = 8;
    public static readonly int FireDemon = 9;
    public static readonly int FireSpirit = 10;
    public static readonly int FireDragon = 11;
    public static readonly int BlueSlime = 12;
    public static readonly int WaterGolem = 13;
    public static readonly int WaterSpike = 14;
    public static readonly int KingOfSea = 15;
    public static readonly int Bee = 16;
    public static readonly int ParentWolf = 17;
    public static readonly int NatureCycle = 18;
    public static readonly int GreatTurtle = 19;
    public static readonly int ChildWolf = 1000;
}
