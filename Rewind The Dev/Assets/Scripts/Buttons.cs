using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] Transform groundPos;
    [SerializeField] Transform airPos;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject plane;
    [SerializeField] GameObject archer;
    [SerializeField] GameObject tank;
    GameObject unit;
    public void SpawnSoldier()
    {
        //gold -= Globals.soldierCost;
        unit = Instantiate(soldier, groundPos.position, Quaternion.identity);
        unit.GetComponent<UnitBehaviour>().playerUnit = true;
    }

    public void SpawnPlane()
    {
        //gold -= Globals.planeCost;
        unit = Instantiate(plane, airPos.position, Quaternion.identity);
        unit.GetComponent<UnitBehaviour>().playerUnit = true;
    }

    public void SpawnTank()
    {
        //gold -= Globals.tankCost;
        unit = Instantiate(tank, groundPos.position, Quaternion.identity);
        unit.GetComponent<UnitBehaviour>().playerUnit = true;
    }

    public void SpawnArcher()
    {
        //gold -= Globals.archerCost;
        unit = Instantiate(archer, groundPos.position, Quaternion.identity);
        unit.GetComponent<UnitBehaviour>().playerUnit = true;
    }

    public void UpgradeDamage()
    {
        Globals.extraDamage += 1;
    }

    public void UpgradeDefense()
    {
        Globals.extraDefense += 1;
    }
}
