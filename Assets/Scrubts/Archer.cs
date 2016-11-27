using UnityEngine;
using System.Collections;

public class Archer : Unit
{
    public int overrideWood = 20;
    public int overrideStone = 20;
    public int overrideMeat = 20;
    // Use this for initialization
    void Start()
    {
        visionRange = 4f;
        maxStamina = 3f;
        attackRange = 2f;
        currentStamina = maxStamina;
        isMoveable = true;
        canBuild = false;
        health = 3;
        attackDamage = 1;
        unitType = "Archer";
        stoneCost = overrideStone;
        meatCost = overrideMeat;
        woodCost = overrideWood;
        targetPosition = this.transform.position;
        hb.setMaxHealth(health);
    }


    public override int GetWoodCost()
    {
        return overrideWood;
    }

    public override int GetStoneCost()
    {
        return overrideStone;
    }

    public override int GetMeatCost()
    {
        return overrideMeat;
    }
}
