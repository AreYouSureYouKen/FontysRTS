using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	private int wood;
	private int meat;
	private int stone;
    private int playerNr;

    private int staminaUpgrade = 0;
    private int visionRangeUpgrade = 0;
    private int HealthUpgrade = 0;


    public int ResourcesPerTurn = 10;
    private string playerName;
	private GameObject UImanager;
	private UIControls UIController;
    private GameManager gamemanager;
    private List<GameObject> units = new List<GameObject>();

	// Use this for initialization
	void Start () {
		wood = 10;
		meat = 10;
		stone = 10;
        UImanager = GameObject.FindWithTag("EventManager");
		UIController = UImanager.GetComponent<UIControls> ();
        gamemanager = Component.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// at the start of the turn this will check if this player has at least 1 unit so that he can still do something, otherwise will remove this player from game
    /// </summary>
	public void StartTurn()
	{
        if (units.Count <= 0)
        {
            UIController.gameOver();
        }
        else
        {
            StartUnits();
            UIController.changeCurrentPlayer(this);
        }
        if(units.Count == 0)
        {
            gamemanager.PlayerGameOver(this);
            gamemanager.changeCurrentPlayer();
        }
	}

	public void EndTurn()
	{
        foreach(GameObject u in units)
        {
            u.GetComponent<Unit>().EndTurn();
        }
	}

    public void SetGameManager(GameManager gm)
    {
        this.gamemanager = gm;
    }

	public int GetWood()
	{
		return this.wood;
	}

	public int GetMeat()
	{
		return this.meat;
	}

	public int GetStone()
	{
		return this.stone;
	}

    public int GetStaminaUpgrade()
    {
        return this.staminaUpgrade;
    }

    public void UpgradeStamina(int amount)
    {
        staminaUpgrade += amount;
    }

    public int GetVisionRangeUpgrade()
    {
        return this.visionRangeUpgrade;
    }

    public void UpgradeVisionRange(int amount)
    {
        visionRangeUpgrade += amount;
    }

    public int GetHealthUpgrade()
    {
        return this.HealthUpgrade;
    }

    public void UpgradeHealth(int amount)
    {
        HealthUpgrade += amount;
    }
    /// <summary>
    /// logic to buy a unit, checks if it can buy a unit and returns true or false
    /// </summary>
    /// <param name="UnitToBuy"></param>
    /// <returns></returns>
	public bool BuyUnit(Unit UnitToBuy)
	{
        Debug.Log("buying unit");
        Debug.Log(UnitToBuy.GetWoodCost());
        if ((wood - UnitToBuy.GetWoodCost()) < 0 || (stone - UnitToBuy.GetStoneCost()) < 0 || (meat - UnitToBuy.GetMeatCost()) < 0)
            return false;

        
        this.wood -= UnitToBuy.GetWoodCost();
        this.stone -= UnitToBuy.GetStoneCost();
        this.meat -= UnitToBuy.GetMeatCost();
        UIController.updateGUI();
        return true;
	}
    /// <summary>
    /// checks if you can build a unit
    /// </summary>
    /// <param name="unitToBuild"></param>
    /// <returns></returns>
    public bool checkCost(Unit unitToBuild)
    {
        if ((wood - unitToBuild.GetWoodCost()) < 0 || (stone - unitToBuild.GetStoneCost()) < 0 || (meat - unitToBuild.GetMeatCost()) < 0)
            return false;

        return true;
    }
    

    public void SetPlayerNr(int i)
    {
        this.playerNr = i;
    }

    public int GetPlayerNr()
    {
        return this.playerNr;
    }
    /// <summary>
    /// adds a unit to this player for the clear fog of war and the logic to check if a player is game over or not
    /// </summary>
    /// <param name="u"></param>
    public void addUnit(GameObject u)
    {
        units.Add(u);
        if(gamemanager == null)
        {
            gamemanager = Component.FindObjectOfType<GameManager>();
        }
        gamemanager.GlobalAddUnit(u);
    }

    public void unitKilled(GameObject u)
    {
       
        gamemanager.GlobalRemoveUnit(u);
        units.Remove(u);
    }

    public void StartUnits()
    {
        wood += ResourcesPerTurn;
        meat += ResourcesPerTurn;
        stone += ResourcesPerTurn;

        foreach(GameObject u in units)
        {
            u.GetComponent<Unit>().StartTurn();
        }
    }

    public void AddWood()
    {
        wood += ResourcesPerTurn;
    }

    public void AddMeat()
    {
        meat += ResourcesPerTurn;
    }

    public void AddStone()
    {
        stone += ResourcesPerTurn;
    }

    public void useWood(int amount)
    {
        wood -= amount;
    }

    public void useStone(int amount)
    {
        stone -= amount;
    }

    public void useMeat(int amount)
    {
        meat -= amount;
    }

    public void SetPlayerName(string name)
    {
        this.playerName = name;
    }

    public string GetPlayerName()
    {
        return this.playerName;
    }
}