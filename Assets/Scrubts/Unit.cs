using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    protected PlayerManager player;
    protected float visionRange;
    protected float attackRange;
    protected float maxStamina;
    protected int health;
    protected int attackDamage;
    protected float currentStamina;
    protected bool isMoveable;
    protected bool canBuild;
    protected string unitType;
    protected int woodCost;
    protected int meatCost;
    protected int stoneCost;
    private bool myTurn;
    public healthBar hb;
    private Tile onCurrentTile;
    protected Vector3 targetPosition;

    public LayerMask OnlyUnits;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetPlayer(PlayerManager i)
    {
        this.player = i;
        hb.setPlayer(i);
    }

    public int GetPlayerNr()
    {
        Debug.Log(player);
        return this.player.GetPlayerNr();
    }
    /// <summary>
    /// sets the target position, turns the unit towards the position and starts the movement
    /// </summary>
    /// <param name="position"></param>
    public void SetTargetPosition(Vector3 position)
    {
        useStamina(Vector3.Distance(transform.position, position));
        this.targetPosition = position;
        Debug.Log("starting movement");
        StartCoroutine(moveUnit(this.transform.position,position,1f));
        
    }
    /// <summary>
    /// moves the unit and if it is still the players turn, clears the fog of war.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="overTime"></param>
    /// <returns></returns>
    IEnumerator moveUnit(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        transform.LookAt(target);
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        if (myTurn)
        {
            ClearFogOfWar();
        }
        transform.rotation = player.transform.rotation;
        transform.position = target;
    }

    public void Disactivate()
    {

    }

    public void Activate()
    {

    }

    public float getStamina()
    {
        return this.currentStamina;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public virtual void StartTurn()
    {
        currentStamina = maxStamina;
        myTurn = true;
        ClearFogOfWar();
    }

    public void EndTurn()
    {
        myTurn = false;
    }

    public void useStamina(float amount)
    {
        currentStamina -= amount;
    }

    public float GetAttackRange()
    {
        return this.attackRange;
    }
    /// <summary>
    /// makes an overlapsphere raycast and everything that it hit with layer grid or unit will be made visible (light grey for grid tiles)
    /// </summary>
    public void ClearFogOfWar()
    {
        Collider[] hitColliders;
        int i = 0;
        Debug.Log("clearing fog with :"+visionRange);
        hitColliders = Physics.OverlapSphere(this.transform.position, (visionRange+player.GetVisionRangeUpgrade()),OnlyUnits);
        while (i < hitColliders.Length)
        {
            hitColliders[i].SendMessage("InVision");
            i++;
        }
    }

    public bool CanMove()
    {
        return this.isMoveable;
    }

    public bool CanBuild()
    {
        return this.canBuild;
    }

    public string getUnitType()
    {
        return this.unitType;
    }

    public virtual int GetWoodCost()
    {
        return this.woodCost;
    }

    public virtual int GetStoneCost()
    {
        return this.stoneCost;
    }

    public virtual int GetMeatCost()
    {
        return this.meatCost;
    }

    public Tile getCurrentTile()
    {
        return onCurrentTile;
    }

    public void setCurrentTile(Tile newTile)
    {
        if(onCurrentTile != null)
        onCurrentTile.deoccupy();
        this.onCurrentTile = newTile;
        onCurrentTile.occupy();
    }
    /// <summary>
    /// uses stamina from the distance of this unit to the unit that is being attacked
    /// </summary>
    /// <param name="unitToAttack"></param>
    public void AttackInRange(Unit unitToAttack)
    {
        if (currentStamina > 0 && this != unitToAttack)
        {
            unitToAttack.BeAttacked(attackDamage);
        }
    }
    /// <summary>
    /// calculated health after attack
    /// </summary>
    /// <param name="forDamage"></param>
    public void BeAttacked(int forDamage)
    {
        this.health -= forDamage;
        hb.SetTargetHealth(health);
        if ((this.health+player.GetHealthUpgrade()) == 0)
        {
            player.unitKilled(this.gameObject);
        }

    }

    public int GetHealthUpgrade()
    {
        return player.GetHealthUpgrade();
    }

    public void InVision()
    {
        this.GetComponent<Renderer>().enabled = true;
        hb.InVision();
    }

    public void RotateHealth(Vector3 position)
    {
        hb.SetNewPlayer(position);
    }

    public void OutofVision()
    {
        this.GetComponent<Renderer>().enabled = false;
        hb.OutofVision();
    }
}
