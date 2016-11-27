using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScene : MonoBehaviour {
    public Text victor;
    public Text turns;
    private GameOverMessage gmv;
	// Use this for initialization
	void Start () {
        gmv = GameObject.FindWithTag("GameOverMessage").GetComponent<GameOverMessage>();

        victor.text = "Victor: "+gmv.GetVictor();
        turns.text = "In number of turns:"+gmv.GetNumberOfTurns();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startNewGame()
    {
        Destroy(gmv.gameObject);
        Destroy(GameObject.FindWithTag("PlayerSelect"));
        Application.LoadLevel("MenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
