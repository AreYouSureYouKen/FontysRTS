using UnityEngine;
using System.Collections;

public class GridCreation : MonoBehaviour {

    public GameObject Hex;
    public GameObject builder;
    public GameObject player;
    public GameManager GameManager;
    private playerSelect pls;


    public int NumberOfPlayers = 2;
    public int gridWithInHexes = 5;
    public int gridHeightInHexes = 5;


    private float hexWidth;
    private float hexHeight;

    private void setNumberOfPlayers()
    {
        this.pls = GameObject.FindWithTag("PlayerSelect").GetComponent<playerSelect>();
        this.NumberOfPlayers = pls.GetNumberOfPlayers();
        this.GameManager.numberOfPlayers = this.NumberOfPlayers;
    }


    private void setSizes()
    {
        Renderer hexRender = Hex.GetComponent<Renderer>();
        hexWidth = hexRender.bounds.size.x;
        hexHeight = hexRender.bounds.size.z;
    }

    private Vector3 calcInitPos()
    {
        Vector3 initPos;

        initPos = new Vector3(-hexWidth * gridWithInHexes / 2f + hexWidth / 2, 0, gridHeightInHexes / 2f * hexHeight - hexHeight / 2);
        return initPos;
    }

    public Vector3 calcWorldCoord(Vector2 gridPos)
    {
        Vector3 initpos = calcInitPos();

        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = initpos.x + offset + gridPos.x * hexWidth;
        float z = initpos.z - gridPos.y * hexHeight * 0.75f;
        return new Vector3(x, 0, z);
    }

    private void createGrid()
    {

        gridWithInHexes = gridWithInHexes * NumberOfPlayers;
        gridHeightInHexes = gridHeightInHexes * NumberOfPlayers;
        GameManager.numberOfPlayers = this.NumberOfPlayers;
        GameObject hexGridGO = new GameObject("HexGrid");
        
        for(float y=0; y < gridHeightInHexes; y++)
        {
            for(float x=0;x<gridWithInHexes; x++)
            {
                GameObject hex = GameObject.Instantiate(Hex);
                Vector2 gridpos = new Vector2(x, y);
                hex.name = "Tile : " + y + "-" + x;
                hex.transform.position = calcWorldCoord(gridpos);
                hex.transform.parent = hexGridGO.transform;
                GameManager.addTile(hex.GetComponent<Tile>());
            }
        }
    }
	// Use this for initialization
	void Start () {
        setNumberOfPlayers();
        setSizes();
        createGrid();
        setPlayers();
	}
    /// <summary>
    /// sets the players on the grid and gives them a builder.
    /// </summary>
    void setPlayers()
    {
        GameObject middleTile = GameObject.Find("Tile : " + ((gridHeightInHexes /2 )- 1) + "-" + ((gridWithInHexes /2 )- 1));
        if (NumberOfPlayers >= 1)
        {
            GameObject rootNode = GameObject.Find("Tile : 0-0");
            GameObject build = GameObject.Instantiate(builder);
            build.transform.position = new Vector3(rootNode.transform.position.x, 0.5f, rootNode.transform.position.z);
            GameObject player1 = GameObject.Instantiate(player);
            player1.transform.parent = rootNode.transform;
            player1.name = "Player1";
            player1.transform.localPosition = new Vector3(0, 0, 0);
            PlayerManager player1comp = player1.GetComponent<PlayerManager>();
            player1comp.SetPlayerNr(1);
            player1comp.addUnit(build);
            build.GetComponent<Unit>().SetPlayer(player1comp);
            player1.transform.LookAt(middleTile.transform);
            player1comp.SetPlayerName(pls.getNamePlayer1());
            GameManager.AddPlayer(player1comp);
            build.transform.rotation = player1.transform.rotation;

        }
        if (NumberOfPlayers >= 2)
        {
            GameObject rootNode = GameObject.Find("Tile : "+(gridHeightInHexes -1)+"-"+(gridWithInHexes-1));
            GameObject build = GameObject.Instantiate(builder);
            build.transform.position = new Vector3(rootNode.transform.position.x, 0.5f, rootNode.transform.position.z); 
            GameObject player2 = GameObject.Instantiate(player);
            player2.transform.parent = rootNode.transform;
            player2.name = "Player2";
            player2.transform.localPosition = new Vector3(0, 0f, 0);
            PlayerManager player2comp = player2.GetComponent<PlayerManager>();
            player2comp.SetPlayerNr(2);
            player2comp.SetPlayerName(pls.getNamePlayer2());
            player2comp.addUnit(build);
            build.GetComponent<Unit>().SetPlayer(player2comp);
            player2.transform.LookAt(middleTile.transform);
            GameManager.AddPlayer(player2comp);
            build.transform.rotation = player2.transform.rotation;
        }
        if (NumberOfPlayers >= 3)
        {
            GameObject rootNode = GameObject.Find("Tile : 0-"+(gridWithInHexes-1));
            GameObject build = GameObject.Instantiate(builder);
            build.transform.position = new Vector3(rootNode.transform.position.x, 0.5f, rootNode.transform.position.z); 
            GameObject player3 = GameObject.Instantiate(player);
            player3.transform.parent = rootNode.transform;
            player3.name = "Player3";
            player3.transform.localPosition = new Vector3(0, 0f, 0);
            PlayerManager player3comp = player3.GetComponent<PlayerManager>();
            player3comp.SetPlayerNr(3);
            player3comp.SetPlayerName(pls.getNamePlayer3());
            player3comp.addUnit(build);
            build.GetComponent<Unit>().SetPlayer(player3comp);
            player3.GetComponent<PlayerManager>().SetPlayerName(pls.getNamePlayer3());
            build.GetComponent<Unit>().SetPlayer(player3comp);
            player3.transform.LookAt(middleTile.transform);
            GameManager.AddPlayer(player3comp);
            build.transform.rotation = player3.transform.rotation;
        }
        if (NumberOfPlayers >= 4)
        {
            GameObject rootNode = GameObject.Find("Tile : "+(gridHeightInHexes -1)+"-0");
            GameObject build = GameObject.Instantiate(builder);
            build.transform.position = new Vector3(rootNode.transform.position.x, 0.5f, rootNode.transform.position.z); 
            GameObject player4 = GameObject.Instantiate(player);
            player4.transform.parent = rootNode.transform;
            player4.name = "Player4";
            player4.transform.localPosition = new Vector3(0, 0f, 0);
            PlayerManager player4comp = player4.GetComponent<PlayerManager>();
            player4comp.SetPlayerNr(4);
            player4comp.SetPlayerName(pls.getNamePlayer4());
            player4comp.addUnit(build);
            build.GetComponent<Unit>().SetPlayer(player4comp);
            build.GetComponent<Unit>().SetPlayer(player4comp);
            player4.transform.LookAt(middleTile.transform);
            GameManager.AddPlayer(player4comp);
            build.transform.rotation = player4.transform.rotation;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
