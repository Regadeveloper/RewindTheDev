using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    SOLDIER = 0,
    ARCHER,
    PLANE,
    TANK,
    MAX_UNITS
}

public static class Globals 
{
    public static int soldierCost = 1;
    public static int archerCost = 3;
    public static int planeCost = 10;
    public static int tankCost = 20;
    public static int level = 1; 
}