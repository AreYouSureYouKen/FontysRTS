using UnityEngine;
using System.Collections;

public class GameOverMessage : MonoBehaviour {
    private string playerName;
    private int numberOfTurns;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetVictor(string name)
    {
        this.playerName = name;
    }

    public string GetVictor()
    {
        return this.playerName;
    }

    public void SetTurns(int turns)
    {
        this.numberOfTurns = turns;
    }

    public int GetNumberOfTurns()
    {
        return this.numberOfTurns;
    }
}
