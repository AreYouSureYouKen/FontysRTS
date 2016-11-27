using UnityEngine;
using System.Collections;

public class Scout : Unit
{
    public int overrideWood = 10;
    public int overrideStone = 10;
    public int overrideMeat = 10;
    // Use this for initialization
    void Start()
    {
        visionRange = 4f;
        maxStamina = 3f;
        attackRange = 0f;
        currentStamina = maxStamina;
        isMoveable = true;
        canBuild = false;
        health = 1;
        attackDamage = 0;
        unitType = "Scout";
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
