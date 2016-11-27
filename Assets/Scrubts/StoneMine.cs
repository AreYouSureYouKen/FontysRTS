using UnityEngine;
using System.Collections;

public class StoneMine : Unit
{
    public int overrideWood = 10;
    public int overrideStone = 10;
    public int overrideMeat = 5;
    // Use this for initialization
    void Start()
    {
        visionRange = 1f;
        maxStamina = 0f;
        attackRange = 0f;
        currentStamina = maxStamina;
        isMoveable = false;
        canBuild = false;
        health = 2;
        attackDamage = 0;
        unitType = "StoneMine";
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

    public override void StartTurn()
    {
        base.StartTurn();
        player.AddStone();
    }
}
