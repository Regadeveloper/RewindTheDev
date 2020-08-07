using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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

    public static int extraDamage = 0;
    public static int extraDefense = 0;

    //Stats
    public static int SoldierLife = 20;
    public static int TankLife = 50;
    public static int ArcherLife = 20;
    public static int PlaneLife = 30;

    public static int SoldierDmg = 2;
    public static int TankDmg = 10;
    public static int ArcherDmg = 10;
    public static int PlaneDmg = 5;
}