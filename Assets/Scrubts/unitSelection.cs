using UnityEngine;
using System.Collections;

public class unitSelection : MonoBehaviour {
    private GameObject unit;
    private GameManager gm;
    public UIControls ui;
    public LayerMask onlyGrid;
    public LayerMask onlyUnits;

    public GameObject Builder;
    public GameObject Barracks;
    public GameObject StoneMine;
    public GameObject LumberMill;
    public GameObject Farm;
    public GameObject Scout;
    public GameObject Archer;

    private bool isBuilding = false;
    private bool isAttacking = false;
    private GameObject currentBuildingUnit;
	// Use this for initialization
	void Start () {
        gm = Camera.main.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        // for the preview of the unit you're currently building, snaps to center of tile you're hovering on.
        if(currentBuildingUnit != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, onlyGrid))
            {
                currentBuildingUnit.transform.position = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
            }
        }
        // to cancel building or attacking with the esc button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentBuildingUnit != null)
            {
                Destroy(currentBuildingUnit);
                currentBuildingUnit = null;
                isBuilding = false;
            }
            if(isAttacking)
            {
                isAttacking = false;
                StopAttacking();
                activateTiles(unit.transform.position);
            }
        }

        // left mouse click for selection of a unit.
        if (Input.GetMouseButtonDown(0))
        {
            ui.ActivateEndTurnButton();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,onlyUnits))
            {
                Unit unitComponent = hit.collider.gameObject.GetComponent<Unit>();
                if(unitComponent != null)
                if(unitComponent.GetPlayerNr().Equals(gm.getCurrentPlayernr()))
                updateSelectedUnit(hit.collider.gameObject); 
            }
        }
        // right mouse click for moving a unit, attacking a unit or building a unit/building
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if it is trying to attack it will check if the tile is moveable(in range) and will do the logic for it.
            if (isAttacking)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, onlyUnits))
                {
                    Unit hitunit = hit.collider.GetComponent<Unit>();
                    if(hitunit.getCurrentTile().Moveable)
                    {
                        Unit selUnit = unit.GetComponent<Unit>();
                        selUnit.AttackInRange(hitunit);
                        StopAttacking();
                        selUnit.useStamina(Vector3.Distance(unit.transform.position, hitunit.transform.position));
                        unit = null;
                        ui.DeactivatePanels();
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, onlyGrid))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.yellow, 5f);
                    Debug.Log(hit.transform.name);
                    Tile clickedTile = hit.collider.gameObject.GetComponent<Tile>();
                    if (clickedTile != null && clickedTile.Moveable)
                    {
                        if (!isBuilding)
                        {
                            moveUnitTowards(clickedTile);
                        }
                        else
                        {
                            tryPlace(clickedTile);
                        }
                    }
                }
            }
            
        }

	}

    private void updateSelectedUnit(GameObject selectedUnit)
    {
        clearTiles();
        unit = selectedUnit;
        if(selectedUnit.GetComponent<Unit>().CanMove())
        activateTiles(unit.transform.position);
        switch(selectedUnit.GetComponent<Unit>().getUnitType())
        {
            case "Builder":
                ui.ActivateBuilderUI();
                break;
            case "Barracks":
                ui.ActivateBarracksUI();
                break;
            case "Archer":
                ui.ActivateAttackPanel();
                break;

            default:
                break;
        }
    }

    public void endTurn()
    {
        this.unit = null;
        this.currentBuildingUnit = null;
        this.isBuilding = false;
        this.isAttacking = false;
        ui.DeactivatePanels();
        ui.DeactivateEndTurnButton();
    }

    private void moveUnitTowards(Tile tile)
    {
        clearTiles();
        if(unit != null)
        {
            Unit unitcomp = unit.GetComponent<Unit>();
            unitcomp.setCurrentTile(tile);
            unitcomp.SetTargetPosition(new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z));
        }
        unit = null;
        ui.DeactivatePanels();
    }
    //clears the tiles so that they cannot be moved to anymore
    public void clearTiles()
    {
        Collider[] hitColliders;
        int i;
        if (unit != null)
        {
            hitColliders = Physics.OverlapSphere(unit.transform.position, (unit.GetComponent<Unit>().getStamina() + gm.GetCurrentPlayer().GetStaminaUpgrade()),onlyGrid);
            i = 0;
            while (i < hitColliders.Length)
            {
                hitColliders[i].SendMessage("Disactivate");
                i++;
            }
        }
    }
    // activates the tiles with the given stamina of the current unit
    private void activateTiles(Vector3 position)
    {
        Collider[] hitColliders;
        int i =0;
        hitColliders = Physics.OverlapSphere(position, (unit.GetComponent<Unit>().getStamina() + gm.GetCurrentPlayer().GetStaminaUpgrade()), onlyGrid);
        while (i < hitColliders.Length)
        {
            if(!hitColliders[i].GetComponent<Tile>().occupied)
            hitColliders[i].SendMessage("Activate");
            i++;
        }
    }
    //tries to build the unit you're currently trying to build
    private void tryPlace(Tile clickedTile)
    {
        if (gm.BuyUnit(currentBuildingUnit.GetComponent<Unit>()))
        {
            GameObject neww = GameObject.Instantiate(currentBuildingUnit);
            neww.GetComponent<Unit>().SetPlayer(gm.GetCurrentPlayer());
            gm.GetCurrentPlayer().addUnit(neww);
            clearTiles();
            unit.GetComponent<Unit>().useStamina(Vector3.Distance(unit.transform.position, clickedTile.transform.position));
            neww.transform.position = new Vector3(clickedTile.transform.position.x, 0.5f, clickedTile.transform.position.z);
            neww.GetComponent<Unit>().setCurrentTile(clickedTile);
            unit = null;
            isBuilding = false;
            Destroy(currentBuildingUnit);
            currentBuildingUnit = null;
            ui.DeactivatePanels();
        }
    }
    //starts the logic for attackking, coloring the tiles red in the attack range.
    public void startAttacking()
    {
        if (unit.GetComponent<Unit>().getStamina() > 0)
        {
            clearTiles();
            isAttacking = true;
            if (unit != null)
            {
                Collider[] hitColliders;
                int i = 0;
                hitColliders = Physics.OverlapSphere(unit.transform.position, unit.GetComponent<Unit>().GetAttackRange(), onlyGrid);
                while (i < hitColliders.Length)
                {
                    hitColliders[i].SendMessage("PrepareYourAnus");
                    i++;
                }
            }
        }
    }

    public void StopAttacking()
    {
        Collider[] hitColliders;
        int i;
        isAttacking = false;
        if (unit != null)
        {
            hitColliders = Physics.OverlapSphere(unit.transform.position, unit.GetComponent<Unit>().GetAttackRange(), onlyGrid);
            i = 0;
            while (i < hitColliders.Length)
            {
                hitColliders[i].SendMessage("Disactivate");
                i++;
            }
        }
    }


    public void StartBuildBarracks()
    {
        if(!isBuilding && gm.CheckCost(Barracks.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(Barracks);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }

    public void StartTrainBuilder()
    {
        if (!isBuilding && gm.CheckCost(Builder.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(Builder);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }


    public void StartTrainArcher()
    {
        if (!isBuilding && gm.CheckCost(Archer.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(Archer);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }

    public void StartTrainScout()
    {
        if (!isBuilding && gm.CheckCost(Scout.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(Scout);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }

    public void StartBuildLumberMill()
    {
        if (!isBuilding && gm.CheckCost(LumberMill.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(LumberMill);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }
    public void StartBuildStoneMine()
    {
        if (!isBuilding && gm.CheckCost(StoneMine.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(StoneMine);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }
    public void StartBuildFarm()
    {
        if (!isBuilding && gm.CheckCost(Farm.GetComponent<Unit>()))
        {
            isBuilding = true;
            currentBuildingUnit = GameObject.Instantiate(Farm);
            currentBuildingUnit.transform.rotation = gm.GetCurrentPlayer().transform.rotation;
            activateTiles(unit.transform.position);
        }
    }


}
