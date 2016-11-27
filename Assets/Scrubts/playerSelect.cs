using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerSelect : MonoBehaviour {

    private int numberofPlayers;
    private string player1;
    private string player2;
    private string player3;
    private string player4;

    public GameObject numberPanel;
    public GameObject namePanel;
    public Text responseText;
    
    public GameObject name_player1;
    private InputField string_player1;
    public GameObject name_player2;
    private InputField string_player2;
    public GameObject name_player3;
    private InputField string_player3;
    public GameObject name_player4;
    private InputField string_player4;


	// Use this for initialization
	void Start () {
        string_player1 = name_player1.GetComponent<InputField>();
        string_player2 = name_player2.GetComponent<InputField>();
        string_player3 = name_player3.GetComponent<InputField>();
        string_player4 = name_player4.GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btn_2player()
    {
        this.numberofPlayers = 2;
        numberPanel.SetActive(false);
        namePanel.SetActive(true);
        activateInput();
    }

    public void btn_3player()
    {
        this.numberofPlayers = 3;
        numberPanel.SetActive(false);
        namePanel.SetActive(true);
        activateInput();
    }
   
    public void btn_4player()
    {
        this.numberofPlayers = 4;
        numberPanel.SetActive(false);
        namePanel.SetActive(true);
        activateInput();
    }
    
    public void btn_back()
    {
        numberPanel.SetActive(true);
        namePanel.SetActive(false);
        deactivateInput();
    }

    public void btn_close()
    {
        Application.Quit();
    }

    public void btn_start()
    {
        if (string_player1.text == "") player1 = "Player 1"; else player1 = string_player1.text;
        if (string_player2.text == "") player2 = "Player 2"; else player2 = string_player2.text;
        if (string_player3.text == "") player3 = "Player 3"; else player3 = string_player3.text;
        if (string_player4.text == "") player4 = "Player 4"; else player4 = string_player4.text;
        if (string_player1.text == "" || string_player2.text == "" || string_player3.text == "" || string_player4.text == "")
            responseText.text = "Empty player name found, defaulting that one.";
        DontDestroyOnLoad(this);
        Application.LoadLevel("MainScene");
    }

    private void activateInput()
    {
        name_player1.SetActive(true);
        name_player2.SetActive(true);
        if (this.numberofPlayers >= 3) name_player3.SetActive(true);
        if (this.numberofPlayers == 4) name_player4.SetActive(true);
    }

    private void deactivateInput()
    {
        name_player1.SetActive(false);
        name_player2.SetActive(false);
        name_player3.SetActive(false);
        name_player4.SetActive(false);
    }

    public int GetNumberOfPlayers()
    {
        return this.numberofPlayers;
    }

    public string getNamePlayer1()
    {
        return this.player1;
    }

    public string getNamePlayer2()
    {
        return this.player2;
    }

    public string getNamePlayer3()
    {
        return this.player3;
    }

    public string getNamePlayer4()
    {
        return this.player4;
    }
}
