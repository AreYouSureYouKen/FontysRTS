using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControls : MonoBehaviour {
	public Text wood;
	public Text meat;
	public Text stone;
    public Text playerName;
    public GameObject builderPanel;
    public GameObject barracksPanel;
    public GameObject AttackPanel;
    

    public Button endTurnButton;
	private PlayerManager currentPlayer;
	public GameManager gm;
    public unitSelection unitSelection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeCurrentPlayer(PlayerManager player)
	{
		this.currentPlayer = player;
		updateGUI ();
	}

	public void updateGUI ()
	{
		this.wood.text = "Wood: "+currentPlayer.GetWood ();
		this.meat.text = "Meat: "+currentPlayer.GetMeat ();
		this.stone.text = "Stone: "+currentPlayer.GetStone ();
        this.playerName.text = "Player: " + currentPlayer.GetPlayerName();
	}

	public void endTurn()
	{
		gm.changeCurrentPlayer ();
	}

    public void ActivateBuilderUI()
    {
        this.barracksPanel.SetActive(false);
        this.AttackPanel.SetActive(true);
        this.builderPanel.SetActive(true);
    }

    public void ActivateBarracksUI()
    {
        this.builderPanel.SetActive(false);
        this.AttackPanel.SetActive(false);
        this.barracksPanel.SetActive(true);
    }

    public void DeactivatePanels()
    {
        this.builderPanel.SetActive(false);
        this.barracksPanel.SetActive(false);
        this.AttackPanel.SetActive(false);
    }
    
    public void ActivateAttackPanel()
    {
        this.AttackPanel.SetActive(true);
    }

    public void DeactivateAttackPanel()
    {
        this.AttackPanel.SetActive(false);
    }

    public void buildBarracks()
    {
        unitSelection.StartBuildBarracks();
    }

    public void trainBuilder()
    {
        unitSelection.StartTrainBuilder();
    }

    public void trainScout()
    {
        unitSelection.StartTrainScout();
    }

    public void trainArcher()
    {
        unitSelection.StartTrainArcher();
    }

    public void buildLumberMill()
    {
        unitSelection.StartBuildLumberMill();
    }

    public void buildStoneMine()
    {
        unitSelection.StartBuildStoneMine();
    }

    public void buildFarm()
    {
        unitSelection.StartBuildFarm();
    }

    public void upgradeStamina()
    {
        currentPlayer.UpgradeStamina(1);
        currentPlayer.useMeat(10);
        currentPlayer.useStone(10);
        currentPlayer.useWood(10);
    }

    public void upgradeHealth()
    {
        currentPlayer.UpgradeHealth(1);
        currentPlayer.useMeat(10);
        currentPlayer.useStone(10);
        currentPlayer.useWood(10);
    }

    public void upgradeVision()
    {
        currentPlayer.UpgradeVisionRange(1);
        currentPlayer.useMeat(10);
        currentPlayer.useStone(10);
        currentPlayer.useWood(10);
    }

    public void startAttack()
    {
        unitSelection.startAttacking();
    }

    public void gameOver()
    {

    }

    public void ActivateEndTurnButton()
    {
        //endTurnButton.interactable = true; removed in last patch??
        //endTurnButton.enabled = true; not working cause public Button endTurnButton does not accept Unity UI Button anymore? GG!
    }

    public void DeactivateEndTurnButton()
    {
        //endTurnButton.interactable = false; removed in last patch???
        //endTurnButton.enabled = false;
    }
}
