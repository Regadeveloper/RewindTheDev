using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IASpawnControl : MonoBehaviour
{
    public float goldGain = 1f;
    public float spawnTik = 3f;
    public int maxEnemiesTik = 3;
    [SerializeField] Transform airSpawn;
    [SerializeField] Transform groundSpawn;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject plane;
    [SerializeField] GameObject archer;
    [SerializeField] GameObject tank;

    float count;
    [SerializeField]float gold;

    void Start()
    {
        count = 0;
        gold = 0;
    }

    void Update()
    {
        if (Time.deltaTime > 0)
        {
            if (count < spawnTik) count += Time.deltaTime;
            else
            {
                gold += goldGain * spawnTik;
                GameObject unit;
                for(int i = 0; i < maxEnemiesTik;i++)
                {
                    int num = Random.Range(0, 4);
                    switch(num)
                    {
                        case 0:
                            if (gold >= Globals.soldierCost)
                            {
                                gold -= Globals.soldierCost;
                                unit = Instantiate(soldier, groundSpawn.position, Quaternion.identity);
                                unit.GetComponent<UnitBehaviour>().playerUnit = false;
                            }
                            break;
                        case 1:
                            if (gold >= Globals.archerCost)
                            {
                                gold -= Globals.archerCost;
                                unit = Instantiate(archer, groundSpawn.position, Quaternion.identity);
                                unit.GetComponent<UnitBehaviour>().playerUnit = false;
                            }
                            break;
                        case 2:
                            if (gold >= Globals.planeCost)
                            {
                                gold -= Globals.planeCost;
                                unit = Instantiate(plane, groundSpawn.position, Quaternion.identity);
                                unit.GetComponent<UnitBehaviour>().playerUnit = false;
                            }
                            break;
                        case 3:
                            if (gold >= Globals.tankCost)
                            {
                                gold -= Globals.tankCost;
                                unit = Instantiate(tank, groundSpawn.position, Quaternion.identity);
                                unit.GetComponent<UnitBehaviour>().playerUnit = false;
                            }
                            break;
                    }
                }

                count = 0;
            }
        }
    }
}
