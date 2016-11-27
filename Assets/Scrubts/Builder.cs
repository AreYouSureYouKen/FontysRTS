using UnityEngine;
using System.Collections;

public class Builder : Unit {
    public int overrideWood = 5;
    public int overrideStone = 0;
    public int overrideMeat = 10;


	// Use this for initialization
	void Start () {
        visionRange = 3f;
        maxStamina = 2f;
        attackRange = 1f;
        attackDamage = 1;
        health = 2;
        currentStamina = maxStamina;
        isMoveable = true;
        canBuild = true;
        unitType = "Builder";
        stoneCost = 5;
        meatCost = 5;
        woodCost = 5;
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
