using UnityEngine;
using System.Collections;

public class Barracks : Unit {
    public int overrideWood = 10;
    public int overrideStone = 10;
    public int overrideMeat = 5;


	// Use this for initialization
	void Start () {
        visionRange = 3f;
        maxStamina = 2f;
        attackRange = 1f;
        currentStamina = maxStamina;
        isMoveable = false;
        canBuild = true;
        health = 4;
        attackDamage = 0;
        unitType = "Barracks";
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
