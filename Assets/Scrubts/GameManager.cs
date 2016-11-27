using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	private int currentPlayer;
    private int numberOfTurns=0;
    private PlayerManager currentPlayerScript;
    public CameraController cc;
	public Camera mainCamera;
    public GameObject gameover;
    private bool gameStarted = false;
    public int numberOfPlayers;
    private List<PlayerManager> players = new List<PlayerManager>();
    private List<GameObject> allUnits = new List<GameObject>();
    private List<Tile> allTiles = new List<Tile>();


	// Use this for initialization
	void Start () {
		currentPlayer = 1;
        StartCoroutine(startGame());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// checks the count of the list and changes the current player to the next player in the list, if last, goes back to first
    /// </summary>
	public void changeCurrentPlayer()
	{
        numberOfTurns++;
        currentPlayerScript.EndTurn();
        Debug.Log(players.Count);
        addGlobalFogOfWar();
        if(currentPlayer >= players.Count)
        {
            currentPlayer = 1;
        }
        else
        {
            currentPlayer++;
        }
        Camera.main.GetComponent<unitSelection>().clearTiles();
        currentPlayerScript = players[(currentPlayer-1)];
        cc.setPlayer(currentPlayerScript.gameObject);
        rotateHealthBars(Camera.main.transform.position);
        currentPlayerScript.StartTurn();
	}

    public int getCurrentPlayernr()
    {
        return currentPlayer;
    }

    public PlayerManager GetCurrentPlayer()
    {
        return currentPlayerScript;
    }

    public void AddPlayer(PlayerManager pl)
    {
        players.Add(pl);
    }
    /// <summary>
    /// removes the player from the list so that he can't make turns anymore and for the game over sreen is there is only 1 player left
    /// </summary>
    /// <param name="pl"></param>
    public void PlayerGameOver(PlayerManager pl)
    {
        players.Remove(pl);
        Debug.Log("Player: " + pl.GetPlayerName() + " is game over.");
        if(players.Count == 1)
        {
            GameObject gmv = GameObject.Instantiate(gameover);
            DontDestroyOnLoad(gmv);
            gmv.GetComponent<GameOverMessage>().SetVictor(players[0].GetPlayerName());
            gmv.GetComponent<GameOverMessage>().SetTurns(numberOfTurns);
            Application.LoadLevel("GameOverScene");
        }
    }

    public bool HasTheGameStarted()
    {
        return gameStarted;
    }
    /// <summary>
    /// delayed start game so that everything is initialized
    /// </summary>
    /// <returns></returns>
    private IEnumerator startGame()
    {
        yield return new WaitForSeconds(1f);
        addGlobalFogOfWar();
        GameObject playerobj = GameObject.Find("Player" + currentPlayer);
        currentPlayerScript = playerobj.GetComponent<PlayerManager>();
        cc.setPlayer(playerobj);
        rotateHealthBars(Camera.main.transform.position);
        currentPlayerScript.StartTurn();

    }
    /// <summary>
    /// Adds the unit to the list to use in the adding of the fog of war
    /// </summary>
    /// <param name="u"></param>
    public void GlobalAddUnit(GameObject u)
    {
        allUnits.Add(u);
    }
    /// <summary>
    /// removes the specified unit from the list of all existing units
    /// </summary>
    /// <param name="u"></param>
    public void GlobalRemoveUnit(GameObject u)
    {
        allUnits.Remove(u);
        Destroy(u);
    }
    /// <summary>
    /// adds the fog of war on all the units known ni this game
    /// </summary>
    private void addGlobalFogOfWar()
    {
        mainCamera.GetComponent<unitSelection>().endTurn();
        foreach (GameObject u in allUnits)
        {
            Debug.Log("Disabling Unit: " + u.name);
            u.GetComponent<Unit>().OutofVision();
        }
        foreach(Tile t in allTiles)
        {
            t.Fog();  
        }
    }
    /// <summary>
    /// keeps a list of all the tiles in the grid
    /// </summary>
    /// <param name="t"></param>
    public void addTile(Tile t)
    {
        allTiles.Add(t);
    }
    /// <summary>
    /// checks if you can build this unit in the first place
    /// </summary>
    /// <param name="unitToBuild"></param>
    /// <returns></returns>
    public bool CheckCost(Unit unitToBuild)
    {
        return currentPlayerScript.checkCost(unitToBuild);
    }
    /// <summary>
    /// buys the unit you're currently building on the current player
    /// </summary>
    /// <param name="unitToBuy"></param>
    /// <returns></returns>
    public bool BuyUnit(Unit unitToBuy)
    {
        return currentPlayerScript.BuyUnit(unitToBuy);
    }

    /// <summary>
    /// Rotates health bars towards the initial camera position of the current player for clarity
    /// </summary>
    /// <param name="camPos"></param>
    private void rotateHealthBars(Vector3 camPos)
    {
        foreach (GameObject u in allUnits)
        {
            u.GetComponent<Unit>().RotateHealth(camPos);
        }
    }
}
